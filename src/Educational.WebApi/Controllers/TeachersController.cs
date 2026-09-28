using Educational.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Educational.WebApi.Controllers;
[ApiController,Route("api/teachers"),Authorize]
public class TeachersController(ITeacherService service) : ControllerBase
{
    [HttpGet] public Task<IReadOnlyList<TeacherDto>> Get(CancellationToken ct)=>service.GetAllAsync(ct);
    [HttpGet("{id:guid}")] public async Task<IActionResult> Get(Guid id,CancellationToken ct)=>Ok(await service.GetAsync(id,ct));
    [HttpPost] public Task<TeacherDto> Post(TeacherDto dto,CancellationToken ct)=>service.CreateAsync(dto,ct);
    [HttpDelete("{id:guid}")] public async Task<IActionResult> Delete(Guid id,CancellationToken ct)=>await service.DeleteAsync(id,ct)?NoContent():NotFound();
}