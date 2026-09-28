using Educational.Application;
using Educational.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder=WebApplication.CreateBuilder(args);
builder.Services.AddEducationalInfrastructure(builder.Configuration);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o=>{o.LoginPath="/Account/Login";o.LogoutPath="/Account/Logout";});
builder.Services.AddAuthorization();
builder.Services.AddRazorPages();
var app=builder.Build();
using(var scope=app.Services.CreateScope()){var db=scope.ServiceProvider.GetRequiredService<AppDbContext>();await db.Database.EnsureCreatedAsync();}
app.UseStaticFiles();app.UseRouting();app.UseAuthentication();app.UseAuthorization();app.MapRazorPages();app.Run();