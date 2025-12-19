using ITBS_Platform.Api.DTOs;
using ITBS_Platform.Api.Models;
using ITBS_Platform.Data;
using ITBS_Platform.Models;
using Microsoft.EntityFrameworkCore;

namespace ITBS_Platform.Api.Services
{
    public class CourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lister toutes les formations (TOUS les utilisateurs connectés)
        public async Task<List<CourseListDto>> ListerFormationsAsync()
        {
            var list = await _context.Courses
                .Select(c => new CourseListDto
                {
                    Id = c.Id,
                    Titre = c.Titre,
                    Description = c.Description,
                    DateDebut = c.DateDebut,
                    FormateurId = c.FormateurId,

                    FormateurNom = _context.Users
                        .Where(u => u.Id == c.FormateurId)
                        .Select(u => (u.Nom + " " + u.Prenom))
                        .FirstOrDefault() ?? ""
                })
                .ToListAsync();

            return list;
        }


        // Créer une formation (ADMIN)
        public async Task<Course> CreerFormationAsync(CreateCourseDto dto)
        {
            // 1️⃣ Vérifier que le formateur existe et qu'il a bien le rôle "Formateur"
            var formateur = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == dto.FormateurId);

            if (formateur == null)
                throw new InvalidOperationException("Le formateur spécifié n'existe pas.");

            if (formateur.Role == null || formateur.Role.RoleName != "Formateur")
                throw new InvalidOperationException("L'utilisateur spécifié n'a pas le rôle 'Formateur'.");

            // 2️⃣ Vérifier qu'il n'y a pas déjà une formation à la même date pour ce formateur
            bool conflit = await _context.Courses.AnyAsync(c =>
                c.FormateurId == dto.FormateurId &&
                c.DateDebut == dto.DateDebut);

            if (conflit)
                throw new InvalidOperationException("Ce formateur a déjà une formation à cette date / heure.");

            // 3️⃣ Création de la formation
            var course = new Course
            {
                Titre = dto.Titre,
                Description = dto.Description,
                DateDebut = dto.DateDebut,
                FormateurId = dto.FormateurId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course;
        }


        // Modifier une formation (ADMIN)
        public async Task<Course?> ModifierFormationAsync(int id, UpdateCourseDto dto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return null;

            // 1️⃣ Vérifier formateur
            var formateur = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == dto.FormateurId);

            if (formateur == null)
                throw new InvalidOperationException("Le formateur spécifié n'existe pas.");

            if (formateur.Role == null || formateur.Role.RoleName != "Formateur")
                throw new InvalidOperationException("L'utilisateur spécifié n'a pas le rôle 'Formateur'.");

            // 2️⃣ Vérifier conflit de planning
            bool conflit = await _context.Courses.AnyAsync(c =>
                c.Id != id && // on exclut la formation en cours de modification
                c.FormateurId == dto.FormateurId &&
                c.DateDebut == dto.DateDebut);

            if (conflit)
                throw new InvalidOperationException("Ce formateur a déjà une formation à cette date / heure.");

            // 3️⃣ Mise à jour
            course.Titre = dto.Titre;
            course.Description = dto.Description;
            course.DateDebut = dto.DateDebut;
            course.FormateurId = dto.FormateurId;

            await _context.SaveChangesAsync();

            return course;
        }


        // Supprimer une formation (ADMIN)
        public async Task<bool> SupprimerFormationAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return true;
        }

        // Modifier une formation par le formateur affecté (FORMATEUR SEULEMENT)
        public async Task<Course?> ModifierFormationParFormateurAsync(int id, UpdateCourseDto dto, int formateurId)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return null;

            // sécurité : le formateur connecté doit être celui affecté à la formation
            if (course.FormateurId != formateurId)
                return null;

            course.Titre = dto.Titre;
            course.Description = dto.Description;
            course.DateDebut = dto.DateDebut;
            // on ne change pas FormateurId ici

            await _context.SaveChangesAsync();

            return course;
        }

        public async Task InscrireEtudiantACourseAsync(int courseId, int userId)
        {
            // 1️⃣ Vérifier que la formation existe
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
                throw new InvalidOperationException("Formation introuvable.");

            // 2️⃣ Vérifier que l'utilisateur existe et est Étudiant
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new InvalidOperationException("Utilisateur introuvable.");

            if (user.Role == null || user.Role.RoleName != "Etudiant")
                throw new InvalidOperationException("Seuls les utilisateurs avec le rôle 'Etudiant' peuvent s'inscrire à une formation.");

            // 3️⃣ Vérifier qu'il n'est pas déjà inscrit à cette formation
            bool dejaInscrit = await _context.Participations.AnyAsync(p =>
                p.CourseId == courseId && p.UserId == userId);

            if (dejaInscrit)
                throw new InvalidOperationException("L'étudiant est déjà inscrit à cette formation.");

            var participation = new Participation
            {
                CourseId = courseId,
                EventId = null,
                UserId = userId,
                DateInscription = DateTime.UtcNow,
                Type = "Course",
                Statut = "En attente"
            };

            _context.Participations.Add(participation);
            await _context.SaveChangesAsync();
        }

        public async Task DesinscrireEtudiantDeCourseAsync(int courseId, int userId)
        {
            var participation = await _context.Participations
                .FirstOrDefaultAsync(p => p.CourseId == courseId && p.UserId == userId);

            if (participation == null)
                throw new InvalidOperationException("L'étudiant n'est pas inscrit à cette formation.");

            _context.Participations.Remove(participation);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> ListerParticipantsCourseAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
                throw new InvalidOperationException("Formation introuvable.");

            var participants = await _context.Participations
                .Where(p => p.CourseId == courseId)
                .Include(p => p.User)
                .Select(p => p.User)
                .ToListAsync();

            return participants;
        }


    }
}
