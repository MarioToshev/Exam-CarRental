using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CarRental.Data.Identity;
using CarRental.Data;
using CarRental.Services.Interfaces;
using CarRental.Models.User;

namespace ExamTemplate.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RentalContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager,
            SignInManager<User> signInManager, RentalContext db, IUserService userService,IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = db;
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registration)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = registration.UserName,
                    FirstName = registration.FirstName,
                    SecondName = registration.SecondName,
                    LastName = registration.LastName,
                    Email = registration.Email,
                    EGN = registration.EGN,
                    PhoneNumber = registration.PhoneNumber,
                    Role = "Admin"
                };
                IdentityResult result = await this._userManager.CreateAsync(user, registration.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "User");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
            }
            return View(registration);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await this._signInManager
                    .PasswordSignInAsync(
                    login.Username,
                    login.Password,
                    false,
                    false);

                if (result.Succeeded)
                {
                    User user = _mapper.Map<User>(await _userService.GetUserByUserName(login.Username));
                    if (user.Role == "Administrator")
                    {
                        return RedirectToAction("AdminPanel", "Administration");
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Login unsuccesful");
            }
            return View(login);
        }
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("AllTasks", "Task");
        }

    }
}
