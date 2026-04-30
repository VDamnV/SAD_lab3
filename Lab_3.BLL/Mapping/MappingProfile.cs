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
            // Сутність бази даних -> DTO
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Booking, BookingDto>();

            // DTO -> Сутність
            CreateMap<RoomDto, Room>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<RoomStatus>(src.Status)));
                
            CreateMap<BookingDto, Booking>();
        }
    }
}
