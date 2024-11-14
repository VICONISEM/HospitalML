using Hospital.DAL.Contexts;
using Hospital.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HospitalML
{
	public static class HospitalMLSeed
	{ 
	public static void SeedData(HospitalDbContext context)
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


			if (!context.patients.Any())
			{
				var patientsData = File.ReadAllText(@"..\HospitalML\DataSeed\patients.json");


				var patient = JsonSerializer.Deserialize<List<Patient>>(patientsData);


				if (patient is not null && patient.Count() > 0)
				{

					context.patients.AddRange(patient);
					context.SaveChanges();
				}
			}


			if (!context.BiologicalIndicators.Any())
			{
				var BiologicalIndicatorsData = File.ReadAllText(@"..\HospitalML\DataSeed\biologicalIndicators.json");


				var BiologicalIndicator = JsonSerializer.Deserialize<List<BiologicalIndicators>>(BiologicalIndicatorsData);


				if (BiologicalIndicator is not null && BiologicalIndicator.Count() > 0)
				{

					context.BiologicalIndicators.AddRange(BiologicalIndicator);
					context.SaveChanges();
				}
			}

        }
	}
}
