using Application.CQRS.Tags.Commands.TagStorage;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Tags.Commands.TagStorage
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator(IAdvertoDbContext context)
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(TagValidationOptions.TitleMinLength, TagValidationOptions.TitleMaxLength)
                .UniqueInsideOfDbSetColumn(context.Tags, t => t.Title);
        }
    }
}