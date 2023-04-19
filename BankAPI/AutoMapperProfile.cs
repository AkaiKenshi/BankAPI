using BankAPI.DTOs.Customer;
using BankAPI.Model;
using AutoMapper;

namespace BankAPI;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, GetCustomerResponseDTO>();
        CreateMap<CreateCustomerRequestDTO, Customer>();
    }
}
