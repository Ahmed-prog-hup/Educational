using Educational.Application; using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Educational.Admin.Pages.Students;
public class IndexModel(IStudentService service):PageModel{public IReadOnlyList<StudentDto> Items{get;private set;}=[];public async Task OnGetAsync(CancellationToken ct)=>Items=await service.GetAllAsync(ct);}