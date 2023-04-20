using BankAPI.DTOs.Customer;
using BankAPI.Model;
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
        CreateMap<Account, GetAccountResponse>();
    }
}
