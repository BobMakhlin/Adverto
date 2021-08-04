using Application.CQRS.Ads.Commands.AdStorage.Update.Realisations;
using Application.Persistence.Interfaces;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage.Update
{
    public class UpdateHtmlAdCommandValidator : AbstractValidator<UpdateHtmlAdCommand>
    {
        public UpdateHtmlAdCommandValidator(IAdvertoDbContext context)
        {
            Include(new UpdateAdCommandBaseValidator(context));

            RuleFor(c => c.Content)
                .ValidHtmlMarkup();
        }
    }
}