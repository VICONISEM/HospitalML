using AutoMapper;
using Hospital.BLL.HospitalServices.Dto;
using Hospital.DAL.Entities;

namespace HospitalML.Helpers
{
    public class HospitalPictureURLResolver : IValueResolver <Hospitals, HospitalDto , string>
    {
        private readonly IHttpContextAccessor configuration;

        public HospitalPictureURLResolver(IHttpContextAccessor configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Hospitals source, HospitalDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageURL))
            {
                var request = configuration.HttpContext.Request;
                return $"{request.Scheme}://{request.Host}/{source.ImageURL}";

            }
            else
            {
                return "Has No Photo";
            }
        }
    }
}
