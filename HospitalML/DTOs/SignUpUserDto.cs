namespace HospitalML.DTOs
{
    public class SignUpUserDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int? HospitalId { get; set; }
    }
}
