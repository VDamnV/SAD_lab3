using System;

namespace Lab_3.BLL.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        
        public int Number { get; set; }
        
        public string Category { get; set; } = string.Empty;
        
        public decimal PricePerNight { get; set; }
        
        // Для зручності виводу на UI передаємо статус як рядок
        public string Status { get; set; } = string.Empty; 
    }

    public class BookingDto
    {
        public int Id { get; set; }
        
        public int RoomId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}