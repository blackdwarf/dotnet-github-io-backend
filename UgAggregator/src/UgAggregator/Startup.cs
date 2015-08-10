using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Runtime;
using UgAggregator.Channels;
using UgAggregator.Contracts;
using UgAggregator.Models;
using UgAggregator.Services;

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

            var configBuilder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json");

            //Configuration = configuration;
            SetConfiguration(configBuilder.Build());
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            /* CORS configuration */
            services.AddCors();
            services.ConfigureCors(options => {
                options.AddPolicy("ugAggregatorAll",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .WithMethods("GET")
                        .AllowCredentials()
                        );
            });
            //var debug = Configuration.GetSubKey("CommunityMegaphone");
            //services.Configure<CommunityMegaphoneConfiguration>(Configuration.GetSubKey("CommunityMegaphone"));
            //services.Configure<MeetupConfiguration>(Configuration.Get("Meetup"));
            services.AddInstance<CommunityMegaphoneConfiguration>(_cmConf);
            services.AddInstance<MeetupConfiguration>(_mConf);
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IChannelStore, DefaultChannelStore>();
            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();

            // Add CORS policy defined above
            app.UseCors("ugAggregatorAll");

            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
        }

        private void SetConfiguration(IConfiguration config) {
            _cmConf = new CommunityMegaphoneConfiguration {
                Url = config.Get("CommunityMegaphone:Url")
            };
            _mConf = new MeetupConfiguration {
                Url = config.Get("Meetup:Url"),
                ApiKey = config.Get("Meetup:ApiKey")
            };

        }
    }
}
