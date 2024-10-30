using Hospital.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Configurations
{
	public class HospitalConfiguration : IEntityTypeConfiguration<Hospitals>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Hospitals> builder)
		{
			builder.HasKey(h => h.Id);

			builder.Property(h => h.Id)
				   .UseIdentityColumn(1, 1);
			builder.HasIndex(h => h.Name)
				  .IsUnique();

			builder.HasMany(h => h.Patients)
				.WithOne(p => p.hospital)
				.HasForeignKey(p => p.HospitalId);

		}
	}
}
