using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.Repository;
using Hospital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.PatientServices.Service
{
    public  interface IPatientService
    {

        public PatientRepository _patientRepository { get; }


        List<string> GetAllName();

        List<PatientDto> GetBIByName(string name);

    }
}
