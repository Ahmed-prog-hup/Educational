namespace Educational.Domain;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}

public sealed class Student : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public ICollection<StudentLesson> Enrollments { get; set; } = new List<StudentLesson>();
}

public sealed class Teacher : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Subject { get; set; } = string.Empty;
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}

public sealed class Lesson : Entity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ICollection<StudentLesson> Enrollments { get; set; } = new List<StudentLesson>();
}

public sealed class StudentLesson
{
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }
    public Guid LessonId { get; set; }
    public Lesson? Lesson { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
}

public sealed class AppUser : Entity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
}
