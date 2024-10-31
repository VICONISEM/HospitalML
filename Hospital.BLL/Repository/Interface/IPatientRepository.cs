using Hospital.BLL.Patient.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Repository.Interface
{
    public interface IPatientRepository
    {
        List<string> GetAllName();

        List<PatientDto> GetBIByName(string name);

    }
}
