using System;
using System.Collections.Generic;

namespace Lab_3.DAL.Entities
{
    // Статуси номеру
    public enum RoomStatus
    {
        Free,      
        Booked,    
        Occupied  
    }

    public class Room : BaseEntity
    {
        public int Number { get; set; }
        
        public string Category { get; set; } = string.Empty;
        
        public decimal PricePerNight { get; set; }
        
        public RoomStatus Status { get; set; } = RoomStatus.Free;

        // Навігаційна властивість для Entity Framework
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
