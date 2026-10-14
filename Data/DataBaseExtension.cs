using Microsoft.EntityFrameworkCore;

namespace ExportStatistics.Data;

public static class DataBaseMigrations
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbcontext = scope.ServiceProvider.GetRequiredService<EfDbContext>();
        dbcontext.Database.Migrate();
    }
}