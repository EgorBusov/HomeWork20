using HomeWork20.AuthModels;
using HomeWork20.Context;
using HomeWork20.Data;
using HomeWork20.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HomeWork20Context>(options => options.UseSqlServer(connection));
                    
builder.Services.AddScoped< IPersoneData, PersoneData>();
                 //AddSingleton() - Аналог, но создает один экземпляр сервиса на все время жизни приложения

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<HomeWork20Context>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => //конфигурирование условий авторизации
{
    options.Password.RequiredLength = 6; //минимальное кол-во знаков в пароле
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //блокировка авторизации
    options.Lockout.MaxFailedAccessAttempts = 5; //максимальное кол-во попыток до блокировки
    options.Lockout.AllowedForNewUsers = true;
    
});

builder.Services.ConfigureApplicationCookie(options => //конфигурирование куки
{
    options.Cookie.HttpOnly = true; //означает, что куки доступны только через HTTP-запросы, это для безопасности
    options.ExpireTimeSpan = TimeSpan.FromDays(1);//жизнь куки 1 день, через 1 день куки удалятся из браузера
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Denied"; //редирект, если нет прав доступа
    options.SlidingExpiration = true; //продлевает время жизни куки при новом обращении

});

// Add services to the container.
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

app.UseAuthentication();//Кто пользователь
app.UseAuthorization();//Какие права

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Persone}/{action=Index}/{id?}");


app.Run();
