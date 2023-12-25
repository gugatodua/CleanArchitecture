using Application.Persons.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using TBCInterviewProject.Api.Resources;

namespace TBCInterviewProject.Api.Validation
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator(IStringLocalizer<ErrorResources> localizer)
        {
            RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage(localizer["FirstNameRequired"])
               .Length(2, 50).WithMessage(localizer["FirstNameLengthInvalid"])
               .Matches(@"^[a-zA-Zა-ჰ]+$").WithMessage(localizer["FirstNameOnlyInGeorgian"]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(localizer["LastNameRequired"])
                .Length(2, 50).WithMessage(localizer["LastNameLengthInvalid"])
                .Matches(@"^[a-zA-Zა-ჰ]+$").WithMessage(localizer["LastNameOnlyInGeorgian"]);

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage(localizer["GenderRequired"]);

            RuleFor(x => x.PersonalId)
                .NotEmpty().WithMessage(localizer["PersonalNumberRequired"])
                .Length(11).WithMessage(localizer["PersonalIdInvalidLength"]);

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage(localizer["BirthDateRequired"]);

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage(localizer["CityIdRequired"]);

            RuleFor(x => x.PhoneNumberDtos)
                .NotEmpty().WithMessage(localizer["PhoneNumberRequired"])
                .Must(x => x != null && x.Count > 0).WithMessage(localizer["PhoneNumberRequired"]);
        }

    }
}
