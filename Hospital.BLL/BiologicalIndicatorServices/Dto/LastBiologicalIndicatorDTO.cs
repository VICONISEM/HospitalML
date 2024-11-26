using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.BiologicalIndicatorServices.Dto
{
    public class LastBiologicalIndicatorDTO
    {
        public string HealthCondition { get; set; }
        public float SugarPercentage { get; set; }
        public float BloodPressure { get; set; }
        public float AverageTemprature { get; set; }
    }
}
