using Educational.Application; using Microsoft.AspNetCore.Mvc;
namespace Educational.WebApi.Controllers;
[ApiController,Route("api/auth")]
public class AuthController(IAuthService auth):ControllerBase{[HttpPost("login")]public async Task<IActionResult> Login(LoginRequest r,CancellationToken ct){var x=await auth.LoginAsync(r.Email,r.Password,true,ct);return x.Succeeded?Ok(x):Unauthorized(x);}}
public record LoginRequest(string Email,string Password);