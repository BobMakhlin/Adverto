using System;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdStorage.Create.Abstractions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Ads.Commands.AdStorage.Create.Realisations
{
    public class CreateVideoAdCommand : CreateAdCommandBase, IRequest<Guid>
    {
        #region Properties

        public override AdType AdType => AdType.VideoAd;

        #endregion

        #region Classes

        public class Handler : CreateAdCommandHandlerBase, IRequestHandler<CreateVideoAdCommand, Guid>
        {
            #region Constructors

            public Handler(IAdvertoDbContext context) : base(context)
            {
            }

            #endregion

            #region IRequestHandler<CreateVideoAdCommand, Guid>

            public async Task<Guid> Handle(CreateVideoAdCommand request, CancellationToken cancellationToken)
                => (await CreateAdAsync(request, cancellationToken)).AdId;

            #endregion
        }

        #endregion
    }
}