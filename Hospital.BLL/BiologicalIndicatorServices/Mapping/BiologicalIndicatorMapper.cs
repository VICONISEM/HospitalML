using AutoMapper;
using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.BiologicalIndicatorServices.Mapping
{
	public class BiologicalIndicatorMapper :Profile
	{
        public BiologicalIndicatorMapper()
        {
            CreateMap<BiologicalIndicators,BiologicalIndicatorDto>().ForMember(x=>x.HealthCondition,y=>y.MapFrom(P=>P.HealthCondition))
                                                                     .ForMember(x=>x.BloodPressure,y=>y.MapFrom(p=>p.BloodPressure))
                                                                     .ForMember(x=>x.AverageTemprature,y=>y.MapFrom(p=>p.AverageTemprature))
                                                                     .ForMember(x=>x.SugarPercentage,y=>y.MapFrom(p=>p.SugarPercentage))
                                                                     .ForMember(x=>x.HealthConditionScore,y=>y.MapFrom(p=>p.HealthConditionScore));
        }
    }
}
