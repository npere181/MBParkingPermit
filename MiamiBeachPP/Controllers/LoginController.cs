using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MiamiBeachPP.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace MiamiBeachPP.Controllers
{
   // [Authorize]
    public class LoginController : Controller
    {
        UserDataAccessLayer objUser = new UserDataAccessLayer();

        [HttpGet]
        public IActionResult RegisterUser()
        {
            ViewBag.data = HttpContext.Session.GetString("UserRole");
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser([Bind] UserDetails user)
        {
            ViewBag.data = HttpContext.Session.GetString("UserRole");
            if (ModelState.IsValid)
            {
                string RegistrationStatus = objUser.RegisterUser(user);
                if (RegistrationStatus == "Success")
                {
                    ModelState.Clear();
                    TempData["Success"] = "Registration Successful!";
                    return View();
                }
                else
                {
                    TempData["Fail"] = "This User ID already exists. Registration Failed.";
                    return View();
                }
            }
            return View();
        }

        //[HttpGet]
        //[AllowAnonymous]
        public IActionResult UserLogin()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin([Bind] UserDetails user)
        {
            UserValitdator uv = new UserValitdator();

            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("UserRole");

            if (ModelState.IsValid)
            {
                string UserRole = objUser.ValidateLogin(user);
                

                if (UserRole != "")
                {
                    
                    uv.UserID = user.UserID;
                    uv.UserRole = UserRole;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserID)
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                   
                   
                    HttpContext.Session.SetString("UserRole", UserRole.ToString());
                    return RedirectToAction("UserHome", "User");
                }
                else
                {
                    TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";
                    return View();
                }
            }
            else
                return View();

        }
    }
}
