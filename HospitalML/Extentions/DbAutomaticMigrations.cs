﻿using Hospital.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HospitalML.Extentions
{
	public static class DbAutomaticMigrations
	{
		public static void ApplyMigrstions(this IServiceProvider services)
		{
			using var scope = services.CreateScope();
			var Dbcontext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
            var identityDbcontext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();

            try
            {
				Dbcontext.Database.Migrate();
                identityDbcontext.Database.Migrate();

                Console.WriteLine("Database migration applied successfully.");

			}

			catch (Exception ex)
			{


				Console.WriteLine("An error occurred while migrating the database: " + ex.Message);
				// Optionally, log the exception or handle it as necessary
 
			} 
			}
		}
	}

 

