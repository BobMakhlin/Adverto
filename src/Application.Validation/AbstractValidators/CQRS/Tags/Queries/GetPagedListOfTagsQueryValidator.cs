using Application.CQRS.Tags.Queries;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Tags.Queries
{
    public class GetPagedListOfTagsQueryValidator : AbstractValidator<GetPagedListOfTagsQuery>
    {
        public GetPagedListOfTagsQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}