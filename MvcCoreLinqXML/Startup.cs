using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCoreLinqXML.Providers;
using MvcCoreLinqXML.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreLinqXML
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
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddAuthorization(options =>
            {
                //VAMOS A CREAR UNA POLITICA DE ACCESO POR ROLES
                //options.AddPolicy("PermisosElevados", policy => policy.RequireRole("CARDIOLOGIA", "DIAGNOSTICO"));
                //options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
                //options.AddPolicy("SoloDoctoresRicos", policy => policy.Requirements.Add(new OverSalarioRequirement()));

            });

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme, config =>
                {
                    config.AccessDeniedPath = "/Manage/ErrorAcceso";
                });

            services.AddTransient<RepositoryJoyerias>();
            services.AddTransient<RepositoryCliente>();
            services.AddTransient<RepositoryPeliculas>();
            services.AddTransient<RepositoryCurso>();

            services.AddControllersWithViews(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            PathProvider.Initialize(env);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //   name: "Enfermo",
                //   template: "{controller=Hospital}/{action=EliminarEnfermo}/{inscripcion?}"
                //   );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );

            });
        }
    }
}
