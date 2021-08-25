using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DragDrop.Models
{
    public class AccountsModelWithSumBalance
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string RegistrationCity { get; set; }
        public int Age { get; set; }

        public IEnumerable<AccountsModelWithSumBalance> GetAccountsWithSumBalances(List<AccountsModel> accounts)
        {
            var accountsModelWith = accounts.Select(x => new AccountsModelWithSumBalance
            {
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
