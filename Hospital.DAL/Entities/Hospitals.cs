using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entities
{
	public class Hospitals:BaseEntity
	{
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public List<Patient> Patients { get; set; } = new List<Patient>();
	}
}
