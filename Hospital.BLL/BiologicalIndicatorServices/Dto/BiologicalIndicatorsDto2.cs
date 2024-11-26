using Hospital.BLL.PatientServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.BiologicalIndicatorServices.Dto
{
    public class BiologicalIndicatorsDto2
    {
        public int Count { get; set; }

        public DateOnly Date { get; set; }

        public List<PatientDtoName> Patients { get; set; }
    }
}
