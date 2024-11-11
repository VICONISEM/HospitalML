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


		//  public GenericRepository() { }

		public async Task<List<TEntity>> GetAll()
	=> await _context.Set<TEntity>().ToListAsync();

		public async Task Add(TEntity entity)
		{
			 _context.Set<TEntity>().Add(entity);
			await _context.SaveChangesAsync();

		}

		public async Task Update(TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }


		public async  Task<TEntity?> GetById(int? id)
		{  
			return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
		}



		public async Task Delete(TEntity entity)
		{
		   _context.Set<TEntity>().Remove(entity);
		}



	}
}
