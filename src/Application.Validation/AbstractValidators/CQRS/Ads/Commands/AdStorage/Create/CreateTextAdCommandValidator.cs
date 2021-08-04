using Application.CQRS.Ads.Commands.AdStorage.Create.Realisations;
using Application.Persistence.Interfaces;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage.Create
{
    public class CreateTextAdCommandValidator : AbstractValidator<CreateTextAdCommand>
    {
        public CreateTextAdCommandValidator(IAdvertoDbContext context)
        {
            Include(new CreateAdCommandBaseValidator(context));
        }
    }
}