using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.PatientServices.Dto
{
    public class PatientDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Age { get; set; }
        public bool Sex { get; set; } //True for Boys
        public int NumberOfBirth { get; set; }
        public bool Pregnant { get; set; }
        public int HospitalId { get; set; }




    }
}
