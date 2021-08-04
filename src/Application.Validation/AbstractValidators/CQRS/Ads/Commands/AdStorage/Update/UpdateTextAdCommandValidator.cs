using Application.CQRS.Ads.Commands.AdStorage.Update.Realisations;
using Application.Persistence.Interfaces;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage.Update
{
    public class UpdateTextAdCommandValidator : AbstractValidator<UpdateTextAdCommand>
    {
        public UpdateTextAdCommandValidator(IAdvertoDbContext context)
        {
            Include(new UpdateAdCommandBaseValidator(context));
        }
    }
}