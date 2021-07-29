using Application.CQRS.Tags.Queries.TagStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Tags.Queries.TagStorage
{
    public class GetTagByIdQueryValidator : AbstractValidator<GetTagByIdQuery>
    {
        public GetTagByIdQueryValidator()
        {
            RuleFor(q => q.TagId)
                .NotEmpty();
        }
    }
}