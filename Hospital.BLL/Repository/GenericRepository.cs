using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Contexts;
using Hospital.DAL.Entities;
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

		public List<TEntity> GetAll()
	=> _context.Set<TEntity>().ToList();

		public void Add(TEntity entity)
		{
			_context.Set<TEntity>().Add(entity);

		}

		public void Update(TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
		}


		public TEntity? GetById(int? id)
		{  
			return  _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
		}



		public void Delete(TEntity entity)
		{
		 _context.Set<TEntity>().Remove(entity);
		}



	}
}
