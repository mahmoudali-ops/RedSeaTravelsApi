using Microsoft.EntityFrameworkCore;
using TourSite.APIs.MidleWare;
using TourSite.Repository.Data;
using TourSite.Repository.Data.Contexts;

namespace TourSite.APIs.Helper
{
    public static class ConfigurationMiddleWare 
    {
        public static async Task<WebApplication> UseConfigurationMiddleWare(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;
            


            var context = services.GetRequiredService<TourDbContext>();
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();
                await TourDbContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while migrating the database.");

            }

            app.UseMiddleware<ExceptionMidleWare>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();
            app.UseCors("AllowAngular");
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }

    }
}
