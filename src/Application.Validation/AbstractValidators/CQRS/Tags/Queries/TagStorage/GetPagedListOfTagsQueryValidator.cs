using Application.CQRS.Tags.Queries.TagStorage;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Tags.Queries.TagStorage
{
    public class GetPagedListOfTagsQueryValidator : AbstractValidator<GetPagedListOfTagsQuery>
    {
        public GetPagedListOfTagsQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}