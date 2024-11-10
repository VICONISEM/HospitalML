
using Hospital.BLL.BiologicalIndicatorServices.Mapping;
using Hospital.BLL.BiologicalIndicatorServices.Service;
using Hospital.BLL.PatientServices.Mapping;
using Hospital.BLL.PatientServices.Service;
using Hospital.BLL.Repository;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Contexts;
using HospitalML.Extentions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
			builder.Services.AddScoped<IPatientService, PatientService>();
			builder.Services.AddScoped<IBiologicalIndicatorService,BiologicalIndicatorService>();


	        builder.Services.AddAutoMapper(x => x.AddProfile(new PatientMapper()));
			builder.Services.AddAutoMapper(x=>x.AddProfile(new BiologicalIndicatorMapper()));	
			//builder.Services.AddAutoMapper(typeof(PatientMapper));



			var app = builder.Build();


			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<HospitalDbContext>();
			HospitalMLSeed.SeedData(context);
 


			app.UseCors();

            if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
            {
                app.UseDeveloperExceptionPage();


                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Scoreboard API v1");
                    c.RoutePrefix = string.Empty;
                });
            }
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
