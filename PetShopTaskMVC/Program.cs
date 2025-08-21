using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetShopTaskMVC.DataContext;
using PetShopTaskMVC.DataContext.Entities;
using System;
using System.Reflection;

namespace PetShopTaskMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default")
                ////eger migrationlar basqa assemblyde olsa
                //, options =>
                //{
                //    options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                //});  
                );
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 2;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            ////eger login account/loginde deyilse burda path gosterilir.
            ////login olmadan gire bilmeyende hansi path-e gondermesi ucun
            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/path/accounts/testLogin";
            //});

            //data initializer classi qosmaq asan yol
            //cunki data initalizer de appdbcontext de servicelere elave olunub.
            //onu goturende appdbcontext de goturulur eyni yerde olduqlarina gore
            builder.Services.AddScoped<DataInitializer>();

            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                var dataInitializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
                dataInitializer.SeedData();
            }



            ////data initalizer classi qosmaq uzun yol
            //using(var scope = app.Services.CreateScope())
            //{
            //    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    var dataInitializer = new DataInitializer(appDbContext);
            //    dataInitializer.SeedData();
            //}



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
