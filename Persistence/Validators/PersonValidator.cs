using Domain;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Persistence.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator(IStringLocalizer localizer)
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .Length(2, 50)
                .Matches(@"^[a-zA-Zა-ჰ]+$")
                .WithMessage(localizer["FirstNameOnlyInGeorgian"]);

            RuleFor(p => p.LastName)
                .NotEmpty()
                .Length(2, 50)
                .Matches(@"^[a-zA-Zა-ჰ]+$")
                .WithMessage(localizer["LastNameOnlyInGeorgian"]);

            RuleFor(p => p.Gender)
                .IsInEnum()
                .WithMessage(localizer["InvalidGender"]);

            RuleFor(p => p.PersonalId)
                .NotEmpty()
                .Length(11)
                .WithMessage(localizer["PersonalIdInvalidLength"]);

            RuleFor(p => p.BirthDate)
                .NotEmpty()
                .LessThan(DateTime.UtcNow)
                .WithMessage(localizer["InvalidBirthDate"]);

            RuleFor(p => p.CityId)
                .NotEmpty()
                .WithMessage(localizer["CityIdRequired"]);
        }
    }
}