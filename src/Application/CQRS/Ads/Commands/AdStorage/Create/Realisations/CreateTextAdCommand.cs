using System;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdStorage.Create.Abstractions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Ads.Commands.AdStorage.Create.Realisations
{
    public class CreateTextAdCommand : CreateAdCommandBase, IRequest<Guid>
    {
        #region Properties

        public override AdType AdType => AdType.TextAd;

        #endregion

        #region Classes

        public class Handler : CreateAdCommandHandlerBase, IRequestHandler<CreateTextAdCommand, Guid>
        {
            #region Constructors

            public Handler(IAdvertoDbContext context) : base(context)
            {
            }

            #endregion

            #region IRequestHandler<CreateTextAdCommand, Guid>

            public async Task<Guid> Handle(CreateTextAdCommand request, CancellationToken cancellationToken)
                => (await CreateAdAsync(request, cancellationToken)).AdId;

            #endregion
        }

        #endregion
    }
}