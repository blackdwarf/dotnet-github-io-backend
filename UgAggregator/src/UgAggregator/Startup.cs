using System;
using System.Collections.Generic;
using System.Linq;
// using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Framework.Runtime;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using UgAggregator.Channels;
using UgAggregator.Contracts;
using UgAggregator.Models;
using UgAggregator.Services;
using Microsoft.AspNetCore.Cors;

namespace UgAggregator
{
    public class Startup
    {
        //public IConfiguration Configuration { get; set; }
        private CommunityMegaphoneConfiguration _cmConf { get; set; }
        private MeetupConfiguration _mConf { get; set; }
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            //var configuration = new Configuration()
            //    .AddJsonFile("config.json");

            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("./config.json");

            //Configuration = configuration;
            SetConfiguration(configBuilder.Build());
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddJsonFormatters();

            /* CORS configuration */
            services.AddCors(options => {
                options.AddPolicy("ugAggregatorAll",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .WithMethods("GET")
                        .AllowCredentials()
                        );
            });
            services.AddSingleton<CommunityMegaphoneConfiguration>(_cmConf);
            services.AddSingleton<MeetupConfiguration>(_mConf);
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IChannelStore, DefaultChannelStore>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // Configure the HTTP request pipeline.
            // app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();

            // Add CORS policy defined above
            app.UseCors("ugAggregatorAll");

            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
        }

        private void SetConfiguration(IConfiguration config) {
            _cmConf = new CommunityMegaphoneConfiguration {
                Url = config["CommunityMegaphone:Url"]
            };
            _mConf = new MeetupConfiguration {
                Url = config["Meetup:Url"],
                ApiKey = config["Meetup:ApiKey"]
            };

        }
    }
}
