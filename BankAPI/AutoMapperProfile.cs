using BankAPI.DTOs.Customers;
using BankAPI.Data.Model;
using AutoMapper;
using BankAPI.DTOs.Account;

namespace BankAPI;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, GetCustomerResponseDTO>();
        CreateMap<CreateCustomerRequestDTO, Customer>();
        CreateMap<Account, GetAccountResponseDTO>().ForCtorParam("OwnerId", c => c.MapFrom(s => s.Customer.Id));
    }
}
