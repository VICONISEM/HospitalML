using Hospital.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Contexts
{
	public class HospitalDbContext : DbContext
	{
		public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}


		public DbSet<BiologicalIndicators> BiologicalIndicators { get; set; }
		public DbSet<Hospitals> Hospitals { get; set; }
		public DbSet<Patient> patients { get; set; }
	}
}
