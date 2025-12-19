using ITBS_Platform.Data;
using ITBS_Platform.Models;
using Microsoft.EntityFrameworkCore;

namespace ITBS_Platform.Api.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> ListerUtilisateursAsync()
        {
            return await _db.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User?> ModifierUtilisateurAsync(int id, User updatedUser)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return null;

            user.Nom = updatedUser.Nom;
            user.Prenom = updatedUser.Prenom;
            user.Email = updatedUser.Email;
            user.RoleId = updatedUser.RoleId;

            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<bool> SupprimerUtilisateurAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return false;

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetCurrentUserAsync(int id)
        {
            return await _db.Users.Include(x => x.Role).FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
