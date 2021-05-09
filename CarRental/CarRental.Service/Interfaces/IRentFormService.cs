using CarRental.Data.Entities;
using CarRental.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentFormRental.Service.Interfaces
{
    public interface IRentFormService
    {
        public Task CreateRentForm(RentFormViewModel model, string carId,string userName);
        public Task EditRentForm(RentFormViewModel model, string id);
        public Task DeleteRentForm(string rentFormId);
        public Task<ICollection<RentFormViewModel>> GetAllUserRentForms(string username);
        public Task<RentForm> GetRentFormById(string rentFormId);
        public Task ReturnCar(string carId, string formId);
    }
}
