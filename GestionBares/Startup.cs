﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionBares.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GestionBares.Models;
using GestionBares.Utils;
using GestionBares.Models.AlmacenModels;

namespace GestionBares
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AdministradorDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AdministradorConnection")));
            services.AddDbContext<AuditorDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AuditorConnection")));
            services.AddDbContext<AmasBDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AmasBConnection")));
            services.AddDbContext<EconomiaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("EconomiaConnection")));
            services.AddDbContext<DependienteDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DependienteConnection")));
            services.AddDbContext<AlmacenDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AlmacenConnection")));

            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient<UsuarioSignInManager>();

            services.AddTransient<DbContext, ApplicationDbContext>();
            services.AddTransient<AdministradorDbContext>();
            services.AddTransient<AuditorDbContext>();
            services.AddTransient<AmasBDbContext>();
            services.AddTransient<DependienteDbContext>();
            services.AddTransient<EconomiaDbContext>();

            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/Account/Login");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
