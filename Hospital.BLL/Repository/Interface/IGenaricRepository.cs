using Hospital.BLL.Patient.Dto;
using Hospital.DAL.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Repository.Interface
{
    public interface IGenaricRepository<TEntity> where TEntity : BaseEntity
    {
        List<TEntity>GetAll();

       
        
    }
}
