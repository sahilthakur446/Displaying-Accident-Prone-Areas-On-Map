using Displaying_AccidentProneAreasOnMap.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol.Plugins;

namespace Displaying_AccidentProneAreasOnMap.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserDbContext obj;

        public HomeController(UserDbContext obj)
        {
            this.obj = obj;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                
                return RedirectToAction("Dashboard");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(User userData)
        {
            var user = obj.Users.FirstOrDefault(x => x.Email == userData.Email && x.Password == userData.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserSession", userData.Email);
                TempData["Success"] = "Login Successful";
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "Login Failed";
            }
            
            return View();
        }
        public IActionResult Dashboard()
        {
            ViewBag.Message = HttpContext.Session.GetString("UserSession").ToString();
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
        public IActionResult Map()
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Signup(User userData)
        {
            var user = obj.Users.Where((x) => x.Email == userData.Email).FirstOrDefault();
            if (user != null)
            {
                ModelState.AddModelError("Email", "Email already in use");
               
                return View();
            }
            obj.Users.Add(userData);
            obj.SaveChanges();
            TempData["Success"] = "Signup Successfull";
            return RedirectToAction("Login");
        }       
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}