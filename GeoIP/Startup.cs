using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GeoIP.Services;
using GeoIP.Middleware;

namespace GeoIP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IMaxmindService>(provider =>
            {
                var service = new MaxmindDBService();

                var citiesDB = Configuration["GEO_CITY_DB"];
                var countriesDB = Configuration["GEO_COUNTRY_DB"];
                var asnDB = Configuration["GEO_ASN_DB"];

                service.InitCitiesDB(citiesDB);
                service.InitCountriesDB(countriesDB);
                service.InitASNDB(asnDB);

                return service;
            });

            services
                .Configure<RouteOptions>(r => r.LowercaseUrls = true)
                .AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandleMiddleware>();
            app.UseMvc();
        }
    }
}
