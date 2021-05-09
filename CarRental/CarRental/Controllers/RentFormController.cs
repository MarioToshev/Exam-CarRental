using AutoMapper;
using CarRental.Model.Entities;
using CarRental.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RentFormRental.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    public class RentFormController : Controller
    {
        private readonly IRentFormService _rentFormService;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public RentFormController(IRentFormService rentFormService, IMapper mapper, ICarService carService)
        {
            _rentFormService = rentFormService;
            _carService = carService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> CreateForm(string carId)
        {
            TempData["CarId"] = carId;
            RentFormViewModel model = new RentFormViewModel();
            model.Car = _mapper.Map<CarViewModel>(await _carService.GetCarById(carId));
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateForm(RentFormViewModel model)
        {

            await _rentFormService.CreateRentForm(model,TempData["CarId"].ToString(),User.Identity.Name);
            TempData.Clear();
            return RedirectToAction("AllRentForms", "RentForm");
        }
        public async Task<IActionResult> AllRentForms()
        {
            return View(await _rentFormService.GetAllUserRentForms(User.Identity.Name));
        }

        public async Task<IActionResult> ReturnCar(string carId, string formId)
        {
            await _rentFormService.ReturnCar(carId, formId);
            return RedirectToAction("Index","Home");
        }
    }
}
