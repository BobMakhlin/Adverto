﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Categories.Commands.CategoryStorage
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        #region Properties

        public string Title { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<CreateCategoryCommand, Guid>
        {
            #region Fields

            private readonly IAdvertoDbContext _context;

            #endregion

            #region Constructors

            public Handler(IAdvertoDbContext context)
            {
                _context = context;
            }

            #endregion

            #region IRequestHandler<CreateCategoryCommand, Guid>

            public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                Category category = ConvertToCategory(request);

                await CreateCategoryAsync(category, cancellationToken)
                    .ConfigureAwait(false);

                return category.CategoryId;
            }

            #endregion
            
            #region Methods

            /// <summary>
            /// Creates an object of type <see cref="Category"/> based on the given <paramref name="command"/>.
            /// </summary>
            /// <param name="command"></param>
            /// <returns>The created object of type <see cref="Category"/></returns>
            private Category ConvertToCategory(CreateCategoryCommand command)
            {
                return new Category
                {
                    Title = command.Title
                };
            }

            private async Task CreateCategoryAsync(Category category, CancellationToken cancellationToken)
            {
                _context.Categories.Add(category);

                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}