using ITBS_Platform.Api.DTOs;
using ITBS_Platform.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace ITBS_Platform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/Event
        // TOUS : tous les utilisateurs authentifiés peuvent lister les événements
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var eventsList = await _eventService.ListerEvenementsAsync();
            return Ok(eventsList);
        }

        // POST: api/Event
        // ADMIN : créer un événement

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateEventDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ev = await _eventService.CreerEvenementAsync(dto);
                return Ok(ev);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // PUT: api/Event/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEventDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ev = await _eventService.ModifierEvenementAsync(id, dto);
                if (ev == null)
                    return NotFound(new { message = "Événement non trouvé" });

                return Ok(ev);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // DELETE: api/Event/{id}
        // ADMIN : supprimer un événement
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _eventService.SupprimerEvenementAsync(id);
            if (!deleted)
                return NotFound(new { message = "Événement non trouvé" });

            return Ok(new { message = "Événement supprimé" });
        }

        // POST: api/Event/{eventId}/register
        // Étudiant : s'inscrire à un événement
        [HttpPost("{eventId}/register")]
        [Authorize(Roles = "Etudiant")]
        public async Task<IActionResult> RegisterToEvent(int eventId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Token invalide (id utilisateur manquant)." });

            var userId = int.Parse(userIdClaim);

            try
            {
                await _eventService.InscrireEtudiantAEventAsync(eventId, userId);
                return Ok(new { message = "Inscription réussie." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Event/{eventId}/register
        // Étudiant : se désinscrire d'un événement
        [HttpDelete("{eventId}/register")]
        [Authorize(Roles = "Etudiant")]
        public async Task<IActionResult> UnregisterFromEvent(int eventId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Token invalide (id utilisateur manquant)." });

            var userId = int.Parse(userIdClaim);

            try
            {
                await _eventService.DesinscrireEtudiantDeEventAsync(eventId, userId);
                return Ok(new { message = "Désinscription réussie." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Event/{eventId}/participants
        // Admin : voir la liste des participants
        [HttpGet("{eventId}/participants")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetParticipants(int eventId)
        {
            try
            {
                var participants = await _eventService.ListerParticipantsEventAsync(eventId);
                return Ok(participants);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
