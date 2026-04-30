using System;
using System.Collections.Generic;
using Lab_3.BLL.DTOs;

namespace Lab_3.BLL.Interfaces
{
    public interface IBusinessService
    {
        IEnumerable<RoomDto> GetAllRooms();

        IEnumerable<RoomDto> GetFreeRooms();

        bool BookRoom(int roomId, DateTime start, DateTime end);

        bool CancelBooking(int bookingId);
    }
}
