using FullProject;
using FullProject.Context;
using FullProject.Data;
using FullProject.Repo;
using FullProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Net.Http;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddBlazorBootstrap();

builder.Services.AddDbContext<LoginDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ThietLapContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString")));

builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<ThietLapService>();
builder.Services.AddScoped<BangCongService>();//Thêm vào đây để database  có thể hiện lên web mà không bị lỗi//
builder.Services.AddScoped<ChamCongService>();

// Register HttpClient
builder.Services.AddScoped<HttpClient>();

builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
