using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TodoApi.Models;

namespace ToDoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        readonly string api = "https://localhost:44351/api";
        readonly string webpage = "http://localhost:4200";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Connection to postgres DB
            services.AddEntityFrameworkNpgsql().AddDbContext<TodoContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("DevConnection")));

            //allow cors
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins(webpage,
                                        api);
                });
            });
            //UseInMemoryDatabase will need to be changed to use an actual database
            //services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //allow origins
            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
