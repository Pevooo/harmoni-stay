using Microsoft.EntityFrameworkCore;

namespace MainProject
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromDays(30));
            builder.Services.AddDbContext<Models.Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Hosted Db")));

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

            app.UseStatusCodePagesWithReExecute("/Error");

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}