using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DragDrop.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using DragDrop.Provider;
using System.Collections.Generic;
using System.Threading;

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
            return View("ViewTable", _context.Accounts);
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

                var file = SaveFileToBD(uploadedFile, path);

                ValidateFile(file);

                if (ViewBag.Message == "Add file has correct format")
                {
                    var result = GetTable(file);

                    if (ViewBag.Message != "Uncorrect structure in file")
                    {
                        SaveTableToBD(result);
                    }

                    return ViewTable(result);

                }
                return View("Index");

            }

            ViewBag.Message = "Please choose file";
            return View("Index");
        }

        public FileModel SaveFileToBD(IFormFile uploadedFile, string path)
        {
            FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
            _context.Files.Add(file);
            _context.SaveChanges();
            return file;
        }

        public void ValidateFile(FileModel file)
        {
            if (Path.GetExtension(file.Path) == ".json" || Path.GetExtension(file.Path) == ".yaml")
            {
                ViewBag.Message = "Add file has correct format";

            }
            else
                ViewBag.Message = "Please choose .json or .yaml file";
        }

        public IEnumerable<AccountsModelWithSumBalance> GetTable(FileModel file)
        {
            try
            {
                var dataProvider = DataProvider.Create(file);
                var accounts = dataProvider.GetData();

                var accountsWithSum = new AccountsModelWithSumBalance();

                return accountsWithSum.GetAccountsWithSumBalances(accounts);

            }
            catch
            {
                ViewBag.Message = "Uncorrect structure in file";
                return null;

            }


        }
        public IActionResult ViewTable(IEnumerable<AccountsModelWithSumBalance> result)
        {
            if (ViewBag.Message == "Uncorrect structure in file")
            {
                return View("Index");
            }
            return View("ViewTable", result);

        }

        public void SaveTableToBD(IEnumerable<AccountsModelWithSumBalance> result)
        {
            foreach (AccountsModelWithSumBalance resultmodel in result)
            {
                _context.Accounts.Add(resultmodel);
                _context.SaveChanges();
            }
        }

    }
}
