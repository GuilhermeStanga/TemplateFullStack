using App.School.Domain.Students.Entities;
using App.School.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace App.School.Infra.Data.UoW
{
    public class SchoolDbContext : DbContext 
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new StudentMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }
    }
}
