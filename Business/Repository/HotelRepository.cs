using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            var hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";
            var addHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoom, HotelRoomDTO>(addHotelRoom.Entity);
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            try
            {
                var roomDetails = await _db.HotelRooms.FindAsync(roomId);
                if(roomDetails != null)
                {
                    var allImages = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
                    _db.HotelRoomImages.RemoveRange(allImages);
                    _db.HotelRooms.Remove(roomDetails);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs = _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(
                    await _db.HotelRooms.Include(x=>x.HotelRoomImages).ToListAsync());

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
                {
                    foreach (var hotelRoomDTO in hotelRoomDTOs)
                    {
                        hotelRoomDTO.IsBooked = await IsRoomBooked(hotelRoomDTO.Id, checkInDateStr, checkOutDateStr);
                    }
                }

                return hotelRoomDTOs;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                var hotelRoomDTO = _mapper.Map<HotelRoom, HotelRoomDTO>( 
                    await _db.HotelRooms.Include(x=>x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
                {
                    hotelRoomDTO.IsBooked = await IsRoomBooked(roomId, checkInDateStr, checkOutDateStr);
                }

                return hotelRoomDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> IsRoomBooked(int roomId, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                if(!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDateStr, "MM/dd/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDateStr, "MM/dd/yyyy", null);

                    var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == roomId && x.IsPaymentSuccessful
                        && ((checkInDate.Date < x.CheckOutDate.Date && checkInDate.Date >= x.CheckInDate.Date)
                        || (checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date)))
                        .FirstOrDefaultAsync();

                    if (existingBooking != null)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //If Room is Unique then returns null else returns room obj
        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if(roomId == 0)
                {
                    var hotelRoomDTO = _mapper.Map<HotelRoom, HotelRoomDTO>(await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));
                    return hotelRoomDTO;
                }
                else
                {
                    var hotelRoomDTO = _mapper.Map<HotelRoom, HotelRoomDTO>(await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != roomId));
                    return hotelRoomDTO;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if(roomId == hotelRoomDTO.Id)
                {
                    var roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    var hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    hotelRoom.UpdatedBy = "";
                    hotelRoom.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.HotelRooms.Update(hotelRoom);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
