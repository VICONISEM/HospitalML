using AutoMapper;
using Hospital.BLL.HospitalServices.Dto;
using Hospital.DAL.Entities;
using HospitalML.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.HospitalServices.Mapping
{
    public class HospitalMapper : Profile
    {
        public HospitalMapper() {
            CreateMap<Hospitals, HospitalDto>()
                .ForMember(destination => destination.ImageURL,  Source => Source.MapFrom<HospitalPictureURLResolver>())
                .ReverseMap();



        }
    }
}
