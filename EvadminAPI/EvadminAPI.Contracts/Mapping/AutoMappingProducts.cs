using AutoMapper;
using EvadminAPI.Contracts.Contracts;
using EvadminAPI.DataBase.Models;

namespace EvadminAPI.Services.Mapping
{
	public class AutoMappingProducts : Profile
	{
		public AutoMappingProducts()
		{
			this.CreateMap<LoginContract, UserModel>();
			this.CreateMap<RegisterContract, UserModel>();
		}
	}
}
