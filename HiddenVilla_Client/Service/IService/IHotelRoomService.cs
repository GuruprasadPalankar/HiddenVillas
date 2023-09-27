using HiddenVilla_Client.Pages.HotelRooms;
using Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service.IService
{
    public interface IHotelRoomService
    {
        public Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string checkInDate, string checkOutDate);
        public Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate);
    }
}
