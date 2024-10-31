using Hospital.DAL.Contexts;
using Hospital.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HospitalML
{
	public static class HospitalMLSeed
	{ 
	 public  static void SeedData(  HospitalDbContext context)
		{
			if (!context.Hospitals.Any())
			{
				var HospitalData = File.ReadAllText(@"..\HospitalML\DataSeed\Hospitals.json");


				var Hospital = JsonSerializer.Deserialize<List<Hospitals>>(HospitalData);


				if (Hospital is not null && Hospital.Count() > 0)
				{

					 context.Hospitals.AddRange(Hospital);
						context.SaveChanges();
				}
			}
		}
	}
}
