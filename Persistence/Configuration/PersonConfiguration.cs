using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasData(
                new Person
                {
                    Id = 1234,
                    FirstName = "John",
                    LastName = "Doe",
                    Gender = Gender.Male,
                    PersonalId = "12345678901",
                    BirthDate = new DateTime(1990, 1, 1),
                    CityId = 1221
                },
                new Person
                {
                    Id = 1123,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Gender = Gender.Female,
                    PersonalId = "12341238901",
                    BirthDate = new DateTime(1992, 1, 1),
                    CityId = 1221
                }
            );
        }
    }

    public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.HasData(
                new PhoneNumber
                {
                    Id = 1,
                    Type = NumberType.Mobile,
                    Number = "1234527890",
                    PersonId = 1123
                }
            );
        }
    }

    public class RelatedPersonConfiguration : IEntityTypeConfiguration<RelatedPerson>
    {
        public void Configure(EntityTypeBuilder<RelatedPerson> builder)
        {
            builder.HasData(
                new RelatedPerson
                {
                    Id = 122,
                    RelationType = RelationType.Relative,
                    PersonId = 1123
                },
                new RelatedPerson
                {
                    Id = 111,
                    RelationType = RelationType.Colleague,
                    PersonId = 1123
                },
                new RelatedPerson
                {
                    Id = 141,
                    RelationType = RelationType.Acquaintance,
                    PersonId = 1234
                }
            );
        }
    }
}
