using Microsoft.EntityFrameworkCore;

namespace Cqrs.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>().HasData(new Student[]
            {

                new Student() { Id =1, Name="Samet", Surname="Dülger", Age=29},
                new Student() { Id =2, Name="Ahmet", Surname="Çelik", Age=28},
                new Student() { Id =3, Name="Mehmet", Surname="Demir", Age=27}

            });

            base.OnModelCreating(modelBuilder);

        }


    }
}
