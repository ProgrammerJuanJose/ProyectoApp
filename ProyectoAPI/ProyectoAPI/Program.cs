using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Models;

namespace ProyectoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //Vamos a leer el CNNSTR de .json para configurar la conexión
            var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));

            CnnStrBuilder.Password = "1234";

            string cnnStr = CnnStrBuilder.ConnectionString;

            builder.Services.AddDbContext<ProyectoFinalContext>(options => options.UseSqlServer(cnnStr));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}