using BLL.Interfaces;
using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly MvcAppDbContext _Context;

        public GenericRepository(MvcAppDbContext context)
        {
            _Context = context;
        }
        public int Add(T entity)
        {
            _Context.Set<T>().Add(entity);
            return _Context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _Context.Set<T>().Remove(entity);
            return _Context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
            => _Context.Set<T>().ToList();

        public T GetById(int? id)
            => _Context.Set<T>().Find(id);

        public int Update(T entity)
        {
            _Context.Set<T>().Update(entity);
            return _Context.SaveChanges();
        }
    }
}
