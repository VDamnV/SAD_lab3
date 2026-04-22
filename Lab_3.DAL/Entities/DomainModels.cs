using System;
using System.Collections.Generic;

namespace Lab_3.DAL.Entities
{
    // Статуси номеру згідно з варіантом 3
    public enum RoomStatus
    {
        Free,      // Вільний
        Booked,    // Заброньований
        Occupied   // Зданий (зайнятий)
    }

    public class Room : BaseEntity
    {
        public int Number { get; set; }
        
        public string Category { get; set; } = string.Empty;
        
        public decimal PricePerNight { get; set; }
        
        public RoomStatus Status { get; set; } = RoomStatus.Free;

        // Навігаційна властивість для Entity Framework (зв'язок 1-до-багатьох)
        // Один номер може мати історію з кількох бронювань
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

    public class Booking : BaseEntity
    {
        // Зовнішній ключ для зв'язку з таблицею Rooms
        public int RoomId { get; set; }
        
        // Навігаційна властивість
        public Room Room { get; set; } = null!;

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}