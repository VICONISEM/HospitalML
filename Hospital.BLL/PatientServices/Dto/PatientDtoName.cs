using Hospital.BLL.BiologicalIndicatorServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.PatientServices.Dto
{
    public  class PatientDtoName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }
        public int HospitalId { get; set; }

        public LastBiologicalIndicatorDTO LastBiologicalIndicator { get; set; }

    }
}
