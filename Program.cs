using Microsoft.EntityFrameworkCore;
using PBL3_DUTLibrary.Data;
using PBL3_DUTLibrary.Models;
using PBL3_DUTLibrary.Interfaces;
using PBL3_DUTLibrary.Helper;
using PBL3_DUTLibrary.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using PBL3_DUTLibrary.Repository;
using PBL3_DUTLibrary.Class;
using PBL3_DUTLibrary.Interface;
using Microsoft.AspNetCore.Authentication.Google;
using DotNetEnv;

Env.Load(); // Load environment variables from .env file

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IMyEmailSender, MyEmailSender>();
// builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.Configure<CloudinarySettings>(options =>
{
    options.CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME") ?? throw new InvalidOperationException("CLOUDINARY_CLOUD_NAME environment variable is not set.");
    options.ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY") ?? throw new InvalidOperationException("CLOUDINARY_API_KEY environment variable is not set.");
    options.ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET") ?? throw new InvalidOperationException("CLOUDINARY_API_SECRET environment variable is not set.");
});
builder.Services.AddDbContext<LibraryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"));
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBorrowRepository, BorrowRepository>();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddIdentity<WebUser, IdentityRole>().AddEntityFrameworkStores<LibraryContext>().AddDefaultTokenProviders();
//builder.Services.AddMemoryCache();
//builder.Services.AddSession();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//        options.LoginPath = "/Account/Login";
//        options.AccessDeniedPath = "/Account/Login";
//    });
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
})
.AddGoogle(options =>
{
    options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? throw new InvalidOperationException("GOOGLE_CLIENT_ID environment variable is not set.");
    options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? throw new InvalidOperationException("GOOGLE_CLIENT_SECRET environment variable is not set."); 
});
builder.Services.AddAuthorization();

builder.Services.AddDistributedMemoryCache(); // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Set session timeout
    options.Cookie.HttpOnly = true; // Ensure security
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING") ?? throw new InvalidOperationException("MONGODB_CONNECTION_STRING environment variable is not set.");
    options.DatabaseName = "library";
    options.CollectionName = "feedback_customer";
});
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseStatusCodePagesWithRedirects("/Error/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=CheckBorrow}/{id?}");



app.Run();

