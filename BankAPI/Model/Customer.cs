﻿namespace BankAPI.Model
{
    public class Customer
    {
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerUsername { get; set; } = string.Empty;
        public string CustomerFirstName { get; set; } = string.Empty;
        public string CustomerLastName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty; 
        public string CustomerPassword { get; set; } = string.Empty;

    }
}