using AutoMapper;
using CarRental.Data.Identity;
using CarRental.Models.User;
using CarRental.Data.Entities;
using CarRental.Model.Entities;

namespace CarRental.Web.Configuraton
{
    public class UserMapping:Profile
    {
		public UserMapping()
		{
			CreateMap<User, LoginViewModel>();
			CreateMap<LoginViewModel, User>();

			CreateMap<User, RegisterViewModel>();
			CreateMap<RegisterViewModel, User>();

			CreateMap<User, UserViewModel>();
			CreateMap<UserViewModel, User>();

			CreateMap<Car, CarViewModel>();
			CreateMap<CarViewModel, Car>();

			CreateMap<RentForm, RentFormViewModel>();
			CreateMap<RentFormViewModel, RentForm>();

			CreateMap<RentStatus, RentStatusViewModel>();
			CreateMap<RentStatusViewModel, RentStatus>();

		}
	}
}
