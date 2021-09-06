using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using DragDrop.Models;

namespace DragDropTests
{
    public class BusinessLogicTests

    {
        [Fact]
        public void BalanseSumTest()
        {
            // Arrange
            var expectedList = new List<AccountsModelWithSumBalance>()
            {
                new AccountsModelWithSumBalance()
                {
                    Name = "Name",
                    Surname = "Surname",
                    Balance = 51,
                    Currency = "Currency",
                    RegistrationCity = "city",
                    Age = 10
                }
            };

            var expected = expectedList.Select(x => x.Balance);

            var actualList = new List<AccountsModel>()
            {
                new AccountsModel()
                {
                    Name = "Name",
                    Surname = "Surname",
                    Balance = new List<decimal> { 1, 2, 3, 45 },
                    Currency = "Currency",
                    RegistrationCity = "city",
                    Age = 10
                }
            };
            // Act
            var a = new AccountsModelWithSumBalance();
            var a2 = a.GetAccountsWithSumBalances(actualList);

            var actual = a2.Select(x => x.Balance);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
