using ContosoUniversity.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Membership;
using Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Permission;

namespace ContosoUniversity.DAL
{
    [DbConfigurationType(typeof(TemplateDBConfiguration))]
    public class TemplateDBContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<EFMembership> Memberships { get; set; }
        public DbSet<EFPermission> Permissions { get; set; }
        public DbSet<EFRoll> Rolls { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));

            modelBuilder.Entity<EFMembership>()
                .HasMany(m => m.Rolls)
                .WithMany(r => r.Members)
                .Map(t => t.MapLeftKey("UserID")
                    .MapRightKey("RollID").
                    ToTable("EFMembershipRolls"));

            modelBuilder.Entity<EFRoll>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Rolls)
                .Map(m => m.MapLeftKey("RollID").MapRightKey("PermissionID").ToTable("EFRollPermissions"));

            modelBuilder.Entity<Department>().MapToStoredProcedures();
        }
    }
}