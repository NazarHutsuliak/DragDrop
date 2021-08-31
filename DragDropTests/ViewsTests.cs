using System.IO;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using DragDrop.Controllers;
using DragDrop.Models;
using Moq;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;
using System;

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
            var expected = new Mock<IActionResult>();

            //Act
            var actual = new Mock<HomeController>(mock.Object, mock1.Object).Object.GetTableFromDB();
            
            //Assert
            Assert.IsType(expected.Object, actual );
        }


    }
}
