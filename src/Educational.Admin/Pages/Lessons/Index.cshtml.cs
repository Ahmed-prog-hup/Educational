using Educational.Application;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Educational.Admin.Pages.Lessons;
public class IndexModel : PageModel
{
    public IReadOnlyList<LessonDto> Items { get; private set; } = [];
    public void OnGet() { }
}