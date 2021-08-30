using System.IO;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using DragDrop.Controllers;
using DragDrop.Models;
using Moq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DragDropTests
{
    public class ViewsTests
    {
        [Fact]
        public async void DownloadFromDBTest()
        {
            //Arrange
            var mock1 = new Mock<ApplicationContext>();
            var mock2 = new Mock<IWebHostEnvironment>();
            var mock3 = new Mock<MockFile>();
            var mock4 = new Mock<FileStream>();
            mock3.Object.FileName = @"C:\Users\nhutsuliak\source\repos\DragDrop\DragDrop\wwwroot\Files\bank.json";


            var b = new HomeController(mock1.Object, mock2.Object);


            //Act
            var mock5 = new Mock<Stream>();
            await mock3.Object.CopyToAsync(mock5.Object);
            await b.AddAndParseFile(mock3.Object);


            //Assert
            //Assert.Equal();
        }


    }
}
