using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using DragDrop.Provider;
using DragDrop.Models;

namespace DragDrop.Adapters
{
    public class AdapterFromJson : IData
    {
        private readonly string _content;
        public AdapterFromJson(FileModel file)
        {
            var pathToFile = @"C:\Users\nhutsuliak\source\repos\DragDrop\DragDrop\wwwroot\" + file.Path;
            _content = File.ReadAllText(pathToFile);
        }

        public List<AccountsModel> GetData()
        {

            return JsonSerializer.Deserialize<List<AccountsModel>>(_content);
        }
    }
}
