using HomeWork20.AuthModels;
using HomeWork20.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork20.Controllers
{
    /// <summary>
    /// Авторизация, аутентификация
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager,
                                SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        /// <summary>
        /// страница входа
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLogin { ReturnUrl = returnUrl });
        }
        /// <summary>
        /// метод входа
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin details)
        {
            if (ModelState.IsValid)//проверка на валидность модели заданной атрибутами в классе UserLogin
            {
                var loginResult = await _signInManager.PasswordSignInAsync(details.UserName, 
                                                                        details.Password,
                                                                        false,
                                                                        false);
                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(details.ReturnUrl))
                    {
                        return Redirect(details.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Persone");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь не найден");
                    return View(details);
                }
            }
            ModelState.AddModelError("", "Заполните все поля");
            return View(details);
        }
        /// <summary>
        /// страница регистрации
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new UserRegistration());
        }
        /// <summary>
        /// метод регистрации
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistration details)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = details.UserName };
                var createResult = await _userManager.CreateAsync(user, details.Password);
               

                if (createResult.Succeeded)
                {                   

                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Persone");
                }
                else//иначе
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }
            ModelState.AddModelError("", "Данные не соответствуют требуемому формату");
            return View(details);            
        }
        /// <summary>
        /// метод выхода
        /// </summary>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Persone");
        }
        /// <summary>
        /// страница запрещенного доступа
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Denied()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        /// <summary>
        /// Список пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetUsers()
        {
            ViewBag.Users = await _userManager.Users.ToListAsync();
            return View();
        }
    }
}
