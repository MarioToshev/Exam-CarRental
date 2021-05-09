using AutoMapper;
using CarRental.Data;
using CarRental.Data.Entities;
using CarRental.Model.Entities;
using CarRental.Service.Interfaces;
using CarRental.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Service
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly RentalContext _context;
      

        public CarService(IMapper mapper, RentalContext context)
        {
            _mapper = mapper;
            _context = context;
          
        }
        public async Task CreateCar(InputCarViewModel model, string url)
        {
            Car car = new Car
            {
                Id = Guid.NewGuid().ToString(),
                IsRented = false,
                PhotoUrl = url,
                YearOfCreation = model.YearOfCreation,
                Description = model.Description,
                PricePerDay = model.PricePerDay,
                Model = model.Model,
                PassagerPlaces = model.PassagerPlaces
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCar(string carId)
        {
            _context.Cars.Remove(await GetCarById(carId));
            await _context.SaveChangesAsync();
        }
        public async Task EditCar(CarViewModel model, string id)
        {
            Car oldCar = await GetCarById(id);
            oldCar.Model = model.Model;
            oldCar.Description = model.Description;
            oldCar.PassagerPlaces = model.PassagerPlaces;
            oldCar.PhotoUrl = model.PhotoUrl;
            oldCar.PricePerDay = model.PricePerDay;
            oldCar.YearOfCreation = model.YearOfCreation;
            _context.Cars.Update(oldCar);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<CarViewModel>> GetAllCars()
        {
            return  await _context.Cars.Select(c => _mapper.Map<CarViewModel>(c)).ToListAsync();
        }
        public async Task<Car> GetCarById(string carId)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        }
    }
}
