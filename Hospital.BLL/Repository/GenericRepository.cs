using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Contexts;
using Hospital.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Repository
{
	public class GenericRepository<TEntity> : IGenaricRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly HospitalDbContext _context;
		public GenericRepository(HospitalDbContext context)
		{
			_context = context;
		}



		public async Task<List<TEntity>> GetAll()
	=> await _context.Set<TEntity>().ToListAsync();

		public async Task<int> Add(TEntity entity)
		{
			 _context.Set<TEntity>().Add(entity);
			return await _context.SaveChangesAsync();

		}

		public async Task<int> Update(TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
           return await _context.SaveChangesAsync();
        }


		public async  Task<TEntity?> GetById(int? id)
		{  
			return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<int> Delete(TEntity entity)
		{
			_context.Set<TEntity>().Remove(entity);
			return await _context.SaveChangesAsync();
		}



	}
}
