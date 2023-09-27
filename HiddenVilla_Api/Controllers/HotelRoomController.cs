using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    public class HotelRoomController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        
        public HotelRoomController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        //[Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> GetHotelRooms(string checkInDate=null, string checkOutDate=null)
        {
            if(string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }

            if(!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy",CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid checkInDate format. Valid format is MM/dd/yyyy"
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid checkOutDate format. Valid format is MM/dd/yyyy"
                });
            }

            var allRooms = await _hotelRepository.GetAllHotelRooms(checkInDate, checkOutDate);
            return Ok(allRooms);
        }

        [HttpGet("roomId")]
        public async Task<IActionResult> GetHotelRoom(int? roomId, string checkInDate = null, string checkOutDate = null)
        {
            if(roomId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid Room Id"
                });
            }

            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }

            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid checkInDate format. Valid format is MM/dd/yyyy"
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid checkOutDate format. Valid format is MM/dd/yyyy"
                });
            }

            var roomDetails = await _hotelRepository.GetHotelRoom(roomId.Value, checkInDate, checkOutDate);
            if (roomDetails == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Invalid Room Id"
                });
            }

            return Ok(roomDetails);
        }
    }
}
