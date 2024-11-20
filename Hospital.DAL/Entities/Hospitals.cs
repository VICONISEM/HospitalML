using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entities
{
	public class Hospitals : BaseEntity
	{
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string ImageURL { get; set; } = null!;

        public List<Patient> Patients { get; set; } = new List<Patient>();

        public ApplicationUser ? ApplicationUser { get; set; }
  
	}
}
