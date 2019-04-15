using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiamiBeachPP.Models;

namespace MiamiBeachPP.Models
{
    public class SpModel
    {
        public int Id { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string LicensePlate { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Inactive { get; set; }
        public string ParkingAreaName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string ParkingAreaTypeDescription { get; set; }

        //public List<GetPermitRecords>() {
        //    UserDataAccessLayer userDataAccessLayer = new UserDataAccessLayer();
        //    userDataAccessLayer.GetPermitRecords(spModel);


        //}
        //public Nullable<double> Latitude { get; set; }
        //public Nullable<double> Longitude { get; set; }

        //public virtual ParkingPermits EffectiveDate { get; set; }
        //public virtual ParkingPermits ExpirationDate { get; set; }
        //public virtual ParkingPermits LicensePlate { get; set; }

    }
}
