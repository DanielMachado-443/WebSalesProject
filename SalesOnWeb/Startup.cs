﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesOnWeb.Models;
using SalesOnWeb.Data;

namespace SalesOnWeb {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<SalesOnWebContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("SalesOnWebContext"), builder =>
            builder.MigrationsAssembly("SalesOnWeb")));

            services.AddScoped<SeedingService>(); // <<< REGISTERED IN THE DEPENDENCY INJECTION SYSTEM OF THIS APPLICATION
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService) { // MATCHS WITH THE METHOD CALLED ABOVE
            if (env.IsDevelopment()) { // <<< developing profile
                app.UseDeveloperExceptionPage();
                seedingService.Seed();
            }
            else {
                app.UseExceptionHandler("/Home/Error"); // <<< production profile
                app.UseHsts();                
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
