using AutoMapper;
using CarRental.Model.Entities;
using CarRental.Service.Interfaces;
using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public CarController(ICarService carService, IMapper mapper, ICloudinaryService cloudinaryService,IUserService userService)
        {
            _carService = carService;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _userService = userService;
        }

        public async Task<IActionResult> AllCars()
        {
            return View(await _carService.GetAllCars());
        }
        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            if (user.Role != "Admin")
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar(InputCarViewModel car)
        {

            await _carService.CreateCar(car, await _cloudinaryService.UploadImage(car.PhotoUrl));
            return RedirectToAction("AllCars","Car");
        }
        [HttpGet]
        public async Task<IActionResult> EditCar(string carId)
        {
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            if (user.Role != "Admin")
            {
                return NotFound();
            }
            TempData["CarId"] = carId;
            return View(_mapper.Map<CarViewModel>(await _carService.GetCarById(carId)));
        }
        [HttpPost]
        public async Task<IActionResult> EditCar(CarViewModel car)
        {
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            if (user.Role != "Admin")
            {
                return NotFound();
            }
            await _carService.EditCar(car, TempData["CarId"].ToString());
            TempData.Clear();
            return RedirectToAction("AllCars", "Car");
        }

        public async Task<IActionResult> DeleteCar(string carId)
        {
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            if (user.Role != "Admin")
            {
                return NotFound();
            }
            await _carService.DeleteCar(carId);
            return RedirectToAction("AllCars", "Car");
        }
   
        public async Task<IActionResult> DetailsCar(string carId)
        {
            return View(_mapper.Map<CarViewModel>(await _carService.GetCarById(carId)));
        }
       

    }
}
