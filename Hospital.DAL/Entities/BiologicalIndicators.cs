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
        public string SugarPercentage { get; set; }
        public int BloodPressure { get; set; }

		public string AverageTemprature { get; set; }
		public TimeOnly Time { get; set; }
        public int PatientId { get; set; }
        public Patient paient { get; set; }



    }
}
