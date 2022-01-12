using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SweatSpace.Api.Extensions;
using SweatSpace.Api.Middlewares;
using SweatSpace.Core.Extensions;
using SweatSpace.Infrastructure.Extensions;
using SweatSpace.Workers.Extensions;
using SweatSpace.Workers.Invocables;

namespace SweatSpace.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureAppServices(_config);
            services.AddAuth(_config);

            services.RegisterCoreServices();
            services.RegisterInfrastructureServices(_config);
            services.RegisterWorkerServices(_config);

            services.AddControllers();
            services.ConfigureSwagger();          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var serviceProvider = app.ApplicationServices;
            if (env.IsDevelopment())
            {
                serviceProvider.UseScheduler(scheduler =>
                scheduler
                .Schedule<SendWeeklyStats>()
                .EveryThirtySeconds());

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SweatSpace.Api v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
            .WithOrigins("http://localhost:4200"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            serviceProvider.UseScheduler(scheduler =>
               scheduler
               .Schedule<SendWeeklyStats>()
               .Weekly());
        }
    }
}