using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AI.TextAnalysis.Interfaces;
using Application.Common.Exceptions;
using Application.CQRS.Tags.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using Domain.Primary.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ads.Commands.AdTag
{
    public class AttachTagsToTextAdByExtractingContentKeywordsCommand : IRequest<List<TagDto>>
    {
        #region Properties

        public Guid AdId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<AttachTagsToTextAdByExtractingContentKeywordsCommand, List<TagDto>>
        {
            #region Fields

            private readonly IAdvertoDbContext _context;
            private readonly IKeywordsExtractorService _keywordsExtractorService;
            private readonly IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IAdvertoDbContext context, IKeywordsExtractorService keywordsExtractorService,
                IMapper mapper)
            {
                _context = context;
                _keywordsExtractorService = keywordsExtractorService;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<AttachTagsToTextAdByExtractingContentKeywordsCommand, List<TagDto>>

            public async Task<List<TagDto>> Handle(AttachTagsToTextAdByExtractingContentKeywordsCommand request,
                CancellationToken cancellationToken)
            {
                Ad ad = await _context.Ads
                            .Include(a => a.Tags)
                            .Where(a => a.AdId == request.AdId)
                            .SingleOrDefaultAsync(cancellationToken)
                            .ConfigureAwait(false)
                        ?? throw new NotFoundException(nameof(Ad), request.AdId);

                List<string> contentKeywords =
                    (
                        await _keywordsExtractorService
                            .GetKeywordsFromStringAsync(ad.Content, cancellationToken)
                            .ConfigureAwait(false)
                    )
                    .ToList();

                await CreateTagsAsync(contentKeywords, cancellationToken)
                    .ConfigureAwait(false);
                List<Tag> tags = await FindTagsByTitlesAsync(contentKeywords)
                    .ConfigureAwait(false);
                List<Tag> attachedTags = await AttachTagsToAdAsync(ad, tags, cancellationToken)
                    .ConfigureAwait(false);

                return _mapper.Map<List<TagDto>>(attachedTags);
            }

            #endregion

            #region Methods

            /// <summary>
            /// Creates tags with the specified <paramref name="titles"/>,
            /// excluding the titles already in the tags-table.
            /// </summary>
            private async Task CreateTagsAsync(IEnumerable<string> titles, CancellationToken cancellationToken)
            {
                List<string> titlesOfAlreadyExistingTags = await _context.Tags
                    .Where(tag => titles.Contains(tag.Title))
                    .Select(tag => tag.Title)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                IEnumerable<string> titlesOfTagsToBeCreated = titles.Except(titlesOfAlreadyExistingTags);
                IEnumerable<Tag> tagsToBeCreated = titlesOfTagsToBeCreated
                    .Select(title => new Tag {Title = title});

                _context.Tags.AddRange(tagsToBeCreated);

                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            /// <summary>
            /// Attaches the specified <paramref name="tags"/> to the specified <paramref name="ad"/>,
            /// excluding the tags already attached to the <paramref name="ad"/>.
            /// </summary>
            /// <returns>
            /// The collection of attached tags.
            /// </returns>
            private async Task<List<Tag>> AttachTagsToAdAsync(Ad ad, List<Tag> tags,
                CancellationToken cancellationToken)
            {
                List<Tag> alreadyAttachedTags = ad.Tags
                    .Where(tags.Contains)
                    .ToList();

                List<Tag> tagsToBeAttached = tags
                    .Except(alreadyAttachedTags)
                    .ToList();
                ad.Tags.AddRange(tagsToBeAttached);

                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return tagsToBeAttached;
            }

            /// <summary>
            /// Returns the list of <see cref="Tag"/>-entities with the specified <paramref name="titles"/>.
            /// </summary>
            private async Task<List<Tag>> FindTagsByTitlesAsync(IEnumerable<string> titles)
            {
                return await _context.Tags
                    .Where(tag => titles.Contains(tag.Title))
                    .ToListAsync()
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}