﻿using System.ComponentModel.DataAnnotations;

namespace BankAPI.Data.Model;

public class Account
{
    [MaxLength(10)]
    public string Id { get; set; } = null!;
    public double Balance { get; set; }
    public AccountType AccountType { get; set; }
    public Customer Customer { get; set; } = null!;
    public DateOnly CraetedDate { get; set; }
    public int? Term { get; set; } 
    
}
