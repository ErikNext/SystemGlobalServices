using LazyCache;
using Microsoft.OpenApi.Models;
using SystemGlobalServices.Core.Common;
using SystemGlobalServices.Integrations.CurrenciesApi.Contracts;
using SystemGlobalServices.Integrations.CurrenciesApi.Services;
using SystemGlobalServices.Services.Currencies;
using SystemGlobalServices.Services.Currencies.Services;

namespace SystemGlobalServices.Core.App
{
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
            services.AddSingleton<ICurrenciesService, CurrenciesService>();
            
            services.AddSingleton<CurrenciesLoaderService>();

            services.AddSingleton<ICurrenciesLoaderService>(x =>
                new CachedCurrenciesLoaderService(x.GetRequiredService<CurrenciesLoaderService>(),
                    x.GetRequiredService<IAppCache>()));

            ApiHelper.InitializeClient();

            services.AddLazyCache();

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Currencies API",
                    
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
