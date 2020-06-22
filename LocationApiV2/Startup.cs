using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocationApiV2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LocationApiV2
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
            services.AddDbContext<locationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("locationDB")));
            services.AddControllers();
            services.AddSwaggerDocument(config =>
                {
                    config.PostProcess = document =>
                    {
                        document.Info.Version = "V1";
                        document.Info.Title = "Location Api";
                        document.Info.Description = " simple API to return locations around the world";
                        document.Info.TermsOfService = "None";
                        document.Info.Contact = new NSwag.OpenApiContact
                        {
                            Name = "Carlos Encine",
                            Email = "contato@carlosencine.com",
                            Url = "https//carlosencine.com"
                        };
                    };
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseOpenApi();            

            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
