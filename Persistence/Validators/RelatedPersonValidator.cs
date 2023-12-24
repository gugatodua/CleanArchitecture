using Domain;
using FluentValidation;

namespace Persistence.Validators
{
    public class RelatedPersonValidator : AbstractValidator<RelatedPerson>
    {
        public RelatedPersonValidator()
        {
            RuleFor(x => x.RelationType)
                .IsInEnum();
        }
    }
}
