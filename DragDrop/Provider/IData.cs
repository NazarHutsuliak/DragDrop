using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragDrop.Models;

namespace DragDrop.Provider
{
    public interface IData
    {
        List<AccountsModel> GetData();
    }
}
