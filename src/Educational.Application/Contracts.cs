using Educational.Domain;

namespace Educational.Application;

public record StudentDto(Guid Id,string FirstName,string LastName,string Email,string? Phone,DateTime? DateOfBirth,bool IsActive);
public record TeacherDto(Guid Id,string FirstName,string LastName,string Email,string? Phone,string Subject,bool IsActive);
public record LessonDto(Guid Id,string Title,string? Description,Guid TeacherId,string TeacherName,DateTime StartTime,DateTime EndTime,bool IsActive);
public record LoginResult(bool Succeeded,string? Token,string? DisplayName,string? Error);

public interface IStudentService
{
    Task<IReadOnlyList<StudentDto>> GetAllAsync(CancellationToken ct);
    Task<StudentDto?> GetAsync(Guid id,CancellationToken ct);
    Task<StudentDto> CreateAsync(StudentDto dto,CancellationToken ct);
    Task<bool> DeleteAsync(Guid id,CancellationToken ct);
}
public interface ITeacherService
{
    Task<IReadOnlyList<TeacherDto>> GetAllAsync(CancellationToken ct);
    Task<TeacherDto?> GetAsync(Guid id,CancellationToken ct);
    Task<TeacherDto> CreateAsync(TeacherDto dto,CancellationToken ct);
    Task<bool> DeleteAsync(Guid id,CancellationToken ct);
}
public interface ILessonService
{
    Task<IReadOnlyList<LessonDto>> GetAllAsync(CancellationToken ct);
    Task<LessonDto?> GetAsync(Guid id,CancellationToken ct);
    Task<LessonDto> CreateAsync(LessonDto dto,CancellationToken ct);
    Task<bool> DeleteAsync(Guid id,CancellationToken ct);
}
public interface IAuthService
{
    Task<LoginResult> LoginAsync(string email,string password,bool createToken,CancellationToken ct);
}