

using BEventsWeb.MappingConfig;
using BEventsWeb.Services;
using BEventsWeb.Services.IServices;
using BusinessEvents.DataAccess.Repository.IRepository;
using BusinessEvents.DataAccess.Repository;
using EventBooking.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using EventBooking.DataAccess;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SQLConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

/*builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(connectionString));*/

builder.Services.AddDbContext<ApplicationDbContext>(options =>

options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

// adding mapping config


builder.Services.AddHttpClient<IBEventService, BEventService>();
builder.Services.AddScoped<IBEventService, BEventService>();
builder.Services.AddScoped<IBookingService,BookingService>();
builder.Services.AddScoped<IEmailSender,EmailSender>();

builder.Services.AddScoped<IUnitofWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddRazorPages();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Home}/{id?}");

app.Run();
