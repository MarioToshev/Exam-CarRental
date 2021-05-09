using AutoMapper;
using CarRental.Data;
using CarRental.Data.Entities;
using CarRental.Data.Identity;
using CarRental.Model.Entities;
using Microsoft.EntityFrameworkCore;
using RentFormRental.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentFormRental.Service
{
    public class RentFormService : IRentFormService
    {
        private readonly IMapper _mapper;
        private readonly RentalContext _context;

        public RentFormService(IMapper mapper, RentalContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task CreateRentForm(RentFormViewModel model, string carId, string userName)
        {
            model.Id = Guid.NewGuid().ToString();
            RentForm form = _mapper.Map<RentForm>(model);
            form.Car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);
            form.User = await _context.Users.FirstOrDefaultAsync(c => c.UserName == userName);
            form.Status = await UpdateStatus(_mapper.Map<RentForm>(model));

            Car car = form.Car;
            car.IsRented = true;
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();


            form.TotalPrice = (model.EndDate - model.StartDate).TotalDays * car.PricePerDay;
            _context.RentForms.Add(form);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteRentForm(string rentFormId)
        {
            _context.RentForms.Remove(await GetRentFormById(rentFormId));
            await _context.SaveChangesAsync();
        }

        public Task EditRentForm(RentFormViewModel model, string id)
        {
            throw new NotImplementedException();
        }

        //public async task editrentform(rentformviewmodel model, string id)
        //{
        //    rentform oldrentform = await getrentformbyid(id);
        //    oldrentform.startdate = model.startdate;
        //    oldrentform.enddate = model.enddate;
        //    oldrentform.status = await updatestatus(model);
        //    // oldrentform.car = _mapper.map<car>(model.car);
        //    oldrentform.totalprice = model.totalprice;

        //    _context.rentforms.update(oldrentform);
        //    await _context.savechangesasync();
        //}
        public async Task<ICollection<RentFormViewModel>> GetAllUserRentForms(string userName)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            ICollection<RentForm> forms = await _context.RentForms.Include( r => r.Car).Where(rf => rf.UserId == user.Id).ToListAsync();
            foreach (var form in forms)
            {
                await UpdateStatus(form);
                _context.RentForms.Update(form);
                await _context.SaveChangesAsync();
            }
            return forms.Select(c => _mapper.Map<RentFormViewModel>(c)).ToList();
        }

        public async Task<RentForm> GetRentFormById(string rentFormId)
        {
            return await _context.RentForms.FirstOrDefaultAsync(c => c.Id == rentFormId);
        }

        public async Task ReturnCar(string carId, string formId)
        {
            Car car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);
            car.IsRented = false;
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            RentForm form = await _context.RentForms.FirstOrDefaultAsync(c => c.Id == formId);
            form.Status = "Used";
            await _context.SaveChangesAsync();
        }

        private async Task<string> UpdateStatus(RentForm model)
        {
            if (model.StartDate > DateTime.Now)
            {
                return "Awaiting";
            }
            else if (model.EndDate < DateTime.Now)
            {
                return "Overdue";

            }
            else if (model.EndDate > DateTime.Now && model.StartDate < DateTime.Now)
            {
                return "Active";

            }
            else if (model.EndDate > DateTime.Now && model.StartDate < DateTime.Now)
            {
                return "Used";

            }
            else
            {
                return "Error";
            }
        }
    }
}
