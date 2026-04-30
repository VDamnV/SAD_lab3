using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Lab_3.DAL.Data;
using Lab_3.DAL.Entities;

namespace Lab_3.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        // Впровадження залежності контексту бази даних
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            // Повертаються всі записи як список.
            return _dbSet.AsNoTracking().ToList();
        }

        public T? GetById(int id)
        {
            // Пошук записів за Id
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            // Повідомлення до Entity Framework, про зміну сутності
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
