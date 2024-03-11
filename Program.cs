using CupMetric.Data;
using CupMetric.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("SqlServerUrl");

builder.Services.AddTransient<RepositoryUsers>();
builder.Services.AddTransient<RepositoryReceta>();
builder.Services.AddTransient<RepositoryUtensilios>();
builder.Services.AddTransient<RepositoryIngredientes>();
builder.Services.AddSession();

builder.Services.AddDbContext<CupMetricContext>
    (options => options.UseSqlServer(connectionString));

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
