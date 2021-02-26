using Microsoft.EntityFrameworkCore;

namespace ToDo_Application.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().ToTable("Todos");
            modelBuilder.Entity<ToDo>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<ToDo>().Property(p => p.IsCompleted).HasDefaultValue(false);
        }
    }
}
