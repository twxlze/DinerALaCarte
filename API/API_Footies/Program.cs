using API_Footies.Data.DAO;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Outils;
using API_Footies.Services.Interfaces;
using API_Footies.Services.Realisations;
using Microsoft.AspNetCore.Identity;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https:gai//aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// ---- Injections des dépendances ----
builder.Services.AddScoped<IInviteDAO, InviteDAO>();
builder.Services.AddScoped<IInviteService, InviteService>();

builder.Services.AddScoped<IPlatDAO, PlatDAO>();
builder.Services.AddScoped<IPlatService, PlatService>();

builder.Services.AddScoped<IGroupeInviteDAO, GroupeInviteDAO>();
builder.Services.AddScoped<IGroupeInvitesService, GroupeInvitesService>();

builder.Services.AddScoped<IMenuDAO, MenuDAO>();
builder.Services.AddScoped<IMenuService, MenuService>();

builder.Services.AddScoped<IInvitationDAO, InvitationDAO>();
builder.Services.AddScoped<IInvitationService, InvitationService>();


builder.Services.AddScoped<IConnexionDAO, ConnexionDAO>();
builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();
builder.Services.AddScoped<IAuthentification, Authentification>();
builder.Services.AddScoped<IConfigHelper,ConfigHelper>();

SQLitePCL.Batteries.Init();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();