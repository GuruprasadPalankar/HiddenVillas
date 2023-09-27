using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HotelRoomDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter room name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter occupancy")]
        public int Occupancy { get; set; }
        [Range(1, 50000, ErrorMessage = "Regular rate must be between 1 to 50000")]
        public double RegularRate { get; set; }
        public string Details { get; set; }
        public string SqFt { get; set; }

        public double TotalAmount { get; set; }
        public int TotalDays { get; set; }

        public virtual ICollection<HotelRoomImageDTO> HotelRoomImages { get; set; }

        public List<string> ImageUrls { get; set; }

        public bool IsBooked { get; set; }
    }
}
