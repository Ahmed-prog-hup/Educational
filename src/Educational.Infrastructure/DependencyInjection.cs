using Educational.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Educational.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddEducationalInfrastructure(this IServiceCollection services,IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(o=>o.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        services.AddScoped<IStudentService,StudentService>();
        services.AddScoped<ITeacherService,TeacherService>();
        services.AddScoped<ILessonService,LessonService>();
        services.AddScoped<IAuthService,AuthService>();
        return services;
    }
}