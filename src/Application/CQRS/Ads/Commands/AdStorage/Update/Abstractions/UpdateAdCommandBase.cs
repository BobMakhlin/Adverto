using System;
using Domain.Primary.Entities;

namespace Application.CQRS.Ads.Commands.AdStorage.Update.Abstractions
{
    /// <summary>
    /// The base type for all ad-update commands.
    /// It specifies a set of properties, that each ad-update command has.
    /// </summary>
    public abstract class UpdateAdCommandBase
    {
        public Guid AdId { get; set; }
        public abstract AdType AdType { get; }
        public string Content { get; set; }
        public double Cost { get; set; }
    }
}