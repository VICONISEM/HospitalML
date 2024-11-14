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
            CreateMap<BiologicalIndicators, BiologicalIndicatorDto>().ForMember(x => x.SugarPercentage, y => y.MapFrom(P => P.SugarPercentage))
                                                        .ForMember(x => x.date, y => y.MapFrom(P => P.Date))
                                                        .ForMember(x => x.time, y => y.MapFrom(P => P.Time))
                                                        .ForMember(x=>x.HealthConditionScore,y=>y.MapFrom(P=>P.HealthConditionScore))
                                                        .ForMember(x=>x.HealthCondition,y=>y.MapFrom(p=>p.HealthCondition));
                                                       







        }
    }
}
