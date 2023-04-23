using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAAPI.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public double Sqft { get; set; }
        public double Occupancy { get; set; }

    }
}
