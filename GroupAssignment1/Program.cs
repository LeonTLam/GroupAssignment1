using GroupAssignment1.DAL;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Identity;
using GroupAssignment1.Areas.Identity.Data;

namespace GroupAssignment1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ItemDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ItemDbContextConnection' not found.");
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddDbContext<HousingDbContext>(options =>
            {
                options.UseSqlite(
                    builder.Configuration["ConnectionStrings:ItemDbContextConnection"]);
            });

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HousingDbContext>();

            builder.Services.AddScoped<IHousingRepository, HousingRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

            builder.Services.AddRazorPages();
            builder.Services.AddSession();


            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

            loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) &&
                e.Level == LogEventLevel.Information &&
                e.MessageTemplate.Text.Contains("Executed DbCommand"));

            var logger = loggerConfiguration.CreateLogger();
            builder.Logging.AddSerilog(logger);

            var app = builder.Build();

            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DBInit.Seed(app);
            }


            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();

            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "User" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string email = "admin@admin.com";
                string password = "Admin123!";

                if(await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new ApplicationUser();
                    user.UserName = email;
                    user.Email = email;

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            
            app.Run();
        }
    }
}
