using Application.CQRS.Tags.Commands.TagStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Tags.Commands.TagStorage
{
    public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
    {
        public DeleteTagCommandValidator()
        {
            RuleFor(c => c.TagId)
                .NotEmpty();
        }
    }
}