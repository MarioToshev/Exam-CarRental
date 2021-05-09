using CarRental.Data.Entities;
using CarRental.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Service.Interfaces
{
    public interface ICarService
    {
        public Task CreateCar(InputCarViewModel model,string url);
        public Task EditCar(CarViewModel model,string id);
        public Task DeleteCar(string carId);
        public Task<ICollection<CarViewModel>> GetAllCars();
        public Task<Car> GetCarById(string carId);
    }
}
