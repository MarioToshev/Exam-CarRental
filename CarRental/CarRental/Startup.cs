using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using CarRental.Data;
using CarRental.Data.Identity;
using CarRental.Service;
using RentFormRental.Service;
using RentFormRental.Service.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Service.Interfaces;
using CarRental.Services;

namespace CarRental
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<RentalContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("RentalContextConnection"), b => b.MigrationsAssembly("CarRental"));
            });
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICarService,
                CarService>();
            services.AddTransient<IRentFormService, RentFormService>();

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<RentalContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddSingleton<ICloudinaryService>(instance => new CloudinaryService(
               this.Configuration["Cloudinary:CloudName"],
               this.Configuration["Cloudinary:ApiKey"],
               this.Configuration["Cloudinary:ApiSecret"]));
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
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Login}/{id?}");
            });
        }
    }
}
