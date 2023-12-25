using Application.Persons.Commands;
using FluentValidation;
using Microsoft.Extensions.Localization;
using TBCInterviewProject.Api.Resources;

namespace TBCInterviewProject.Api.Validation
{
    public class AddRelatedPersonCommandValidator : AbstractValidator<AddRelatedPersonCommand>
    {
        public AddRelatedPersonCommandValidator(IStringLocalizer<ErrorResources> localizer)
        {
            
        }
    }
}
