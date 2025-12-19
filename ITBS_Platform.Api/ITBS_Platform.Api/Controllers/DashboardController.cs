using ITBS_Platform.Api.DTOs;
using ITBS_Platform.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITBS_Platform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("admin-stats")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DashboardStatsDto>> GetAdminStats()
        {
            var totalUsers = await _db.Users.CountAsync();
            var totalCourses = await _db.Courses.CountAsync();
            var totalEvents = await _db.Events.CountAsync();

            // ✅ demandes en attente : Participation.Statut == "En attente"
            // (et on peut filtrer Type == "Course" si tu veux uniquement les demandes de formation)
            var pendingRegistrations = await _db.Participations.CountAsync(p =>
                p.Statut == "En attente" && p.Type == "Course"
            );

            // ✅ formations actives : à adapter selon ton modèle exact Course
            // Si Course.DateDebut existe et n’est pas nullable:
            var activeCourses = await _db.Courses.CountAsync(c =>
                c.DateDebut <= DateTime.Today
            );

            return Ok(new DashboardStatsDto
            {
                TotalUsers = totalUsers,
                TotalCourses = totalCourses,
                TotalEvents = totalEvents,
                ActiveCourses = activeCourses,
                PendingRegistrations = pendingRegistrations
            });
        }
    }
}
