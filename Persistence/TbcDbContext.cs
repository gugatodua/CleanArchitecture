using Domain;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Persistence
{
    public class TbcDbContext : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<RelatedPerson> RelatedPersons { get; set; }
    }
}