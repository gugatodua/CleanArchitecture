using Application.Persons.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using TBCInterviewProject.Api.Resources;

namespace TBCInterviewProject.Api.Validation
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator(IStringLocalizer<ErrorResources> localizer)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(localizer["FirstNameRequired"])
                .Length(2, 50).WithMessage(localizer["FirstNameLengthInvalid"]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(localizer["LastNameRequired"])
                .Length(2, 50).WithMessage(localizer["LastNameLengthInvalid"]);

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage(localizer["GenderRequired"]);

            RuleFor(x => x.PersonalNumber)
                .NotEmpty().WithMessage(localizer["PersonalNumberRequired"]);

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage(localizer["BirthDateRequired"]);

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage(localizer["CityIdRequired"]);

            RuleFor(x => x.PhoneNumbers)
                .NotEmpty().WithMessage(localizer["PhoneNumberRequired"])
                .Must(x => x != null && x.Count > 0).WithMessage(localizer["PhoneNumberRequired"]);
        }
    }
}
