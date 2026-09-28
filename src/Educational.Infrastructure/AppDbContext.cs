using Educational.Domain;
using Microsoft.EntityFrameworkCore;
namespace Educational.Infrastructure;
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
 public DbSet<Student> Students=>Set<Student>(); public DbSet<Teacher> Teachers=>Set<Teacher>(); public DbSet<Lesson> Lessons=>Set<Lesson>(); public DbSet<StudentLesson> StudentLessons=>Set<StudentLesson>(); public DbSet<AppUser> Users=>Set<AppUser>();
 protected override void OnModelCreating(ModelBuilder b){
  b.HasDefaultSchema("Educational");
  b.Entity<Student>().ToTable("Students").HasKey(x=>x.Id); b.Entity<Teacher>().ToTable("Teachers").HasKey(x=>x.Id); b.Entity<Lesson>().ToTable("Lessons").HasKey(x=>x.Id); b.Entity<AppUser>().ToTable("Users").HasKey(x=>x.Id); b.Entity<StudentLesson>().ToTable("StudentLessons").HasKey(x=>new{x.StudentId,x.LessonId});
  b.Entity<Lesson>().HasOne(x=>x.Teacher).WithMany(x=>x.Lessons).HasForeignKey(x=>x.TeacherId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<StudentLesson>().HasOne(x=>x.Student).WithMany(x=>x.Enrollments).HasForeignKey(x=>x.StudentId); b.Entity<StudentLesson>().HasOne(x=>x.Lesson).WithMany(x=>x.Enrollments).HasForeignKey(x=>x.LessonId);
  b.Entity<AppUser>().HasIndex(x=>x.Email).IsUnique();
  b.Entity<AppUser>().HasData(new AppUser{Id=Guid.Parse("11111111-1111-1111-1111-111111111111"),Email="admin@educational.local",DisplayName="Administrator",PasswordHash="DEMO_PASSWORD",CreatedAt=new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc)});
 }
}