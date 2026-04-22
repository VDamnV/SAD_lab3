using System;
using AutoMapper;
using Lab_3.BLL.DTOs;
using Lab_3.DAL.Entities;

namespace Lab_3.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Маппінг: Сутність бази даних -> DTO (передача на UI)
            CreateMap<Room, RoomDto>()
                // Явно вказуємо, що enum RoomStatus треба перетворити в рядок
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Booking, BookingDto>();

            // Зворотний маппінг: DTO -> Сутність (якщо колись знадобиться зберігати DTO напряму)
            CreateMap<RoomDto, Room>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<RoomStatus>(src.Status)));
                
            CreateMap<BookingDto, Booking>();
        }
    }
}