using System;
using System.Collections.Generic;
using Lab_3.BLL.DTOs;

namespace Lab_3.BLL.Interfaces
{
    public interface IBusinessService
    {
        /// <summary>
        /// Повертає список усіх номерів готелю
        /// </summary>
        IEnumerable<RoomDto> GetAllRooms();

        /// <summary>
        /// Повертає список лише вільних номерів
        /// </summary>
        IEnumerable<RoomDto> GetFreeRooms();

        /// <summary>
        /// Бронює номер на вказаний період
        /// </summary>
        /// <param name="roomId">Ідентифікатор номеру</param>
        /// <param name="start">Дата початку</param>
        /// <param name="end">Дата завершення</param>
        /// <returns>True, якщо успішно, інакше False (наприклад, номер вже зайнятий)</returns>
        bool BookRoom(int roomId, DateTime start, DateTime end);

        /// <summary>
        /// Знімає існуюче бронювання
        /// </summary>
        /// <param name="bookingId">Ідентифікатор бронювання</param>
        /// <returns>True, якщо бронь знайдена та знята</returns>
        bool CancelBooking(int bookingId);
    }
}