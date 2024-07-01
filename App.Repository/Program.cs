using App.Repository.Data.Databases;
using App.Repository.Models.Configuration;
using App.Repository.Services.Auth;
using App.Repository.Services.Repositorios;
using App.Repository.Setup;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.AddConfiguration();

var _config = builder.Configuration.Get<AppRepositoryConfiguration>();

// -- Injetando o contexto do banco de dados, já está recebendo 
// a connection string de acordo com o  ambiente
builder.Services.AddDbContext<RepositoryDbContext>(options =>
{
    options.UseMySql(_config.ConnectionStrings!.BancoDeDadosConexao,
        ServerVersion.AutoDetect(_config!.ConnectionStrings!.BancoDeDadosConexao));
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// -- Injetando Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRepositorioService, RepositorioService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapGet("/", context =>
    {
        return Task.Run(() => context.Response.Redirect("/auth/login"));
    });

    endpoints.MapFallback(context => {
        context.Response.Redirect("/auth/login");
        return Task.CompletedTask;
    });
});

app.Run();
