using Hospital.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Configurations
{
    public class PatientCongiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .UseIdentityColumn(1, 1);

            builder.HasIndex(p => p.Name);

            builder.HasMany(p => p.biologicalIndicators)
                .WithOne(BI => BI.patient)
                .HasForeignKey(BI => BI.PatientId);






        }
    }
}
