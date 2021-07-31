﻿using Application.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Primary.DataAccess;
using Persistence.Primary.Options;
using Persistence.Primary.Seeders;

namespace Persistence.Primary
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers persistence-layer services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            AddPrimaryDbContext(services, configuration);
            
            services.AddScoped<IAdvertoDbContext>(provider => provider.GetService<AdvertoDbContext>());
            services.AddScoped<IDbSeeder<IAdvertoDbContext>, AdvertoDbContextSeeder>();

            return services;
        }

        /// <summary>
        /// Registers primary <see cref="DbContext"/> inside of <paramref name="services"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void AddPrimaryDbContext(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(PersistenceOptions.PrimaryDatabase);

            services.AddDbContext<AdvertoDbContext>
            (
                options => options.UseSqlServer(connectionString)
            );
        }
    }
}