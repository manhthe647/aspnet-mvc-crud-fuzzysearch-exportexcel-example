using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.WebAppMvc.Models;
using QuanLySinhVien.WebAppMvc.ViewModel;

namespace QuanLySinhVien.WebAppMvc.Controllers
{
    public class UserController: Controller
    {
        private readonly AppDbContext _dbContext;
        public UserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

   

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind("Username, PasswordHash")] UserVm user)
        {
            if (ModelState.IsValid)
            {
                if (isValidUser(user.Username, user.PasswordHash))
                {
                    // Xác thực thành công
                    string token = GenerateToken();

                    HttpContext.Session.SetString("Token", token);
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin không hợp lệ");
                }
            }

            return View("Login", user);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "User");

        }

        private bool isValidUser(string username, string password)
        {
            
            var result= _dbContext.Users.FirstOrDefault(u=>u.Username == username && u.PasswordHash==password);

          
            return result != null;
        }

        private string GenerateToken()
        {
            string token = Guid.NewGuid().ToString();

            return token;
        }
    }
}
