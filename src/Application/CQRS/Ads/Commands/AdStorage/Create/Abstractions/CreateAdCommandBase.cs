using System;
using Domain.Primary.Entities;

namespace Application.CQRS.Ads.Commands.AdStorage.Create.Abstractions
{
    /// <summary>
    /// The base type for all ad-creation commands.
    /// It specifies a set of properties, that each ad-creation command has.
    /// </summary>
    public abstract class CreateAdCommandBase
    {
        public abstract AdType AdType { get; }
        public string Content { get; set; }
        public double Cost { get; set; }
        public Guid[] CategoryIds { get; set; }
        public Guid[] TagIds { get; set; }
    }
}