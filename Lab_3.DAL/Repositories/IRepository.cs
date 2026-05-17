using System.Collections.Generic;
using Lab_3.DAL.Entities;

namespace Lab_3.DAL.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        
        T? GetById(int id);
        
        void Add(T entity);
        
        void Update(T entity);
        
        void Delete(int id);
    }
}