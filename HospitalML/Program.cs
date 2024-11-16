
using Hospital.BLL.BiologicalIndicatorServices.Mapping;
using Hospital.BLL.BiologicalIndicatorServices.Service;
using Hospital.BLL.HospitalServices.Mapping;
using Hospital.BLL.PatientServices.Mapping;
using Hospital.BLL.PatientServices.Service;
using Hospital.BLL.Repository;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Contexts;
using HospitalML.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HospitalML", Version = "v1" });

                // Adding Bearer Token Authorization
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,  // Token will be passed in the Header
                    Description = "Enter Bearer Token",  // Description for Swagger UI
                    Type = SecuritySchemeType.Http,  // The security type for HTTP-based authentication
                    BearerFormat = "JWT",  // Indicating that the token is a JWT
                    Scheme = "Bearer"  // This tells Swagger it's a Bearer token
                });

                // Applying the security requirement for all API endpoints
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"  // The ID should match the name of the security definition
                }
            },
            new string[] { }
        }
    });
            });


            builder.Services.AddDbContext<HospitalDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

           

            //builder.Services.AddScoped<IPatientRepository, PatientRepository>();
			builder.Services.AddScoped<IPatientService, PatientService>();
			builder.Services.AddScoped<IBiologicalIndicatorService,BiologicalIndicatorService>();

            builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenericRepository<>));

			builder.Services.AddHttpClient();


            builder.Services.AddAutoMapper(x => x.AddProfile(new PatientMapper()));
			builder.Services.AddAutoMapper(x=>x.AddProfile(new BiologicalIndicatorMapper()));
			builder.Services.AddAutoMapper(x => x.AddProfile(new HospitalMapper()));
            //builder.Services.AddAutoMapper(typeof(PatientMapper));

			//Add Identity Services TO DI Container
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.Configure<IdentityOptions>(options =>
			{
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            });


			var app = builder.Build();


			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;

			var context = services.GetRequiredService<HospitalDbContext>();

            HospitalMLSeed.SeedData(context);
			IdentitySeed.SeedIdentity(services);
 
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

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
