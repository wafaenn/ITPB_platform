using ITBS_Platform.Data;
using ITBS_Platform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITBS_Platform.Api.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;

        public AuthService(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        // NOUVELLE SIGNATURE : nom + prenom
        public async Task<(bool Success, string Message)> RegisterAsync(
            string nom,
            string prenom,
            string email,
            string password,
            int roleId)
        {
            // Vérifier email déjà utilisé
            if (await _db.Users.AnyAsync(u => u.Email == email))
                return (false, "Email déjà utilisé");

            // Vérifier que le role existe
            if (!await _db.Roles.AnyAsync(r => r.Id == roleId))
                return (false, "Rôle invalide");

            // Hash du mot de passe
            var hashed = BCrypt.Net.BCrypt.HashPassword(password);

            // Créer l'utilisateur
            var user = new User
            {
                Nom = nom,
                Prenom = prenom,
                Email = email,
                MotDePasse = hashed,
                RoleId = roleId
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return (true, "Utilisateur créé avec succès");
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _db.Users.Include(u => u.Role)
                                      .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, user.MotDePasse)) return null;

            return GenerateJwt(user);
        }


        public void Logout()
        {
            // JWT = stateless → rien à effacer côté serveur
            // Le front/Postman doit supprimer le token localement.
            // Cette méthode existe pour respecter ton architecture.
        }


        private string GenerateJwt(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.Nom} {user.Prenom}"),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(double.Parse(_config["Jwt:ExpiryHours"] ?? "5")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
