using AutoMapper;
using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.PatientServices.Dto;
using Hospital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.PatientServices.Mapping
{
	public class PatientMapper :Profile
	{
        public PatientMapper()
        {
            CreateMap<Patient, PatientDto>().ReverseMap();
                                                       







        }
    }
}
