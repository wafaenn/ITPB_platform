using ITBS_Platform.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITBS_Platform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _context.Roles
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.RoleName
                })
                .ToListAsync();

            return Ok(roles);
        }
    }
}
