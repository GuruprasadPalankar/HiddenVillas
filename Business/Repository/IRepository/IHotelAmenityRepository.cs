using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelAmenityRepository
    {
        public Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenityDTO);
        public Task<HotelAmenityDTO> UpdateHotelAmenity(int amenityId, HotelAmenityDTO hotelAmenityDTO);
        public Task<int> DeleteHotelAmenity(int amenityId);
        public Task<HotelAmenityDTO> GetHotelAmenity(int amenityId);
        public Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenities();
        public Task<HotelAmenityDTO> IsAmenityUnique(string name, int amenityId = 0);
    }
}
