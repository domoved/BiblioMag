using BiblioMag.Models;
using BiblioMag.Models.Repositories;
using BiblioMag.Models.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "noBooksFound",
        pattern: "Home/NoBooksFound",
        defaults: new { controller = "Home", action = "NoBooksFound" });

    endpoints.MapFallbackToController("Index", "Home");

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