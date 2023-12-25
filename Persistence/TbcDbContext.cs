using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Configuration;

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

            modelBuilder.Entity<Person>()
                .Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(p => p.PersonalId)
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength();

            modelBuilder.Entity<Person>()
                .Property(p => p.CityId)
                .IsRequired();

            modelBuilder.Entity<PhoneNumber>().HasKey(e => e.Id);

            modelBuilder.Entity<RelatedPerson>().HasKey(e => e.Id);

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PhoneNumberConfiguration());
            modelBuilder.ApplyConfiguration(new RelatedPersonConfiguration());

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<RelatedPerson> RelatedPersons { get; set; }
    }
}