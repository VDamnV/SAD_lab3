using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lab_3.BLL.DTOs;
using Lab_3.BLL.Interfaces;
using Lab_3.DAL.Entities;
using Lab_3.DAL.UoW;

namespace Lab_3.BLL.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        // Впровадження залежностей
        public BusinessService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<RoomDto> GetAllRooms()
        {
            var rooms = _uow.Rooms.GetAll();
            // Використовуєnmcz AutoMapper для перетворення списку Room у список RoomDto
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public IEnumerable<RoomDto> GetFreeRooms()
        {
            // Gjier номерsd, зі статусом Free
            var freeRooms = _uow.Rooms.GetAll().Where(r => r.Status == RoomStatus.Free);
            return _mapper.Map<IEnumerable<RoomDto>>(freeRooms);
        }

        public bool BookRoom(int roomId, DateTime start, DateTime end)
        {
            var room = _uow.Rooms.GetById(roomId);
            
            if (room == null || room.Status != RoomStatus.Free)
            {
                return false; 
            }

            // Нове бронювання
            var booking = new Booking
            {
                RoomId = roomId,
                StartDate = start,
                EndDate = end
            };

            // Змінюєтьсяо статус номеру на "Заброньовано"
            room.Status = RoomStatus.Booked;
            
            // Додається бронювання та оновлюється номер у репозиторіях
            _uow.Bookings.Add(booking);
            _uow.Rooms.Update(room);
            
            _uow.Save();

            return true;
        }

        public bool CancelBooking(int bookingId)
        {
            var booking = _uow.Bookings.GetById(bookingId);
            if (booking == null) return false;

            var room = _uow.Rooms.GetById(booking.RoomId);
            if (room != null)
            {
                // Повертається номеру статус "Вільний"
                room.Status = RoomStatus.Free;
                _uow.Rooms.Update(room);
            }

            // Видаляється запис про бронювання
            _uow.Bookings.Delete(bookingId);
            
            _uow.Save();

            return true;
        }
    }
}
