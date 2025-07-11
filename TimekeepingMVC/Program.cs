using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TimekeepingMVC.Services;
using TimekeepingMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5000, listenOptions => { }); 
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps(); 
    });
});

builder.Services.AddControllersWithViews();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value));

builder.Services.AddScoped<ITimekeepingService, TimekeepingService>();
builder.Services.AddScoped<ISummaryService, SummaryService>();

var app = builder.Build();

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
