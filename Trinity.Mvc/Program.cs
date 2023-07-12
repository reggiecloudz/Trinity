using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Trinity.Mvc.Data;
using Trinity.Mvc.Data.Repository;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();

builder.Services.Configure<IdentityOptions>(options =>
{
        // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IConnectionRepository, ConnectionRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN"); 
builder.Services.AddControllersWithViews().AddNewtonsoftJson(opt => 
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddSignalR(configure => configure.EnableDetailedErrors = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chatHub");
    endpoints.MapHub<NotificationHub>("/notificationHub");
});

app.Run();
