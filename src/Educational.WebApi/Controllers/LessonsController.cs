using Educational.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Educational.WebApi.Controllers;
[ApiController,Route("api/lessons"),Authorize]
public class LessonsController(ILessonService service) : ControllerBase
{
    [HttpGet] public Task<IReadOnlyList<LessonDto>> Get(CancellationToken ct)=>service.GetAllAsync(ct);
    [HttpGet("{id:guid}")] public async Task<IActionResult> Get(Guid id,CancellationToken ct)=>Ok(await service.GetAsync(id,ct));
    [HttpPost] public Task<LessonDto> Post(LessonDto dto,CancellationToken ct)=>service.CreateAsync(dto,ct);
    [HttpDelete("{id:guid}")] public async Task<IActionResult> Delete(Guid id,CancellationToken ct)=>await service.DeleteAsync(id,ct)?NoContent():NotFound();
}