using MagicVilla_VillaAAPI.Models.Dto;

namespace MagicVilla_VillaAAPI.Data
{
    public class VillaStore
    {

        public static List<VillaDto> villaList =  new List<VillaDto>
            {
                new VillaDto{ Id = 1, Name = "Pool View", Sqft = 1000, Occupancy = 3 },
                new VillaDto{ Id = 2, Name = "Beach View", Sqft= 650, Occupancy = 1 }
            };
    }
}
