using Hospital.BLL.PatientServices.Dto;
using Hospital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Repository.Interface
{
    public interface IPatientRepository:IGenaricRepository<Patient>
    {
        List<PatientDtoName> GetAllName();

        List<BiologicalIndicators> GetBIByName(string name);

    }
}
