using BankAPI.DTOs.Customer;
using BankAPI.Model;

namespace BankAPI;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, GetCustomerResponseDTO>();
        CreateMap<CreateCustomerRequestDTO, GetCustomerResponseDTO>();
    }
}
