using System.Collections.Generic;
using Lab_3.DAL.Entities;

namespace Lab_3.DAL.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        // Отримати всі записи
        IEnumerable<T> GetAll();
        
        // Отримати запис за його ідентифікатором
        T? GetById(int id);
        
        // Додати новий запис
        void Add(T entity);
        
        // Оновити існуючий запис
        void Update(T entity);
        
        // Видалити запис за ідентифікатором
        void Delete(int id);
    }
}