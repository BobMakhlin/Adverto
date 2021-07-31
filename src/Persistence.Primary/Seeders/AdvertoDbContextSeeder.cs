using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Primary.Seeders
{
    /// <summary>
    /// A seeder type used to populate <see cref="IAdvertoDbContext"/> with data.
    /// </summary>
    public class AdvertoDbContextSeeder : IDbSeeder<IAdvertoDbContext>
    {
        #region Fields

        private readonly IAdvertoDbContext _context;
        private readonly ILogger<AdvertoDbContextSeeder> _logger;

        private Tag[] _tags;
        private Category[] _categories;
        private Ad[] _ads;
        private AdQueue[] _adQueues;

        #endregion

        #region Constructors

        public AdvertoDbContextSeeder(IAdvertoDbContext context, ILogger<AdvertoDbContextSeeder> logger)
        {
            _context = context;
            _logger = logger;

            InitializePrivateCollections();
        }

        #endregion

        #region IDbSeeder<IAdvertoDbContext>

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            bool isNeedToSeed = await IsNeedToSeedAsync(cancellationToken)
                .ConfigureAwait(false);
            if (!isNeedToSeed)
            {
                _logger.LogInformation($"The {nameof(IAdvertoDbContext)} is already seeded");
                return;
            }

            FillDbSets();

            await _context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            _logger.LogInformation($"{nameof(IAdvertoDbContext)} was seeded successfully");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes private collections like <see cref="_tags"/>, <see cref="_categories"/>, etc.
        /// </summary>
        private void InitializePrivateCollections()
        {
            _tags = new[]
            {
                new Tag {TagId = Guid.NewGuid(), Title = "Portfolio Management"},
                new Tag {TagId = Guid.NewGuid(), Title = "Mutual Fund"},
                new Tag {TagId = Guid.NewGuid(), Title = "Crypto"},
                new Tag {TagId = Guid.NewGuid(), Title = "Stocks"},
                new Tag {TagId = Guid.NewGuid(), Title = "Real Estate"},
                new Tag {TagId = Guid.NewGuid(), Title = "Self Improvement"},
                new Tag {TagId = Guid.NewGuid(), Title = "Motivation"},
                new Tag {TagId = Guid.NewGuid(), Title = "Personal Growth"},
                new Tag {TagId = Guid.NewGuid(), Title = "Gym"},
                new Tag {TagId = Guid.NewGuid(), Title = "Fitness"},
                new Tag {TagId = Guid.NewGuid(), Title = "Doctor"},
                new Tag {TagId = Guid.NewGuid(), Title = "Medicines"},
                new Tag {TagId = Guid.NewGuid(), Title = "Healthy Body"},
                new Tag {TagId = Guid.NewGuid(), Title = "Proper Nutrition"},
                new Tag {TagId = Guid.NewGuid(), Title = "Pepsi"},
                new Tag {TagId = Guid.NewGuid(), Title = "Coca Cola"},
                new Tag {TagId = Guid.NewGuid(), Title = "Fast Food"},
                new Tag {TagId = Guid.NewGuid(), Title = "Pizza"},
                new Tag {TagId = Guid.NewGuid(), Title = "Sushi"},
                new Tag {TagId = Guid.NewGuid(), Title = "BMW"},
                new Tag {TagId = Guid.NewGuid(), Title = "Sport Car"},
                new Tag {TagId = Guid.NewGuid(), Title = "Tesla"},
                new Tag {TagId = Guid.NewGuid(), Title = "Magazine"}
            };

            _categories = new[]
            {
                new Category {CategoryId = Guid.NewGuid(), Title = "Finance"},
                new Category {CategoryId = Guid.NewGuid(), Title = "Education"},
                new Category {CategoryId = Guid.NewGuid(), Title = "Sport"},
                new Category {CategoryId = Guid.NewGuid(), Title = "Health"},
                new Category {CategoryId = Guid.NewGuid(), Title = "Food"},
                new Category {CategoryId = Guid.NewGuid(), Title = "Cars"},
                new Category {CategoryId = Guid.NewGuid(), Title = "Information Technologies"}
            };

            _ads = new[]
            {
                new Ad
                {
                    AdId = Guid.NewGuid(),
                    AdType = AdType.TextAd,
                    Content = "Propel - Portfolio Management Tool",
                    Cost = 0.9,
                    Categories = new List<Category> {_categories[0]},
                    Tags = new List<Tag> {_tags[0], _tags[2], _tags[3], _tags[4]},
                    AdViews = new List<AdView>
                    {
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now}
                    },
                    DisabledAd = null
                },
                new Ad
                {
                    AdId = Guid.NewGuid(),
                    AdType = AdType.TextAd,
                    Content = "XFUND - Crypto Currency Fund",
                    Cost = 1.1,
                    Categories = new List<Category> {_categories[0]},
                    Tags = new List<Tag> {_tags[1], _tags[2]},
                    AdViews = new List<AdView>
                    {
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                    },
                    DisabledAd = null
                },
                new Ad
                {
                    AdId = Guid.NewGuid(),
                    AdType = AdType.TextAd,
                    Content = "Peter Lunch Investing Courses",
                    Cost = 0.88,
                    Categories = new List<Category> {_categories[0], _categories[1]},
                    Tags = new List<Tag> {_tags[0], _tags[2], _tags[3], _tags[4], _tags[5], _tags[7]},
                    AdViews = new List<AdView>
                    {
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now}
                    },
                    DisabledAd = null
                },
                new Ad
                {
                    AdId = Guid.NewGuid(),
                    AdType = AdType.BannerAd,
                    Content =
                        "https://thumbor.forbes.com/thumbor/960x0/https%3A%2F%2Fspecials-images.forbesimg.com%2Fimageserve%2F5f69149a52e9ae5eb5ba6df5%2FGold%2F960x0.jpg%3Ffit%3Dscale",
                    Cost = 1.3,
                    Categories = new List<Category> {_categories[0]},
                    Tags = new List<Tag> {_tags[1]},
                    DisabledAd = new DisabledAd {DisabledAt = DateTime.Now}
                },
                new Ad
                {
                    AdId = Guid.NewGuid(),
                    AdType = AdType.TextAd,
                    Content = "Asus Academy Courses",
                    Cost = 1.99,
                    Categories = new List<Category> {_categories[1], _categories[6]},
                    AdViews = new List<AdView>
                    {
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now}
                    },
                    DisabledAd = null
                },
                new Ad
                {
                    AdId = Guid.NewGuid(),
                    AdType = AdType.BannerAd,
                    Content =
                        "https://cdn.vox-cdn.com/thumbor/2Bcpu3cwfzZJ2gUnhWaiyWFS0Mw=/0x0:8238x5492/1200x800/filters:focal(3460x2087:4778x3405)/cdn.vox-cdn.com/uploads/chorus_image/image/67606043/GettyImages_1132006407.0.jpg",
                    Cost = 1.2,
                    Categories = new List<Category> {_categories[2]},
                    Tags = new List<Tag> {_tags[5], _tags[7], _tags[8], _tags[9], _tags[13], _tags[14]},
                    AdViews = new List<AdView>
                    {
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now}
                    },
                    DisabledAd = null
                },
                new Ad
                {
                    AdId = Guid.NewGuid(),
                    AdType = AdType.TextAd,
                    Content = "Luxury Clinic \"LUX\"",
                    Cost = 2.97,
                    Categories = new List<Category> {_categories[3]},
                    Tags = new List<Tag> {_tags[10], _tags[11], _tags[12], _tags[13]},
                    AdViews = new List<AdView>
                    {
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now}
                    },
                    DisabledAd = null
                },
                new Ad
                {
                    AdId = Guid.NewGuid(),
                    AdType = AdType.TextAd,
                    Content = "McDonald's near \"X\" Square Opened",
                    Cost = 0.1,
                    Categories = new List<Category> {_categories[4]},
                    Tags = new List<Tag> {_tags[13], _tags[14], _tags[15], _tags[16]},
                    AdViews = new List<AdView>
                    {
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now}
                    },
                    DisabledAd = null
                },
                new Ad
                {
                    AdId = Guid.NewGuid(),
                    AdType = AdType.TextAd,
                    Content = "Sushi X - free delivery",
                    Cost = 1.79,
                    Categories = new List<Category> {_categories[4]},
                    Tags = new List<Tag> {_tags[13], _tags[14], _tags[15], _tags[18]},
                    AdViews = new List<AdView>
                    {
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now}
                    },
                    DisabledAd = null
                },
                new Ad
                {
                    AdId = Guid.NewGuid(),
                    AdType = AdType.HtmlAd,
                    Content =
                        @"
                        <!DOCTYPE html>
                        <html lang=""en"">
                        <head>
                            <meta charset=""UTF-8"">
                            <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                            <title>Document</title>

                            <style>
                                .card {
                                    box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
                                    transition: 0.3s;
                                    border-radius: 5px; /* 5px rounded corners */
                                }

                                .card:hover {
                                    box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2);
                                }

                                .container {
                                    padding: 2px 16px;
                                }
                        </head>
                        <body>
                        <div class=""card"">
                            <div class=""container"">
                                <h4><b>BMW x5</b></h4>
                                <p>Buy now!</p>
                            </div>
                        </div>
                        </body>
                        </html>                    
                    ",
                    Cost = 7.99,
                    Categories = new List<Category> {_categories[5]},
                    Tags = new List<Tag> {_tags[19]},
                    AdViews = new List<AdView>
                    {
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now}
                    },
                    DisabledAd = null
                },
                new Ad
                {
                    AdId = Guid.NewGuid(),
                    AdType = AdType.BannerAd,
                    Content = "https://images.drive.ru/i/0/5ffd645ab8a90767f35912a5.jpg",
                    Cost = 1.99,
                    Categories = new List<Category> {_categories[5]},
                    Tags = new List<Tag> {_tags[21]},
                    AdViews = new List<AdView>
                    {
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now},
                        new AdView {AdViewId = Guid.NewGuid(), ViewedAt = DateTime.Now}
                    },
                    DisabledAd = null
                },
            };

            _adQueues = new[]
            {
                new AdQueue {AdQueueId = Guid.NewGuid(), CurrentAdIndex = 0}
            };
        }

        /// <summary>
        /// Fills the DbSets of the <see cref="_context"/>.
        /// </summary>
        private void FillDbSets()
        {
            _context.Tags.AddRange(_tags);
            _context.Categories.AddRange(_categories);
            _context.Ads.AddRange(_ads);
            _context.AdQueues.AddRange(_adQueues);
        }

        /// <summary>
        /// Checks is need to seed the database with data.
        /// </summary>
        private async Task<bool> IsNeedToSeedAsync(CancellationToken cancellationToken)
        {
            bool adsDbSetEmpty = !await _context.Ads.AnyAsync(cancellationToken)
                .ConfigureAwait(false);
            return adsDbSetEmpty;
        }

        #endregion
    }
}