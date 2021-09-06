using System.Collections.Generic;
using DragDrop.Models;

namespace DragDrop.Provider
{
    public interface IData
    {
        List<AccountsModel> GetData();
    }
}
