using DinkToPdf;
using DinkToPdf.Contracts;
using FreshFruit.Models;
using FreshFruit.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.Loader;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();

// Load native library libwkhtmltox.dll từ wwwroot
var context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IInvoiceServices, InvoiceServices>();

// Razor view renderer
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

// DinkToPdf
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// DB context
builder.Services.AddDbContext<FreshFruitDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("FreshFruitConnection")));

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// === ROUTING ===
app.MapControllerRoute(
	name: "blog_slug",
	pattern: "bai-viet/{id:int}/{slug}",
	defaults: new { controller = "Blog", action = "Details" });

app.MapControllerRoute(
	name: "admin",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
