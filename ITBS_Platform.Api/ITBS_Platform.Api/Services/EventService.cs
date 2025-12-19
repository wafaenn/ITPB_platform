using ITBS_Platform.Api.DTOs;
using ITBS_Platform.Api.Models;
using ITBS_Platform.Data;
using ITBS_Platform.Models;
using Microsoft.EntityFrameworkCore;

namespace ITBS_Platform.Api.Services
{
    public class EventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lister tous les événements (TOUS les utilisateurs connectés)
        public async Task<List<Event>> ListerEvenementsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        // Créer un événement (ADMIN)
        public async Task<Event> CreerEvenementAsync(CreateEventDto dto)
        {
            // 🔒 Sécurité complémentaire côté service (en plus des [Required])
            if (string.IsNullOrWhiteSpace(dto.Titre))
                throw new InvalidOperationException("Le titre de l'événement est obligatoire.");

            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new InvalidOperationException("La description de l'événement est obligatoire.");

            if (dto.Date == default)
                throw new InvalidOperationException("La date de l'événement est obligatoire.");

            // 1️⃣ Vérifier qu'il n'y a pas déjà un événement à la même date/heure
            bool memeDate = await _context.Events.AnyAsync(e =>
                e.Date == dto.Date);

            if (memeDate)
                throw new InvalidOperationException("Un autre événement est déjà planifié à cette date et heure.");

            // 2️⃣ Vérifier qu'il n'y a pas un événement avec le même titre et la même date
            string titreNormalise = dto.Titre.Trim().ToLower();

            bool memeTitreEtDate = await _context.Events.AnyAsync(e =>
                e.Date == dto.Date &&
                e.Titre.ToLower() == titreNormalise);

            if (memeTitreEtDate)
                throw new InvalidOperationException("Un événement avec le même titre est déjà planifié à cette date.");

            // ✅ Création si tout est OK
            var ev = new Event
            {
                Titre = dto.Titre,
                Description = dto.Description,
                Date = dto.Date
            };

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            return ev;
        }


        // Modifier un événement (ADMIN)
        public async Task<Event?> ModifierEvenementAsync(int id, UpdateEventDto dto)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null)
                return null;

            if (string.IsNullOrWhiteSpace(dto.Titre))
                throw new InvalidOperationException("Le titre de l'événement est obligatoire.");

            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new InvalidOperationException("La description de l'événement est obligatoire.");

            if (dto.Date == default)
                throw new InvalidOperationException("La date de l'événement est obligatoire.");

            // 1️⃣ Vérifier conflit sur la date (un autre événement à la même date/heure)
            bool memeDate = await _context.Events.AnyAsync(e =>
                e.Id != id &&                // on exclut l'événement courant
                e.Date == dto.Date);

            if (memeDate)
                throw new InvalidOperationException("Un autre événement est déjà planifié à cette date et heure.");

            // 2️⃣ Vérifier même titre + même date sur un autre événement
            string titreNormalise = dto.Titre.Trim().ToLower();

            bool memeTitreEtDate = await _context.Events.AnyAsync(e =>
                e.Id != id &&
                e.Date == dto.Date &&
                e.Titre.ToLower() == titreNormalise);

            if (memeTitreEtDate)
                throw new InvalidOperationException("Un événement avec le même titre est déjà planifié à cette date.");

            // ✅ Mise à jour
            ev.Titre = dto.Titre;
            ev.Description = dto.Description;
            ev.Date = dto.Date;

            await _context.SaveChangesAsync();

            return ev;
        }


        // Supprimer un événement (ADMIN)
        public async Task<bool> SupprimerEvenementAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null)
                return false;

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task InscrireEtudiantAEventAsync(int eventId, int userId)
        {
            // 1️⃣ Vérifier que l'événement existe
            var ev = await _context.Events.FindAsync(eventId);
            if (ev == null)
                throw new InvalidOperationException("Événement introuvable.");

            // 2️⃣ Vérifier que l'utilisateur existe ET est étudiant
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new InvalidOperationException("Utilisateur introuvable.");

            if (user.Role == null || user.Role.RoleName != "Etudiant")
                throw new InvalidOperationException("Seuls les utilisateurs avec le rôle 'Etudiant' peuvent s'inscrire.");

            // 3️⃣ Vérifier qu'il n'est pas déjà inscrit
            bool dejaInscrit = await _context.Participations.AnyAsync(p =>
                p.EventId == eventId && p.UserId == userId);

            if (dejaInscrit)
                throw new InvalidOperationException("L'étudiant est déjà inscrit à cet événement.");

            // 4️⃣ Empêcher une inscription sur un événement passé (optionnel mais logique)
            if (ev.Date < DateTime.UtcNow)
                throw new InvalidOperationException("Impossible de s'inscrire à un événement déjà passé.");

            var participation = new Participation
            {
                EventId = eventId,
                CourseId = null,
                UserId = userId,
                DateInscription = DateTime.UtcNow,
                Type = "Event"
            };

            _context.Participations.Add(participation);
            await _context.SaveChangesAsync();
        }

        public async Task DesinscrireEtudiantDeEventAsync(int eventId, int userId)
        {
            var participation = await _context.Participations
                .FirstOrDefaultAsync(p => p.EventId == eventId && p.UserId == userId);

            if (participation == null)
                throw new InvalidOperationException("L'étudiant n'est pas inscrit à cet événement.");

            _context.Participations.Remove(participation);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> ListerParticipantsEventAsync(int eventId)
        {
            var ev = await _context.Events.FindAsync(eventId);
            if (ev == null)
                throw new InvalidOperationException("Événement introuvable.");

            var participants = await _context.Participations
                .Where(p => p.EventId == eventId)
                .Include(p => p.User)
                .Select(p => p.User)
                .ToListAsync();

            return participants;
        }
    }
}
