using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiamiBeachPP.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace MiamiBeachPP.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        UserDataAccessLayer objUser = new UserDataAccessLayer();
        private PermitDBContext db = new PermitDBContext();

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("UserLogin", "Login");

        }

        [Authorize]
        public ActionResult UserHome(string searchString)
        {
            ViewBag.data = HttpContext.Session.GetString("UserRole");

            ViewData["CurrentFilter"] = searchString;

            var parkingList = (from ep in db.ParkingPermits.ToList()
                               join e in db.ParkingAreas.ToList() on ep.ParkingAreaId equals e.Id
                               join t in db.ParkingAreaTypes on e.ParkingAreaTypeId equals t.Id
                               //where ep.LicensePlate.Contains(searchString)
                               select new SpModel
                               {
                                   Id = ep.Id,
                                   Latitude = e.Latitude,
                                   Longitude = e.Longitude,
                                   DateCreated = ep.DateCreated,
                                   LicensePlate = ep.LicensePlate,
                                   ParkingAreaName = e.ParkingAreaName,
                                   Inactive = ep.Inactive,
                                   ExpirationDate = ep.ExpirationDate,
                                   EffectiveDate = ep.EffectiveDate,
                                   ParkingAreaTypeDescription = e.ParkingAreaType.ParkingAreaTypeDescription

                               });

            if (!String.IsNullOrEmpty(searchString))
            {
                parkingList = parkingList.Where(s => s.LicensePlate.ToLower().Contains(searchString.ToLower()));
            }

            if(ViewBag.data == "servRep")
            {
                return View("MasterDetails", parkingList.ToList());
            }

            return View(parkingList.ToList());
        }

        [Authorize]
        //GET Areas By Type
        public IActionResult DisplayAreabyType()
        {
            ViewBag.data = HttpContext.Session.GetString("UserRole");
            var parkingList = (from ep in db.ParkingPermits.ToList()
                               join e in db.ParkingAreas.ToList() on ep.ParkingAreaId equals e.Id
                               join t in db.ParkingAreaTypes on e.ParkingAreaTypeId equals t.Id
                               //where ep.LicensePlate.Contains(searchString)
                               select new SpModel
                               {
                                   Id = ep.Id,
                                   Latitude = e.Latitude,
                                   Longitude = e.Longitude,
                                   DateCreated = ep.DateCreated,
                                   LicensePlate = ep.LicensePlate,
                                   ParkingAreaName = e.ParkingAreaName,
                                   Inactive = ep.Inactive,
                                   ExpirationDate = ep.ExpirationDate,
                                   EffectiveDate = ep.EffectiveDate,
                                   ParkingAreaTypeDescription = e.ParkingAreaType.ParkingAreaTypeDescription

                               });

            return View(parkingList.ToList());
        }

        //GET parking permit by area
        [Authorize]
        public IActionResult DisplayParkingPermitByArea()
        {
            ViewBag.data = HttpContext.Session.GetString("UserRole");
            var parkingList = (from ep in db.ParkingPermits.ToList()
                               join e in db.ParkingAreas.ToList() on ep.ParkingAreaId equals e.Id
                               join t in db.ParkingAreaTypes on e.ParkingAreaTypeId equals t.Id
                               //where ep.LicensePlate.Contains(searchString)
                               select new SpModel
                               {
                                   Id = ep.Id,
                                   Latitude = e.Latitude,
                                   Longitude = e.Longitude,
                                   DateCreated = ep.DateCreated,
                                   LicensePlate = ep.LicensePlate,
                                   ParkingAreaName = e.ParkingAreaName,
                                   Inactive = ep.Inactive,
                                   ExpirationDate = ep.ExpirationDate,
                                   EffectiveDate = ep.EffectiveDate,
                                   ParkingAreaTypeDescription = e.ParkingAreaType.ParkingAreaTypeDescription

                               });

            return View(parkingList.ToList());
        }

        [Authorize]
        public ActionResult PermitExpiration()
        {
            ViewBag.data = HttpContext.Session.GetString("UserRole");
            var parkingList = (from ep in db.ParkingPermits.ToList()
                               join e in db.ParkingAreas.ToList() on ep.ParkingAreaId equals e.Id
                               join t in db.ParkingAreaTypes on e.ParkingAreaTypeId equals t.Id
                               select new SpModel
                               {
                                   Id = ep.Id,
                                   Latitude = e.Latitude,
                                   Longitude = e.Longitude,
                                   DateCreated = ep.DateCreated,
                                   LicensePlate = ep.LicensePlate,
                                   ParkingAreaName = e.ParkingAreaName,
                                   Inactive = ep.Inactive,
                                   ExpirationDate = ep.ExpirationDate,
                                   EffectiveDate = ep.EffectiveDate,
                                   ParkingAreaTypeDescription = e.ParkingAreaType.ParkingAreaTypeDescription

                               });

            return View(parkingList.ToList());
        }

        [Authorize]
        // GET: /Edit/5
        public ActionResult Edit(int? id)
        {

            ViewBag.data = HttpContext.Session.GetString("UserRole");
           
            var parkingpermits = db.ParkingPermits.FirstOrDefault(s => s.Id == id);
           // ParkingPermits pp = db.ParkingPermits.Find(id);
            if (id == null)
            {
                return new NotFoundResult();
             }

            if (parkingpermits == null)
            {
                return new NotFoundResult();
            }
            return View(parkingpermits);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Inactive, EffectiveDate, ExpirationDate")] ParkingPermits spModel)
        {
            ViewBag.data = HttpContext.Session.GetString("UserRole");

            var parkingpermits = db.ParkingPermits.FirstOrDefault(s => s.Id == id);

            if (parkingpermits == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //db.ParkingPermits.Add(new ParkingPermits()
                //{
                //    Inactive = spModel.Inactive
                //});

                //db.ParkingPermits.Update(new ParkingPermits()
                //{
                //    Inactive = spModel.Inactive,
                //    EffectiveDate = spModel.EffectiveDate,
                //    ExpirationDate=spModel.ExpirationDate,
                //    DateCreated = DateTime.Now
                //});
                parkingpermits.Inactive = spModel.Inactive;
                parkingpermits.EffectiveDate = spModel.EffectiveDate;
                parkingpermits.ExpirationDate = spModel.ExpirationDate;
               // parkingpermits.DateCreated = spModel.DateCreated;
               // db.SaveChanges();



              //  db.Entry(spModel).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("UserHome");
             }
            
            else
            {
                return View("Edit", spModel);
            }
        }

    }
}