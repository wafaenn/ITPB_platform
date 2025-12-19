using ITBS_Platform.Api.DTOs;
using ITBS_Platform.Api.Services;
using ITBS_Platform.Data;
using Microsoft.AspNetCore.Authorization;
using ITBS_Platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;





namespace ITBS_Platform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;
        private readonly ApplicationDbContext _context;

        public CourseController(CourseService courseService, ApplicationDbContext context)
        {
            _courseService = courseService;
            _context = context;
        }

        // GET: api/Course
        // TOUS : tous les utilisateurs authentifiés peuvent lister les formations
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.ListerFormationsAsync();
            return Ok(courses);
        }

        // POST: api/Course
       
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var course = await _courseService.CreerFormationAsync(dto);
                return Ok(course);
            }
            catch (InvalidOperationException ex)
            {
                // Règles métier non respectées → 400
                return BadRequest(new { message = ex.Message });
            }
        }


        // PUT: api/Course/{id}
        // ADMIN : modifier une formation
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var course = await _courseService.ModifierFormationAsync(id, dto);
                if (course == null)
                    return NotFound(new { message = "Formation non trouvée" });

                return Ok(course);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _courseService.SupprimerFormationAsync(id);
            if (!deleted)
                return NotFound(new { message = "Formation non trouvée" });

            return Ok(new { message = "Formation supprimée" });
        }

        // PUT: api/Course/{id}/formateur
        // FORMATEUR SEULEMENT : modifier une formation dont il est responsable
        [HttpPut("{id}/formateur")]
        [Authorize(Roles = "Formateur")]
        public async Task<IActionResult> UpdateByFormateur(int id, [FromBody] UpdateCourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Token invalide" });

            var formateurId = int.Parse(userIdClaim);

            var course = await _courseService.ModifierFormationParFormateurAsync(id, dto, formateurId);
            if (course == null)
                return NotFound(new { message = "Formation non trouvée ou accès refusé" });

            return Ok(course);
        }



        // POST: api/Course/{courseId}/register
        // Étudiant : s'inscrire à une formation
        [HttpPost("{courseId}/register")]
        [Authorize(Roles = "Etudiant")]
        public async Task<IActionResult> RegisterToCourse(int courseId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Token invalide (id utilisateur manquant)." });

            var userId = int.Parse(userIdClaim);

            try
            {
                await _courseService.InscrireEtudiantACourseAsync(courseId, userId);
                return Ok(new { message = "Inscription à la formation réussie." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Course/{courseId}/register
        // Étudiant : se désinscrire d'une formation
        [HttpDelete("{courseId}/register")]
        [Authorize(Roles = "Etudiant")]
        public async Task<IActionResult> UnregisterFromCourse(int courseId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Token invalide (id utilisateur manquant)." });

            var userId = int.Parse(userIdClaim);

            try
            {
                await _courseService.DesinscrireEtudiantDeCourseAsync(courseId, userId);
                return Ok(new { message = "Désinscription de la formation réussie." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Course/{courseId}/participants
        // Admin : voir la liste des participants à une formation
        [HttpGet("{courseId}/participants")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCourseParticipants(int courseId)
        {
            try
            {
                var participants = await _courseService.ListerParticipantsCourseAsync(courseId);
                return Ok(participants);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        public class UpdateParticipationStatusDto
        {
            public string Statut { get; set; } // "En attente" / "Approuvée" / "Refusée"
        }

        // PUT: api/Course/registrations/{participationId}/status
        [HttpPut("registrations/{participationId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRegistrationStatus(
            int participationId,
            [FromBody] UpdateParticipationStatusDto dto)
        {
            var participation = await _context.Participations.FindAsync(participationId);
            if (participation == null || participation.CourseId == null)
                return NotFound(new { message = "Inscription introuvable." });

            if (dto.Statut != "En attente" && dto.Statut != "Approuvée" && dto.Statut != "Refusée")
                return BadRequest(new { message = "Statut invalide." });

            participation.Statut = dto.Statut;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Statut mis à jour." });
        }

        // GET: api/Course/registrations/pending
        [HttpGet("registrations/pending")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPendingRegistrations()
        {
            var list = await _context.Participations
                .Include(p => p.Course)
                .Include(p => p.User)
                .Where(p => p.CourseId != null && p.Statut == "En attente")
                .Select(p => new
                {
                    p.Id,
                    CourseId = p.CourseId,
                    CourseTitre = p.Course.Titre,
                    EtudiantId = p.UserId,
                    EtudiantNom = p.User.Nom + " " + p.User.Prenom,
                    p.Statut,
                    p.DateInscription
                })
                .ToListAsync();

            return Ok(list);
        }



        // GET: api/Course/my-registrations
        [HttpGet("my-registrations")]
        [Authorize(Roles = "Etudiant")]
        public async Task<IActionResult> GetMyRegistrations()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var etudiantId = int.Parse(userIdClaim);

            var list = await _context.Participations
                .Include(p => p.Course)
                .Where(p => p.CourseId != null && p.UserId == etudiantId)
                .Select(p => new
                {
                    p.CourseId,
                    p.Course.Titre,
                    p.Course.Description,
                    p.Statut,
                    p.DateInscription
                })
                .ToListAsync();

            return Ok(list);
        }




    }
}
