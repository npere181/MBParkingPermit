using System;
using System.Collections.Generic;

namespace MiamiBeachPP.Models
{
    public partial class ParkingPermits
    {
        public int Id { get; set; }
        public int ParkingAreaId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string LicensePlate { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Inactive { get; set; }

        public virtual ParkingAreas ParkingArea { get; set; }
       
    }
}
