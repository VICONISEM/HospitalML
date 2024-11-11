﻿using Hospital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.BiologicalIndicatorServices.Dto
{
	public class BiologicalIndicatorDto
	{
		 
		public int HealthConditionScore { get; set; }
		public string HealthCondition { get; set; }
		public float SugarPercentage { get; set; }
		public float BloodPressure { get; set; }

		public float AverageTemprature { get; set; }
		
	}
}