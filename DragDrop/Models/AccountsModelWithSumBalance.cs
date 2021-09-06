﻿using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace DragDrop.Models
{
    public class AccountsModelWithSumBalance
    {
        [Key]
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string RegistrationCity { get; set; }
        public int Age { get; set; }

        public IEnumerable<AccountsModelWithSumBalance> GetAccountsWithSumBalances(List<AccountsModel> accounts)
        {
            var accountsModelWith = accounts.Select(x => new AccountsModelWithSumBalance
            {   AccountId = AccountId,
                Name = x.Name,
                Surname = x.Surname,
                Balance = x.Balance.ToArray().Sum(),
                Currency = x.Currency,
                RegistrationCity = x.RegistrationCity,
                Age = x.Age
            });

            return accountsModelWith;
        }
    }
}
