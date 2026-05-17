using System;
using Lab_3.DAL.Entities;
using Lab_3.DAL.Repositories;

namespace Lab_3.DAL.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Room> Rooms { get; }
        
        IRepository<Booking> Bookings { get; }
        
        void Save();
    }
}
