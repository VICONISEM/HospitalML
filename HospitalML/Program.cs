
using Hospital.BLL.Repository;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Contexts;
using HospitalML.Extentions;
using Microsoft.EntityFrameworkCore;

namespace HospitalML
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<HospitalDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));





			});
			builder.Services.AddScoped<IPatientRepository, PatientRepository>();
		 


			var app = builder.Build();
			app.Services.ApplyMigrstions();

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
}
