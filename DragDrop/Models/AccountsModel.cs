using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragDrop.Models
{
    public class AccountsModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<decimal> Balance { get; set; }
        public string Currency { get; set; }
        public string RegistrationCity { get; set; }
        public int Age { get; set; }

    }
}
