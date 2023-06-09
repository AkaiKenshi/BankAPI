﻿using BankAPI.Data.Model;
using AutoMapper;
using BankAPI.Contracts.DTOs.Customers;
using BankAPI.Contracts.DTOs.Accounts;

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
