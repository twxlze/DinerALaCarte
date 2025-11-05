using API_Footies.Data.DAO;
using API_Footies.Data.Interfaces;
using API_Footies.Services.Interfaces;
using API_Footies.Services.Realisations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// ---- Injections des dépendances ----
builder.Services.AddScoped<IInviteDAO, InviteDAO>();
builder.Services.AddScoped<IInviteService, InviteService>();


builder.Services.AddScoped<IPersonneDAO, PersonneDAO>();
builder.Services.AddScoped<IInviteDAO, InviteDAO>();
builder.Services.AddScoped<ITypeService, TypeService>();

SQLitePCL.Batteries.Init();

var app = builder.Build();

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
