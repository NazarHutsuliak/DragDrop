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
        public void DownloadFromDBTest()
        {
            //Arrange
            var mock = new Mock<ApplicationContext>();
            var mock1 = new Mock<IWebHostEnvironment>();
            var home = new HomeController(mock.Object,mock1.Object);



            //Act
            var actual = home.GetTableFromDB();


            
            //Assert
            Assert.Equal("ViewTable",   );
        }


    }
}
