using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public class TbcDbContext : DbContext
    {
        public TbcDbContext(DbContextOptions<TbcDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(p => p.PhoneNumbers)
                .WithOne(pn => pn.Person)
                .HasForeignKey(pn => pn.PersonId);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.RelatedPeople)
                .WithOne(rp => rp.Person)
                .HasForeignKey(rp => rp.PersonId);

            modelBuilder.Entity<PhoneNumber>().HasKey(e => e.Id);

            modelBuilder.Entity<RelatedPerson>().HasKey(e => e.Id);
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<RelatedPerson> RelatedPersons { get; set; }
    }
}