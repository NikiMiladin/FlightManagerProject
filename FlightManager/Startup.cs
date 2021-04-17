using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.MappingProfile;
using Data;
using Data.Repositories;
using Data.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FlightManager
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
            services.AddDbContext<FlightDb>();
            services.AddControllersWithViews();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IPassengerRepository, PassengerRepository>();
            services.AddIdentity<ApplicationUser,IdentityRole>()
                    .AddEntityFrameworkStores<FlightDb>()
                    .AddDefaultTokenProviders();
        //    services.AddAuthorization(options =>
        //    {
        //        options.FallbackPolicy = new AuthorizationPolicyBuilder()
        //            .RequireAuthenticatedUser()
        //            .Build();
        //    });
            services.ConfigureApplicationCookie(options =>
            {    
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout"; 
                options.AccessDeniedPath = "/Account/AccessDenied"; 
            });
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           
           // services.AddTransient<FlightDbContextSeedData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
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

            app.UseAuthentication();
            app.UseAuthorization();

            Data.FlightDbContextSeedData.SeedAdmin(roleManager,userManager,"ADMINISTRATOR","P@ssw0rd");
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
