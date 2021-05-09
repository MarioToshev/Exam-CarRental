using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Data;
using CarRental.Data.Identity;
using CarRental.Models.User;
using CarRental.Services.Interfaces;

namespace CarRental.Service
{
    public class UserService : IUserService
    {
        private readonly RentalContext _context;
        private readonly IMapper _mapper;


        public UserService(RentalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ChangeRole(UserViewModel model)
        {
            User oldUser =await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.Username);
            oldUser.Role = model.Role;
            _context.Update(oldUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(string username)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UserViewModel>> GetAllUsers()
        {
            return await _context.Users.Include(u => u.rentForms).Select(u => _mapper.Map<UserViewModel>(u)).ToListAsync();
        }

        public async Task<UserViewModel> GetUserByUserName(string username)
        {
            return  _mapper.Map<UserViewModel>(await _context.Users.Include(u => u.rentForms).FirstOrDefaultAsync(u => u.UserName == username));
        }
    }
}
