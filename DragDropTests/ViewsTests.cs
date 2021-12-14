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
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace DragDropTests
{
    public class ViewsTests
    {
        [Fact]
        public void DownloadFromDBTest()
        {
            //Arrange
            var mockcontext = new Mock<ApplicationContext>();
            var mockhost = new Mock<IWebHostEnvironment>();
            var controller = new HomeController(mockcontext.Object, mockhost.Object);

            //Act
            ViewResult viewResult = controller.GetTableFromDB() as ViewResult;

            //Assert
            Assert.Equal("ViewTable", viewResult.ViewName);
        }

        [Fact]
        public void IndexViewTest()
        {
            //Arrange
            var mockcontext = new Mock<ApplicationContext>();
            var mockhost = new Mock<IWebHostEnvironment>();
            var controller = new HomeController(mockcontext.Object, mockhost.Object);

            //Act

            ViewResult viewResult = controller.Index() as ViewResult;

            //Assert
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public void UncorrectStructureFileTest()
        {
            //Arrange
            var mockcontext = new Mock<ApplicationContext>();
            var mockhost = new Mock<IWebHostEnvironment>();
            var controller = new HomeController(mockcontext.Object, mockhost.Object);
            controller.ViewBag.Message = "Incorrect structure in file";

            //Act
            ViewResult viewResult = controller.ViewTable(mockcontext.Object.Accounts) as ViewResult;

            //Assert
            Assert.Equal("Index", viewResult.ViewName);

        }

        [Fact]
        public void CorrectStructureFileTest()
        {
            //Arrange
            var mockcontext = new Mock<ApplicationContext>();
            var mockhost = new Mock<IWebHostEnvironment>();
            var controller = new HomeController(mockcontext.Object, mockhost.Object);

            //Act
            ViewResult viewResult = controller.ViewTable(mockcontext.Object.Accounts) as ViewResult;

            //Assert
            Assert.Equal("ViewTable", viewResult.ViewName);

        }

        [Fact]
        public void IncorrectFormatFileTest()
        {
            //Arrange
            var mockcontext = new Mock<ApplicationContext>();
            var mockhost = new Mock<IWebHostEnvironment>();
            var mockFile = new Mock<FileModel>();
            var controller = new HomeController(mockcontext.Object, mockhost.Object);

            mockFile.Object.Path = "bank.txt";

            //Act
            controller.ValidateFile(mockFile.Object);

            //Assert
            Assert.Equal("Please choose .json or .yaml file", controller.ViewBag.Message);

        }

        [Fact]
        public void CorrectFormatFileTest()
        {
            //Arrange
            var mockcontext = new Mock<ApplicationContext>();
            var mockhost = new Mock<IWebHostEnvironment>();
            var file = new FileModel();
            var controller = new HomeController(mockcontext.Object, mockhost.Object);

            file.Path = "bank.json";

            //Act
            controller.ValidateFile(file);

            //Assert
            Assert.Equal("Add file has correct format", controller.ViewBag.Message);

        }

  
    }
}
