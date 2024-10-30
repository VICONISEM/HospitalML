using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entities
{
	public class BiologicalIndicators :BaseEntity
	{
        public DateOnly Date { get; set; }

        public string HealthaCondition { get; set; }
        public float SugarPercentage { get; set; }
        public float BloodPressure { get; set; }

		public float AverageTemprature { get; set; }
		public TimeOnly Time { get; set; }
        public int PatientId { get; set; }
        public Patient paient { get; set; }



    }
}
