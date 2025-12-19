using ITBS_Platform.Api.DTOs;
using ITBS_Platform.Api.Services;
using ITBS_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ITBS_Platform.Data;
using System.ComponentModel.DataAnnotations;



namespace ITBS_Platform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _context;


        public UserController(UserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        // 1️⃣ GET tous les utilisateurs (Admin)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.ListerUtilisateursAsync();
            return Ok(users);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //  Empêcher les champs vides
            if (string.IsNullOrWhiteSpace(dto.Nom) ||
                string.IsNullOrWhiteSpace(dto.Prenom) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                return BadRequest(new { message = "Nom, Prénom et Email sont obligatoires et ne peuvent pas être vides." });
            }

            if (!new EmailAddressAttribute().IsValid(dto.Email))
            {
                return BadRequest(new { message = "L'adresse email est invalide." });
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { message = "Utilisateur non trouvé." });

            user.Nom = dto.Nom;
            user.Prenom = dto.Prenom;
            user.Email = dto.Email;
            user.MotDePasse = BCrypt.Net.BCrypt.HashPassword(dto.MotDePasse);
            user.RoleId = dto.RoleId;

            await _context.SaveChangesAsync();
            await _context.Entry(user).Reference(u => u.Role).LoadAsync();

            return Ok(user);
        }


        // 3️⃣ Supprimer utilisateur (Admin)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.SupprimerUtilisateurAsync(id);
            if (!deleted) return NotFound(new { message = "Utilisateur non trouvé" });

            return Ok(new { message = "Utilisateur supprimé" });
        }

        // 4️⃣ Récupérer son propre compte
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userService.GetCurrentUserAsync(userId);

            return Ok(user);
        }



        [HttpGet("safe")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPasMotPass()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserListDto
                {
                    Id = u.Id,
                    Nom = u.Nom,
                    Prenom = u.Prenom,
                    Email = u.Email,
                    RoleId = u.RoleId,
                    RoleName = u.Role.RoleName
                })
                .ToListAsync();

            return Ok(users);
        }


    }
}


