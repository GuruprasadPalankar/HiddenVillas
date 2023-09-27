using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HotelAmenityDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter amenity name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please mention the timing upto which amenity is available")]
        public string Timing { get; set; }

        [Required(ErrorMessage = "Please enter suitable icon name")]
        public string Icon { get; set; }

        public string Description { get; set; }
    }
}
