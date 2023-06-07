using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Panel()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            using (StudioContext db = new StudioContext())
            {
                var validUser = db.Пользовательs.FirstOrDefault(u => u.Логин == login && u.Пароль == password);
                var validAdmin = db.Администраторs.FirstOrDefault(u => u.Логин == login && u.Пароль == password);
                if (validUser != null)
                {
                    ViewBag.Message = "Вы успешно вошли в аккаунт!";
                    return View("Cabinet");
                }
                else if (validAdmin != null)
                {
                    ViewBag.Message = "Вы успешно вошли в аккаунт!";
                    return View("Panel");
                }
                else
                {
                    ViewBag.Message = "Проверьте набранные данные!";
                    return View("Login");
                }
            }
        }
    
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Record()
        {
            return View();
        }
        public IActionResult NoAcc()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Cabinet()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Cabinet(string number, string idservice)
        {
            int id = 0;
            int idd = Convert.ToInt32(idservice);
            id = idd + 9;
            double number1 = Convert.ToDouble(number);

            if (id > 19 || number1 > 899999999999)
            {
                ViewBag.Message = "Введите корректные данные!";
                return View();
            }
            else
            {
                using (StudioContext db = new StudioContext())
                {
                    Запись запись = new Запись();
                    запись.Телефон = number;
                    запись.Idуслуги = id;
                    запись.Idпользователя = 12;
                    db.Add(запись);
                    db.SaveChanges();
                    ViewBag.Message = "Вы успешно оставили заявку!";
                    return View();
                }
            }
        }

        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Registration(string login, string password, string name, string surname)
        {
            using (StudioContext db = new StudioContext())
            {
                int count = (from c in db.Пользовательs where c.Логин == login select c).Count();
                if (count != 0)
                {
                    ViewBag.Message = "Аккаунт с таким логином уже существует!";
                    return View();
                }
                else if (login == null || password == null || name == null || surname == null)
                {
                    ViewBag.Message = "Заполните все поля!";
                    return View();
                }
                else
                {
                    Пользователь user = new Пользователь();
                    user.Логин = login;
                    user.Пароль = password;
                    user.Имя = name;
                    user.Фамилия = surname;
                    db.Add(user);
                    db.SaveChanges();
                    ViewBag.Message = "Вы успешно создали аккаунт!";
                    return View("Login");
                }

            }
        }

    }
}
    
       