using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WalletServiceAPI.Data;
using WalletServiceAPI.Entities;
using WalletServiceAPI.Services;
using WalletServiceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace WalletServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddHttpClient(); // Register HttpClient for dependency injection
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ICurrencyConversionService, CurrencyConversionService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wallet Service API", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Use default error page in development
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Use default exception handler in production
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wallet Service API v1");
                c.RoutePrefix = "swagger";  // Serve Swagger UI at the root
            });

            app.MapControllers();

            app.Run();
        }
    }
}
