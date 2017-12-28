using IDI.Core.Common;
using IDI.Core.Infrastructure;
using IDI.Digiccy.Domain.Transaction.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IDI.Digiccy.Transaction.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //public Startup(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddEnvironmentVariables();

        //    Configuration = builder.Build();
        //}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Runtime.Initialize(services);
            Runtime.Services.AddSingleton<ITransactionService, TransactionService>();

            #region AllowCORS
            //var urls = Configuration[Constants.Policy.AllowCORSDomain].Split(',');
            var urls = new string[] { "http://localhost:29835" };
            services.AddCors(options => options.AddPolicy(Constants.Policy.AllowCORSDomain, builder => builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
            services.Configure<MvcOptions>(options => { options.Filters.Add(new CorsAuthorizationFilterFactory(Constants.Policy.AllowCORSDomain)); });
            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseCors(Constants.Policy.AllowCORSDomain);
        }
    }
}
