using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.WebAppMvc.Models;
using System.Diagnostics;

namespace QuanLySinhVien.WebAppMvc.Controllers
{
    public class HomeController : BaseController
    {
        
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Student");
        }

      
    }
}