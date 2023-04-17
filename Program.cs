using InventoryManger.Data;
using InventoryManger.Interfaces;
using InventoryManger.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add must otherwise program will be not goig to those page
builder.Services.AddScoped<IUnit, UnitRepository>();
builder.Services.AddScoped<ICategory, CategoryRepository >();
builder.Services.AddScoped<IBrand, BrandRepo >();
builder.Services.AddScoped<IProductProfile, ProductProfileRepo>();
builder.Services.AddScoped<IProductGroup, ProductGroupRepo >();
builder.Services.AddScoped<IProduct, ProductRepo >();
builder.Services.AddScoped<ISupplier, SupplierRepo >();



var connectionString = builder.Configuration.GetConnectionString("DbConn");
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<InventoryDbContext>();

builder.Services.AddControllersWithViews();


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
app.UseAuthentication();;

app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );
//    endpoints.MapAreaControllerRoute(
//        name: "default",
//        areaName: "Admin",
//        pattern: "{controller=Home}/{action=Index}/{id?}");
//    endpoints.MapRazorPages();
//});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
