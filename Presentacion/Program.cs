using Microsoft.AspNetCore.Authentication.Cookies;

using log4net.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

//Coockies
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(option => {
//        option.LoginPath = "/Usuario/Login";
//        option.ExpireTimeSpan= TimeSpan.FromMinutes(20);
//        option.AccessDeniedPath= "/Home/Privacy";
//    });

//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(60);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});


//Log4net
builder.Logging.AddLog4Net(); //Proveedor
XmlConfigurator.Configure(new FileInfo("log4net.config"));

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//Hbiliar session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Usuario}/{action=Login}");

app.Run();
