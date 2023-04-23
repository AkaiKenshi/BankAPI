using BankAPI.DTOs.Customer;
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
        CreateMap<CreateCheckingAccountRequestDTO, Account>();
        CreateMap<CreateSavingsAccountRequestDTO, Account>();
        CreateMap<CreateFixedTermInvestmentAccountRequestDTO, Account>();
        CreateMap<Account, GetAccountResponseDTO>().ForCtorParam("OwnerId", c => c.MapFrom(s => s.Customer.Id));
    }
}
