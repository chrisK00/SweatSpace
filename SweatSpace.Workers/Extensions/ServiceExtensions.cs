using Coravel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SweatSpace.Workers.Invocables;

namespace SweatSpace.Workers.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterWorkerServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<SendWeeklyStats>();
            services.AddMailer(config);
        }
    }
}
