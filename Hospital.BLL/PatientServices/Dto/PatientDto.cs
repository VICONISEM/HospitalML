using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.PatientServices.Dto
{
    public class PatientDto
    {

        public float SugarPercentage { get; set; }

        public TimeOnly Time { get; set; }

        public DateOnly Date { get; set; }

        public int HealthConditionScore { get; set; }
        public string HealthCondition { get; set; }


    }
}
