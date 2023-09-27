using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service.IService
{
    public interface IHotelAmenityService
    {
        Task<IEnumerable<HotelAmenityDTO>> GetHotelAmenities();
    }
}
