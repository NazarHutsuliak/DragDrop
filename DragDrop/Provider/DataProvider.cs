using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragDrop.Adapters;
using DragDrop.Models;
namespace DragDrop.Provider
{
    public class DataProvider
    {
        public static IData Create(FileModel file)
        {
            var pathToFile = @"C:\Users\nhutsuliak\source\repos\DragDrop\DragDrop\wwwroot\F" + file.Path;
            return Path.GetExtension(pathToFile) switch
            {
                ".json" => new AdapterFromJson(file),
                ".yaml" => new AdapterFromYaml(file),
                _ => throw new Exception("Unknown file format"),
            };
        }
    }
}
