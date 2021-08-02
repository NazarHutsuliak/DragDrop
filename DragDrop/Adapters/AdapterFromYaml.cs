using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using DragDrop.Provider;
using DragDrop.Models;
namespace DragDrop.Adapters
{
    public class AdapterFromYaml : IData
    {
        private readonly string _content;
        public AdapterFromYaml(FileModel file)
        {
            var pathToFile = @"C:\Users\nhutsuliak\source\repos\DragDrop\DragDrop\wwwroot\" + file.Path;
            _content = File.ReadAllText(pathToFile);
        }

        public List<AccountsModel> GetData()
        {
            var deserializer = new Deserializer();
            return deserializer.Deserialize<List<AccountsModel>>(_content);
        }
    }
}
