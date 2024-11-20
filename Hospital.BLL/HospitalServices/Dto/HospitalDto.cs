using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;

namespace  Hospital.BLL.HospitalServices.Dto
{
    public class HospitalDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string ?ImageURL { get; set; } = null!;
        public IFormFile HospitalImage { get; set; }=null!;

    }
}
