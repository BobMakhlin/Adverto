using Application.CQRS.Ads.Queries.AdStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Queries.AdStorage
{
    public class FilterAdsQueryValidator : AbstractValidator<FilterAdsQuery>
    {
        public FilterAdsQueryValidator()
        {
            RuleFor(q => q.CategoryIds)
                .NotNull();

            RuleFor(q => q.TagIds)
                .NotNull();
        }
    }
}