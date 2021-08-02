using Application.CQRS.Ads.Queries.AdStorage;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Queries.AdStorage
{
    public class FilterAdsQueryValidator : AbstractValidator<FilterAdsQuery>
    {
        public FilterAdsQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}