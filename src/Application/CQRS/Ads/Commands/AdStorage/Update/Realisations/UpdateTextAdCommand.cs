using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdStorage.Update.Abstractions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Ads.Commands.AdStorage.Update.Realisations
{
    public class UpdateTextAdCommand : UpdateAdCommandBase, IRequest
    {
        #region Properties

        public override AdType AdType => AdType.TextAd;

        #endregion

        #region Classes

        public class Handler : UpdateAdCommandHandlerBase, IRequestHandler<UpdateTextAdCommand>
        {
            #region Constructors

            public Handler(IAdvertoDbContext context) : base(context)
            {
            }

            #endregion

            #region IRequestHandler<UpdateAdCommand>

            public async Task<Unit> Handle(UpdateTextAdCommand request, CancellationToken cancellationToken)
            {
                await UpdateAdAsync(request, cancellationToken).ConfigureAwait(false);
                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}