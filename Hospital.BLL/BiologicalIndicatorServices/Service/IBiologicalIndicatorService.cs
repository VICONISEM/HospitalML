﻿using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.Repository;
using Hospital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.BiologicalIndicatorServices.Service
{
	public interface IBiologicalIndicatorService
	{
        public BiologicalIndicatorsRepository _BiologicalIndicatorsRepository { get; }

		Task<List<BiologicalIndicatorDto>> GetAllBiologicalIndicators();

        Task<List<BiologicalIndicatorDto>> GetAll();

        Task<BiologicalIndicatorDto?> GetById(int? id);

        Task<int> Add(BiologicalIndicatorDto entity);

        Task<int> Update(BiologicalIndicatorDto entity);
        Task<int> Delete(BiologicalIndicatorDto entity);
    }
}