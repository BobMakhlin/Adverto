using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdStorage.Update.Abstractions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Ads.Commands.AdStorage.Update.Realisations
{
    public class UpdateVideoAdCommand : UpdateAdCommandBase, IRequest
    {
        #region Properties

        public override AdType AdType => AdType.VideoAd;

        #endregion

        #region Classes

        public class Handler : UpdateAdCommandHandlerBase, IRequestHandler<UpdateVideoAdCommand>
        {
            #region Constructors

            public Handler(IAdvertoDbContext context) : base(context)
            {
            }

            #endregion

            #region IRequestHandler<UpdateAdCommand>

            public async Task<Unit> Handle(UpdateVideoAdCommand request, CancellationToken cancellationToken)
            {
                await UpdateAdAsync(request, cancellationToken).ConfigureAwait(false);
                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}