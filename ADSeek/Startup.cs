using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSeek.Application.Interfaces.Mappers;
using ADSeek.Application.Interfaces.Services;
using ADSeek.Domain.Interfaces;
using ADSeek.Infrastructure.Mappers;
using ADSeek.Infrastructure.Services;
using ADSeek.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Novell.Directory.Ldap;

namespace ADSeek
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
            services.AddControllersWithViews();

            var settings = new ActiveDirectorySettings(Configuration);
            services.AddSingleton<IActiveDirectorySettings, ActiveDirectorySettings.ActiveDirectoryConnectionSettings>(_ => settings.ConnectionSettings);

            services.AddTransient<IActiveDirectoryService, ActiveDirectoryService>();
            services.AddTransient<IActiveDirectoryConverter<LdapAttributeSet>, ActiveDirectoryConverter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/");
            });
        }
    }
}