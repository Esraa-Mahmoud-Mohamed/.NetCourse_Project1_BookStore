﻿using Microsoft.AspNetCore.Identity;

namespace BookStore.Models
{
    public class Customer:IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        
    }
}