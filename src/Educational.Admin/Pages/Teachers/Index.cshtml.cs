using Educational.Application;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Educational.Admin.Pages.Teachers;
public class IndexModel : PageModel
{
    private readonly ITeacherService _service;
    public IndexModel(ITeacherService service) => _service = service;
    public IReadOnlyList<TeacherDto> Items { get; private set; } = [];
    public async Task OnGetAsync(CancellationToken ct) => Items = await _service.GetAllAsync(ct);
}