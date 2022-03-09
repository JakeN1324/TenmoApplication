﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Account
    {
        
        public int UserId { get; set; } 

        public decimal Balance { get; set; } = 1000M;       
    }
}