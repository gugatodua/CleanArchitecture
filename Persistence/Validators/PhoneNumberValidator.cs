using Domain;
using FluentValidation;

namespace Persistence.Validators
{
    public class PhoneNumberValidator : AbstractValidator<PhoneNumber>
    {
        public PhoneNumberValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty()
                .Length(4, 50);

            RuleFor(x => x.Type)
                .IsInEnum();
        }
    }
}