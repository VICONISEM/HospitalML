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
    public class ApplicationUserConfigration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(A => A.Hospital)
                .WithOne(A => A.ApplicationUser)
                .HasForeignKey<ApplicationUser>(A => A.HospitalId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
