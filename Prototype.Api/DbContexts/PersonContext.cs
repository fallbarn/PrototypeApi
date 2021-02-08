using Microsoft.EntityFrameworkCore;
using Prototype.Api.Entities;

namespace Prototype.Api.DbContexts
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {

        }

        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person()
                {
                    Id = 1,
                    FirstName = "Micky",
                    LastName = "Mouse"
                },
                new Person()
                {
                    Id = 2,
                    FirstName = "Mini",
                    LastName = "Mouse"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
