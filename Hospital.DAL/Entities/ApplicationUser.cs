using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entities
{
     public  class ApplicationUser:IdentityUser
    {
        public int ? HospitalId { get; set; }

        public Hospitals Hospital { get; set; }

    }
}
