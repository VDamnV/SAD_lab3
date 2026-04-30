using System;
using Lab_3.DAL.Data;
using Lab_3.DAL.Entities;
using Lab_3.DAL.Repositories;

namespace Lab_3.DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        
        // Приватні поля для зберігання екземплярів репозиторіїв
        private IRepository<Room>? _roomRepository;
        private IRepository<Booking>? _bookingRepository;
        
        private bool _disposed = false;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        // Властивість для доступу до кімнат.
        public IRepository<Room> Rooms
        {
            get
            {
                _roomRepository ??= new Repository<Room>(_context);
                return _roomRepository;
            }
        }

        // Властивість для доступу до бронювань
        public IRepository<Booking> Bookings
        {
            get
            {
                _bookingRepository ??= new Repository<Booking>(_context);
                return _bookingRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
