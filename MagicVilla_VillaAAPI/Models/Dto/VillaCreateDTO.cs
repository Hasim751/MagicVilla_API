using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAAPI.Models.Dto
{
    public class VillaCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public double Sqft { get; set; }
        public double Occupancy { get; set; }
        [Required]
        public double Rate { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public string Description { get; set; }


    }
}
