namespace WebApi
{
    using Consumers;
    using Contracts;
    using Filters;
    using MassTransit;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;


    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CheckInventoryConsumer>();

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.UseSendFilter(typeof(TokenSendFilter<>), context);
                    cfg.UsePublishFilter(typeof(TokenPublishFilter<>), context);
                    cfg.UseConsumeFilter(typeof(TokenConsumeFilter<>), context);

                    cfg.ConfigureEndpoints(context);
                });

                x.AddRequestClient<CheckInventory>();
            });
            services.AddMassTransitHostedService();

            services.AddScoped<Token>();
            services.AddScoped<TokenActionFilter>();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(TokenActionFilter));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WebApi",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}