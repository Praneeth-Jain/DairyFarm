using DairyFarm.Data.DBContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace DairyFarm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                string ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
                options.UseSqlServer(ConnectionString);
            }
          );

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/OwnerLogin"; 
                    options.AccessDeniedPath = "/Subscription/Index";
                    options.LogoutPath = "/OwnerLogout";
                });



            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("FreeTier", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == "SubscriptionType" &&
                            
                        (c.Value == "free" || c.Value == "standard" || c.Value == "premium"))));

                options.AddPolicy("StandardTier", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == "SubscriptionType" &&
                                                    (c.Value == "standard" || c.Value == "premium"))));

                options.AddPolicy("PremiumTier", policy =>
                    policy.RequireClaim("SubscriptionType", "premium"));
            });



            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

           


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
