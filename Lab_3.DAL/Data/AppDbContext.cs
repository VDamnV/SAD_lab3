using Microsoft.EntityFrameworkCore;
using Lab_3.DAL.Entities;

namespace Lab_3.DAL.Data
{
    public class AppDbContext : DbContext
    {
        // Таблиці бази даних
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        // Конструктор для передачі налаштувань
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("HotelLab3Db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Налаштування зв'язку між Room та Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade); // При видаленні кімнати, видаляються і її бронювання
        }
    }
}
