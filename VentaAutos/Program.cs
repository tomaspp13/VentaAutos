using Microsoft.EntityFrameworkCore;
using VentaAutos.Models;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.Options;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
        if (builder.Environment.IsDevelopment())
        {
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        }
    });
});

builder.WebHost.UseUrls("http://0.0.0.0:8080");

builder.Services.AddHealthChecks();

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();  // Intenta aplicar las migraciones

    }
    catch (Exception ex)
    {
        // Captura y registra la excepción
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error al aplicar migraciones a la base de datos");
        throw;  // Vuelve a lanzar la excepción para que sea visible en los logs
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
