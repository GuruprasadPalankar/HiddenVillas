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
    public class HotelAmenityRepository : IHotelAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelAmenityRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenityDTO)
        {
            var hotelAmenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenityDTO);
            hotelAmenity.CreatedDate = DateTime.Now;
            hotelAmenity.CreatedBy = "";
            var addhotelAmenity = await _db.HotelAmenities.AddAsync(hotelAmenity);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelAmenity, HotelAmenityDTO>(addhotelAmenity.Entity);
        }

        public async Task<int> DeleteHotelAmenity(int amenityId)
        {
            try
            {
                var amenityDetails = await _db.HotelAmenities.FindAsync(amenityId);
                if (amenityDetails != null)
                {
                    _db.HotelAmenities.Remove(amenityDetails);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenities()
        {
            try
            {
                IEnumerable<HotelAmenityDTO> hotelAmenityDTO = _mapper.Map<IEnumerable<HotelAmenity>, IEnumerable<HotelAmenityDTO>>(await _db.HotelAmenities.ToListAsync());
                return hotelAmenityDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> GetHotelAmenity(int amenityId)
        {
            try
            {
                var hotelAmenityDTO = _mapper.Map<HotelAmenity, HotelAmenityDTO>(
                    await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Id == amenityId));
                return hotelAmenityDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //If amenity is Unique then returns null else returns amenity obj
        public async Task<HotelAmenityDTO> IsAmenityUnique(string name, int amenityId = 0)
        {
            try
            {
                if (amenityId == 0)
                {
                    var hotelAmenityDTO = _mapper.Map<HotelAmenity, HotelAmenityDTO>(await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));
                    return hotelAmenityDTO;
                }
                else
                {
                    var hotelAmenityDTO = _mapper.Map<HotelAmenity, HotelAmenityDTO>(await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != amenityId));
                    return hotelAmenityDTO;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelAmenityDTO> UpdateHotelAmenity(int amenityId, HotelAmenityDTO hotelAmenityDTO)
        {
            try
            {
                if (amenityId == hotelAmenityDTO.Id)
                {
                    var amenityDetails = await _db.HotelAmenities.FindAsync(amenityId);
                    var hotelAmenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenityDTO, amenityDetails);
                    hotelAmenity.UpdatedBy = "";
                    hotelAmenity.UpdatedDate = DateTime.Now;
                    var updatedamenity = _db.HotelAmenities.Update(hotelAmenity);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelAmenity, HotelAmenityDTO>(updatedamenity.Entity);
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
