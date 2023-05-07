using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAAPI.Models.Dto
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public double Sqft { get; set; }
        [Required]
        public double Occupancy { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public string Description { get; set; }


    }
}
