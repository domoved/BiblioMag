using BiblioMag.Models;
using BiblioMag.Models.Repositories;
using BiblioMag.Models.Services;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "XDD";
});

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=library;Username=postgres;Password=xdd");
});
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IDownloadService, DownloadService>();
builder.Services.AddScoped<IReadingService, ReadingService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IReadingSessionRepository, ReadingSessionRepository>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

var app = builder.Build();

app.Use((context, next) =>
{
    string path = context.Request.Path.Value;

    if (path != null && path.ToLower().Contains("/book/addbook"))
    {
        var antiforgery = context.RequestServices.GetRequiredService<IAntiforgery>();
        var tokens = antiforgery.GetAndStoreTokens(context);
        context.Response.Cookies.Append("XDD", tokens.RequestToken,
            new CookieOptions() { HttpOnly = false });
    }

    return next(context);
});
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    dbContext.Database.Migrate();
}

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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapFallbackToController("Index", "Home");

    endpoints.MapControllerRoute(
        name: "add",
        pattern: "Book/Add",
        defaults: new { controller = "Book", action = "Add" });

    endpoints.MapControllerRoute(
        name: "noBooksFound",
        pattern: "Home/NoBooksFound",
        defaults: new { controller = "Home", action = "NoBooksFound" });

    endpoints.MapControllerRoute(
        name: "bookIndex",
        pattern: "Book",
        defaults: new { controller = "Book", action = "Index" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    dbContext.Database.Migrate();
}

app.Run();