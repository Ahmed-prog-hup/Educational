using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Educational.Admin.Pages.Account;
public class LoginModel : PageModel
{
 [BindProperty] public string Email { get; set; } = "admin@educational.local";
 [BindProperty] public string Password { get; set; } = "";
 public IActionResult OnPost() => RedirectToPage("/Index");
}