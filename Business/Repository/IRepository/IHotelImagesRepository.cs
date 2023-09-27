using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelImagesRepository
    {
        public Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDTO);

        public Task<int> DeleteHotelRoomImageByImageId(int imageId);

        public Task<int> DeleteHotelRoomImageByRoomId(int roomId);

        public Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl);

        public Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId);
    }
}
