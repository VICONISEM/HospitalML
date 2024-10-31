﻿namespace Hospital.DAL.Entities
{
	public class Patient:BaseEntity
	{
        public string Name { get; set; }

        public string PhoneNumber { get; set; }


		public string Address { get; set; }

  

        public bool Sex { get; set; } //True for Boys

        public int NumberOfBirth { get; set; }

        public bool Pregnant { get; set; }
		public int HospitalId { get; set; }
		public Hospitals hospital { get; set; }

        public  List<BiologicalIndicators> biologicalIndicators { get; set; } = new List<BiologicalIndicators>();




    }
}