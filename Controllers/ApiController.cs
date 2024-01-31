﻿using Microsoft.AspNetCore.Mvc;
using MSIT155.Models;
using MSIT155Site.Models.DTO;
using System.Linq;
using System.Text;

namespace MSIT155.Controllers
{
    public class ApiController : Controller
    {
        private readonly MyDBContext _context;
        public ApiController(MyDBContext context)
        {
            _context = context;
        }

        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(a => a.City).Distinct();
            return Json(cities);
        }
        public IActionResult Districts(string city)
        {
            var districts = _context.Addresses.Where(a => a.City==city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }
        public IActionResult Roads(string districts)
        {
            var roads = _context.Addresses.Where(a => a.SiteId == districts).Select(a => a.Road).Distinct();
            return Json(roads);
        }

        public IActionResult Index()
        {
            Thread.Sleep(3000);

            //int x = 10;
            //int y = 0;
            //int z = x / y;
            return Content("Content 你好!!", "text/plain", Encoding.UTF8);
        }

        //public IActionResult Register(string name, int age = 28)
        public IActionResult Register(UserDTO _user)
        {
            if (string.IsNullOrEmpty(_user.Name))
            {
                _user.Name = "guest";
            }
            return Content($"Hello {_user.Name}, {_user.Age}歲了, 電子郵件是 {_user.Email}", "text/plain", Encoding.UTF8);
        }
        public IActionResult Avatar(int id = 1)
        {
            Member? member = _context.Members.Find(id);
            if (member != null)
            {
                byte[] img = member.FileData;
                if (img != null)
                {
                    return File(img, "image/jpeg");
                }
            }

            return NotFound();
        }
        public IActionResult CheckAccountAction(UserDTO _user)
        {
            bool check = _context.Members.Any(a=>a.Name== _user.Name);
            if (check)
                return Content("帳號已使用");
            else
                return Content("帳號可使用");
        }

    }
}
