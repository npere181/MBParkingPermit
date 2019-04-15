using System;
using System.Collections.Generic;

namespace MiamiBeachPP.Models
{
    public partial class ParkingAreaTypes
    {
        public ParkingAreaTypes()
        {
            ParkingAreas = new HashSet<ParkingAreas>();
        }

        public int Id { get; set; }
        public string ParkingAreaTypeDescription { get; set; }
        public bool Inactive { get; set; }

        public ICollection<ParkingAreas> ParkingAreas { get; set; }
    }
}
