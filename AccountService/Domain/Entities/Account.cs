﻿using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace AccountService.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public int UserId { get; set; }

    }

}
