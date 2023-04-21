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
                 //AddSingleton() - ������, �� ������� ���� ��������� ������� �� ��� ����� ����� ����������

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<HomeWork20Context>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => //���������������� ������� �����������
{
    options.Password.RequiredLength = 6; //����������� ���-�� ������ � ������
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //���������� �����������
    options.Lockout.MaxFailedAccessAttempts = 5; //������������ ���-�� ������� �� ����������
    options.Lockout.AllowedForNewUsers = true;
    
});

builder.Services.ConfigureApplicationCookie(options => //���������������� ����
{
    options.Cookie.HttpOnly = true; //��������, ��� ���� �������� ������ ����� HTTP-�������, ��� ��� ������������
    options.ExpireTimeSpan = TimeSpan.FromDays(1);//����� ���� 1 ����, ����� 1 ���� ���� �������� �� ��������
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Denied"; //��������, ���� ��� ���� �������
    options.SlidingExpiration = true; //���������� ����� ����� ���� ��� ����� ���������

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

app.UseAuthentication();//��� ������������
app.UseAuthorization();//����� �����

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Persone}/{action=Index}/{id?}");


app.Run();
