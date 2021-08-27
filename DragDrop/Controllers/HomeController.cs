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

        [HttpGet]
        public IActionResult GetTableFromDB()
        {
            return View("GetTable", _context.Accounts);
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

                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();

                return ValidateAndGetTable(file);

            }

            ViewBag.Message = "Please choose file";
            return View("Index");

        }

        public IActionResult ValidateAndGetTable(FileModel file)
        {
            if (Path.GetExtension(file.Path) == ".json" || Path.GetExtension(file.Path) == ".yaml")
            {
                try
                {
                    GetAndSaveTable(file);
                    return View("GetTable");
                }
                catch
                {
                    ViewBag.Message = "Uncorrect structure in file ";
                    return View("Index");
                }

            }

            ViewBag.Message = "Please choose .json or .yaml file";
            return View("Index");


        }

        public IActionResult GetAndSaveTable(FileModel file)
        {

            var dataProvider = DataProvider.Create(file);
            var accounts = dataProvider.GetData();

            var accountsWithSum = new AccountsModelWithSumBalance();
            var result = accountsWithSum.GetAccountsWithSumBalances(accounts);

            foreach (AccountsModelWithSumBalance resultmodel in result)
            {
                _context.Accounts.Add(resultmodel);
                _context.SaveChanges();
            }

            return View("GetTable" , result);

        }

    }
}
