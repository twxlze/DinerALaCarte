using API_Footies.Data.DAO;
using API_Footies.Data.Interfaces;
using API_Footies.Services.Interfaces;
using API_Footies.Services.Realisations;
using SQLitePCL;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        /// ---- Injections des dépendances Pour Invite ----
        builder.Services.AddScoped<IInviteDAO, InviteDAO>();
        builder.Services.AddScoped<IInviteService, InviteService>();


        Batteries_V2.Init();

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
    }
}