
using AlunosAPI.Context;
using AlunosAPI.DTOs.Mapping;
using AlunosAPI.Repository.Interfaces;
using AlunosAPI.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AlunosAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connDB = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connDB)
            );

            builder.Services.AddControllers();

            // Registrando serviço do Unit of Work (repositório)
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Registrando o serviço o AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

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

            app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}