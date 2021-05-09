using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Data.Identity;
using CarRental.Models.User;

namespace CarRental.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserViewModel> GetUserByUserName(string username);
        public Task<ICollection<UserViewModel>> GetAllUsers();
        public Task ChangeRole (UserViewModel model);
        public Task DeleteUser(string username);


    }
}
