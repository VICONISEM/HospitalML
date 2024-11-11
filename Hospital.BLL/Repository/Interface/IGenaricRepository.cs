using Hospital.BLL.PatientServices.Dto;
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
        Task<List<TEntity>>GetAll();

         Task<TEntity?> GetById(int? id);    

        Task Add(TEntity entity); 
        
        Task Update(TEntity entity);    
        Task Delete(TEntity entity);    
       


       
        
    }
}
