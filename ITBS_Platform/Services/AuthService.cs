using ITBS_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ITBS_Platform.Services
{
    public class authService
    {
        // ✅ Liste d'utilisateurs en mémoire (mock)
        private static List<User> _mockUsers = new List<User>
        {
            new User
            {
                Id = 1,
                FullName = "Administrateur Système",
                Email = "admin@itbs.com",
                PasswordHash = HashPasswordStatic("admin123"),
                RoleId = 1,
                Role = new Role { Id = 1, Name = "Admin" }
            },
            new User
            {
                Id = 2,
                FullName = "Jean Formateur",
                Email = "formateur@itbs.com",
                PasswordHash = HashPasswordStatic("formateur123"),
                RoleId = 2,
                Role = new Role { Id = 2, Name = "Formateur" }
            },
            new User
            {
                Id = 3,
                FullName = "Marie Étudiante",
                Email = "etudiant@itbs.com",
                PasswordHash = HashPasswordStatic("etudiant123"),
                RoleId = 3,
                Role = new Role { Id = 3, Name = "Etudiant" }
            }
        };

        private static List<Role> _mockRoles = new List<Role>
        {
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Formateur" },
            new Role { Id = 3, Name = "Etudiant" }
        };

        public authService()
        {
            // Constructeur vide pour mock
        }

        // Hash du mot de passe
        public string HashPassword(string password)
        {
            return HashPasswordStatic(password);
        }

        private static string HashPasswordStatic(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // ✅ Login sans base de données
        public async Task<User?> LoginAsync(string email, string password)
        {
            await Task.Delay(500); // Simuler un délai de connexion

            var hashedPassword = HashPassword(password);
            var user = _mockUsers.FirstOrDefault(u =>
                u.Email.ToLower() == email.ToLower() &&
                u.PasswordHash == hashedPassword);

            return user;
        }

        // ✅ Créer un utilisateur sans base de données
        public async Task<bool> CreateUserAsync(string fullName, string email, string password, int roleId)
        {
            await Task.Delay(300); // Simuler un délai

            // Vérifier si l'email existe déjà
            if (_mockUsers.Any(u => u.Email.ToLower() == email.ToLower()))
                return false;

            var role = _mockRoles.FirstOrDefault(r => r.Id == roleId);
            if (role == null)
                return false;

            var newUser = new User
            {
                Id = _mockUsers.Count + 1,
                FullName = fullName,
                Email = email,
                PasswordHash = HashPassword(password),
                RoleId = roleId,
                Role = role
            };

            _mockUsers.Add(newUser);
            return true;
        }

        // ✅ Obtenir tous les rôles
        public async Task<List<Role>> GetAllRolesAsync()
        {
            await Task.Delay(100); // Simuler un délai
            return _mockRoles;
        }

        // ✅ Vérifier si un email existe
        public async Task<bool> EmailExistsAsync(string email)
        {
            await Task.Delay(100);
            return _mockUsers.Any(u => u.Email.ToLower() == email.ToLower());
        }
    }
}