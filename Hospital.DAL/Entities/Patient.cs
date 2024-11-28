namespace Hospital.DAL.Entities
{
	public class Patient:BaseEntity
	{
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
		public string Address { get; set; } = null!;
        public bool Sex { get; set; } //True for Boys
        public int NumberOfBirth { get; set; }
        public int Age { get; set; }
        public bool Pregnant { get; set; }
		public int HospitalId { get; set; }
		public Hospitals hospital { get; set; } = null!;
        public  List<BiologicalIndicators> biologicalIndicators { get; set; } = new List<BiologicalIndicators>();

    }
}