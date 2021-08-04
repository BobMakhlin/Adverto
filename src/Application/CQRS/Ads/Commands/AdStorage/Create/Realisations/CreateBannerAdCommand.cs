using System;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdStorage.Create.Abstractions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Ads.Commands.AdStorage.Create.Realisations
{
    public class CreateBannerAdCommand : CreateAdCommandBase, IRequest<Guid>
    {
        #region Properties

        public override AdType AdType => AdType.BannerAd;

        #endregion

        #region Classes

        public class Handler : CreateAdCommandHandlerBase, IRequestHandler<CreateBannerAdCommand, Guid>
        {
            #region Constructors

            public Handler(IAdvertoDbContext context) : base(context)
            {
            }

            #endregion

            #region IRequestHandler<CreateBannerAdCommand, Guid>

            public async Task<Guid> Handle(CreateBannerAdCommand request, CancellationToken cancellationToken)
                => (await CreateAdAsync(request, cancellationToken)).AdId;

            #endregion
        }

        #endregion
    }
}