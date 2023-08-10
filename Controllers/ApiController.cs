using AJAX.Models;
using AJAX.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace AJAX.Controllers
{
    public class ApiController : Controller
    {

        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;
        
        public ApiController(DemoContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        public IActionResult Index()
        {
            
            return Content("Hello Fetch!!");
        }
        public IActionResult getdemo(CUserInfoViewModel user) 
        {
           
            if (string.IsNullOrEmpty(user.name))
            {
                user.name = "guest";
            }

            return Content($"Hello {user.name}! You are {user.age} years old.");
        }
        public IActionResult register(Members member, IFormFile file)
        {
            if (file != null)
            {
                string filePath = Path.Combine(_host.WebRootPath, "uploads", file.FileName);
                using (var filestrem = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(filestrem);
                }
                byte[]? imgBytes = null;
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    imgBytes = memoryStream.ToArray();
                }
              
                member.FileName = file.FileName;
                member.FileData = imgBytes;
            }
            _context.Members.Add(member);
            _context.SaveChanges();
            return Content("註冊成功!");
          
        }
        public IActionResult getImgById(int id = 1) 
        {
         
            Members? member = _context.Members.Find(id);
            byte[]? img = member.FileData;
            return File(img, "image/jpg");
        }
        public IActionResult City()
        {
            var cities = _context.Address.Select(c => c.City).Distinct();
            return Json(cities);
        }
        public IActionResult District(string city)
        {
            var districts = _context.Address.Where(c => c.City == city).Select(c => c.SiteId).Distinct();
            return Json(districts);
        }
        public IActionResult Road(string siteId)
        {
            var roads = _context.Address.Where(c => c.SiteId == siteId).Select(c => c.Road).Distinct();
            return Json(roads);
        }
    }
}
