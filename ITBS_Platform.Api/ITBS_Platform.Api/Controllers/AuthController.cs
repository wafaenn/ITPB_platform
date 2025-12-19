using Microsoft.AspNetCore.Mvc;
using ITBS_Platform.Api.DTOs;
using ITBS_Platform.Api.Services;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;
    public AuthController(AuthService auth) { _auth = auth; }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (success, message) = await _auth.RegisterAsync(
            dto.Nom,
            dto.Prenom,
            dto.Email,
            dto.MotDePasse,
            dto.RoleId
        );

        if (!success)
            return BadRequest(new { message });

        return Ok(new { message = "Utilisateur créé" });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _auth.LoginAsync(dto.Email, dto.Password);
        if (token == null) return Unauthorized(new { message = "Email ou mot de passe incorrect" });
        return Ok(new { token });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _auth.Logout(); // appelle la méthode vide, utile si plus tard tu veux une blacklist
        return Ok(new { message = "Déconnexion réussie" });
    }


}
