using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetworkOfShops.Data;
using NetworkOfShops.Models;
using NetworkOfShops.Repositories;
using NetworkOfShops.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    options.UseInMemoryDatabase(databaseName: "Test");
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AplicationDbContext>();
builder.Services.AddScoped<AplicationDbInitializer>();
builder.Services.AddScoped<IGenericRepository<Product>,GenericRepository<Product>>();
builder.Services.AddScoped<IGenericRepository<Shop>, GenericRepository<Shop>>();
builder.Services.AddScoped<IAuthorizationInitializer, AuthorizationInitializer>();

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

SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<AplicationDbInitializer>();
        var service2 = scope.ServiceProvider.GetService<IAuthorizationInitializer>();
        service.Seed();
        service2.GenerateAdminAndRoles().Wait();
    }
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
