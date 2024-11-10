using AutoMapper;
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
            CreateMap<BiologicalIndicators, PatientDto>().ForMember(x => x.SugarPercentage, y => y.MapFrom(P => P.SugarPercentage))
                                                        .ForMember(x => x.Date, y => y.MapFrom(P => P.Date))
                                                        .ForMember(x => x.Time, y => y.MapFrom(P => P.Time))
                                                        .ForMember(x=>x.HealthConditionScore,y=>y.MapFrom(P=>P.HealthConditionScore));
                                                       







        }
    }
}
