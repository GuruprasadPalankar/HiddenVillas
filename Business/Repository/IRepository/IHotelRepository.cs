using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelRepository
    {
        public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO);
        public Task<int> DeleteHotelRoom(int roomId);
        public Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDate=null, string checkOutDate=null);
        public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string checkInDate=null, string checkOutDate=null);
        public Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0);
        public Task<bool> IsRoomBooked(int roomId, string checkInDate, string checkOutDate);
    }
}
