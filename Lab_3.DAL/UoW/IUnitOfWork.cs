using System;
using Lab_3.DAL.Entities;
using Lab_3.DAL.Repositories;

namespace Lab_3.DAL.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        // Доступ до репозиторію номерів
        IRepository<Room> Rooms { get; }
        
        // Доступ до репозиторію бронювань
        IRepository<Booking> Bookings { get; }
        
        // Метод для збереження всіх змін однією транзакцією
        void Save();
    }
}