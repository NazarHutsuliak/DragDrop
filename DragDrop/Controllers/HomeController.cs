using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DragDrop.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using DragDrop.Provider;
namespace DragDrop.Controllers
{
    public class HomeController : Controller
    {
       readonly ApplicationContext _context;
       readonly IWebHostEnvironment _appEnvironment;
        public HomeController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View(_context.Files.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddAndParseFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {

                string path = @"\Files\" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                if (Path.GetExtension(path) == ".json" || Path.GetExtension(path) == ".yaml")
                {

                    FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                    _context.Files.Add(file);
                    _context.SaveChanges();

                    try
                    {
                        DrawTable(file);
                        return View("DrawTable");

                    }
                    catch 
                    {
                        ViewBag.Message = "Uncorrect structure in file ";
                        return View("Index");
                    }


                }
                ViewBag.Message = "PLease choose .json or .yaml file";
                return View("Index");
            }

            ViewBag.Message = "PLease choose file";
            return View("Index");

        }

        public IActionResult DrawTable(FileModel file)
        {

            var dataProvider = DataProvider.Create(file);
            var accounts = dataProvider.GetData();

            var accountsWithSum = new AccountsModelWithSumBalance();
            var result = accountsWithSum.GetAccountsWithSumBalances(accounts);

            return View(result);

        }
    }
}
