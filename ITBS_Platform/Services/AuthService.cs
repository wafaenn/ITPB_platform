using ITBS_Platform.Data;
using ITBS_Platform.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ITBS_Platform.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hash du mot de passe
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // Vérifier le login AVEC EMAIL
        public async Task<User?> LoginAsync(string email, string password)
        {
            var hashedPassword = HashPassword(password);

            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == hashedPassword);
        }

        // Créer un utilisateur
        public async Task<bool> CreateUserAsync(string fullName, string email, string password, int roleId)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return false;

            var user = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = HashPassword(password),
                RoleId = roleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
