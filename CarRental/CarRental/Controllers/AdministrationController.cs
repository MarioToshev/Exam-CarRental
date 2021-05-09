using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using CarRental.Models.User;
using CarRental.Services.Interfaces;

namespace CarRental.Web.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IUserService _userService;

        public AdministrationController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> AllUsers()
        {
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            if(user.Role != "Admin")
            {
                return NotFound();
            }

            return View(await _userService.GetAllUsers());
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string username)
        {
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            if (user.Role != "Admin")
            {
                return NotFound();
            }
            TempData["Username"] = username;
            return View(await _userService.GetUserByUserName(username));
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(UserViewModel model)
        {
            model.Username = TempData["Username"].ToString();
            TempData.Clear();
            if (model.Role != "Client" && model.Role != "Admin")
            {
                return RedirectToAction("AllUsers","Administration");
            }
            else
            {
                await _userService.ChangeRole(model);
                return RedirectToAction("AllUsers", "Administration");
            }
        }
        public async Task<IActionResult> DeleteUser(string username)
        {
            await _userService.DeleteUser(username);
            return RedirectToAction("AllUsers", "Administration");
        }

    }
}
