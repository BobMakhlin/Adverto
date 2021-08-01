using System.Reflection;
using Application.Common.Behaviours;
using Application.Queues.Interfaces;
using Application.Queues.Realisations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers application-layer services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(currentAssembly);
            services.AddMediatR(currentAssembly);
            AddMediatorPipelineBehaviours(services);
            AddQueueEnumerators(services);
            
            return services;
        }

        /// <summary>
        /// Registers MediatR pipeline behaviours on the given <paramref name="services"/>.
        /// </summary>
        /// <param name="services"></param>
        private static void AddMediatorPipelineBehaviours(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionLoggingBehaviour<,>));
        }

        /// <summary>
        /// Registers queue enumerators (e.g. <see cref="AdQueueDatabaseInfiniteEnumerator"/>)
        /// on the specified <paramref name="services"/>.
        /// </summary>
        private static void AddQueueEnumerators(IServiceCollection services)
        {
            services.AddScoped<IAdQueueEnumerator, AdQueueDatabaseInfiniteEnumerator>();
        }
    }
}