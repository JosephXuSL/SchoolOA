using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SchoolOA.Context;
using SchoolOA.Repositories;
using SchoolOA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SchoolOA
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
            AddSchoolContext(services);
            RegisterServices(services);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddControllers()
                .AddNewtonsoftJson(cfg=>cfg.SerializerSettings.ReferenceLoopHandling= ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SchoolOA", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolOA v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }        

        protected void AddSchoolContext(IServiceCollection services)
        {
            services.AddDbContext<SchoolContext>(options =>
                {
                    options.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }))
                    .UseSqlServer("Data Source = (localdb)\\ProjectsV13; Initial Catalog = SchoolOA; Integrated Security = True; Connect Timeout = 30; MultipleActiveResultSets = true;");
                });
        }

        protected void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SchoolRepository>();
            services.AddScoped<PictureService>();
        }
    }
}
