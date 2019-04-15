using System;
using System.Collections.Generic;

namespace MiamiBeachPP.Models
{
    public partial class ParkingAreas
    {
        public ParkingAreas()
        {
            ParkingPermits = new HashSet<ParkingPermits>();
        }

        public int Id { get; set; }
        public int ParkingAreaTypeId { get; set; }
        public string ParkingAreaName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string DateCreated { get; set; }
        public bool Inactive { get; set; }

        public ParkingAreaTypes ParkingAreaType { get; set; }
        public ICollection<ParkingPermits> ParkingPermits { get; set; }
    }
}
