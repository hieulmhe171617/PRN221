using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OnlineMedicine.Hubs;
using OnlineMedicine.Models;
using OnlineMedicine.ViewModels;
using System;

namespace OnlineMedicine.Controllers
{
    public class MedicineController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<SignalrServer> _hubContext;

        public MedicineController(AppDbContext context, IHubContext<SignalrServer> _hubContext)
        {
            _context = context;
            this._hubContext = _hubContext;
        }

        // GET: Medicine
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Medicine> medicines = _context.Medicines.Where(x => x.Quantity > 0)
                                    .Include(m => m.Category)
                                    .Include(m => m.Country)
                                    .ToList();

            List<Category> categories = _context.Categories
                                        .ToList();

            List<Country> countries = _context.Countries.ToList();
            MedicineListModel model = new MedicineListModel
            {
                Medicines = medicines,
                Categories = categories,
                Countries = countries
            };

            return View(model);
        }

        // GET: Medicine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(List<int>? CategoryIds, string? SearchKeyword, int? countryId, int sortType)
        {
            if (SearchKeyword == null || SearchKeyword == string.Empty)
            {
                SearchKeyword = "";
            }
            List<Medicine> medicines = _context.Medicines.Where(x => x.Quantity > 0)
                                    .Include(m => m.Category)
                                    .Include(m => m.Country)
                                    .Where(m => CategoryIds == null || CategoryIds.Count <= 0 || CategoryIds.Contains(m.CategoryId))
                                    .Where(m => m.Name.Contains(SearchKeyword)
                                                || m.Descript.Contains(SearchKeyword)
                                                || m.Country.Name.Contains(SearchKeyword))
                                    .ToList();
            if (countryId != null && countryId != 0)
            {
                medicines = medicines.Where(x => x.CountryId == countryId).ToList();
            }
            if (sortType == 0)
            {
                medicines = medicines.OrderBy(x => x.Name.ToLower()).ToList();
            }
            else
            {
                medicines = medicines.OrderByDescending(x => x.Name.ToLower()).ToList();
            }

            List<Category> categories = _context.Categories
                                        .ToList();
            List<Country> countries = _context.Countries.ToList();

            MedicineListModel model = new MedicineListModel
            {
                CategoryIds = CategoryIds,
                Medicines = medicines,
                Categories = categories,
                Countries = countries,
                CountryId = countryId,
                SortType = sortType
            };

            return View(model);
        }
        public IActionResult Details(int? id)
        {
            Medicine m = _context.Medicines.Include(x => x.Country).
                            Include(x => x.Type).Include(x => x.Category).
                            FirstOrDefault(x => x.Id == id);
            List<Medicine> listM = _context.Medicines.Where(x => x.Id != id).OrderBy(x => Guid.NewGuid()).Take(3).ToList();
            ViewBag.m = m;
            ViewBag.listM = listM;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ListAll()
        {
            ViewBag.Medicine = _context.Medicines.Include(x => x.Category).Include(x => x.Type).Include(x => x.Country).ToList();
            return View("List");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.category = _context.Categories.ToList();
            ViewBag.type = _context.Types.ToList();
            ViewBag.country = _context.Countries.ToList();
            _hubContext.Clients.All.SendAsync("LoadMedicines");
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(string name, int category, DateTime expriedDate, string image, string descript, int minAge,
            int typeId, int country, int price, int quantity)
        {
            Medicine m = new Medicine();
            m.Name = name;
            m.CategoryId = category;
            m.ExpiredDate = expriedDate;
            m.Image = image;
            m.Descript = descript;
            m.MinAge = minAge;
            m.TypeId = typeId;
            m.Price = price;
            m.Quantity = quantity;
            m.CountryId = country;
            _context.Medicines.Add(m);
            if (_context.SaveChanges() > 0)
            {
                ViewBag.Medicine = _context.Medicines.Include(x => x.Category).Include(x => x.Type).Include(x => x.Country).ToList();
                _hubContext.Clients.All.SendAsync("LoadMedicines");

                return View("List");
            }
            ViewBag.Error = "Create new failed";
            return View("Create");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int mid)
        {
            Medicine m = _context.Medicines.Include(x => x.Category)
                .Include(x => x.Type).Include(x => x.Country)
                .FirstOrDefault(x => x.Id == mid);
            ViewBag.category = _context.Categories.Where(x => x.Id != m.CategoryId).ToList();
            ViewBag.type = _context.Types.Where(x => x.Id != m.TypeId).ToList();
            ViewBag.country = _context.Countries.Where(x => x.Id != m.CountryId).ToList();
            ViewBag.date = m.ExpiredDate.Value.ToString("yyyy-MM-dd");
            ViewBag.Medicine = m;
            _hubContext.Clients.All.SendAsync("LoadMedicines");
            return View("Edit");
        }
        [HttpPost]
        public IActionResult Edit(string name, int category, DateTime expriedDate, string image, string descript, int minAge,
           int typeId, int country, decimal price, int quantity, int mid)
        {
            Medicine m = _context.Medicines.Include(x => x.Category)
                .Include(x => x.Type).Include(x => x.Country).FirstOrDefault(x => x.Id == mid);
            m.Name = name;
            m.CategoryId = category;
            m.ExpiredDate = expriedDate;
            m.Image = image;
            m.Descript = descript;
            m.MinAge = minAge;
            m.TypeId = typeId;
            m.Price = price;
            m.Quantity = quantity;
            m.CountryId = country;
            if (_context.SaveChanges() >= 0)
            {
                m = _context.Medicines.Include(x => x.Category)
               .Include(x => x.Type).Include(x => x.Country).FirstOrDefault(x => x.Id == mid);
                ViewBag.category = _context.Categories.Where(x => x.Id != m.CategoryId).ToList();
                ViewBag.type = _context.Types.Where(x => x.Id != m.TypeId).ToList();
                ViewBag.country = _context.Countries.Where(x => x.Id != m.CountryId).ToList();
                ViewBag.date = m.ExpiredDate.Value.ToString("yyyy-MM-dd");
                ViewBag.Medicine = m;
                _hubContext.Clients.All.SendAsync("LoadMedicines");
                return View("Edit");
            }
            return View("Edit");
        }
        public IActionResult Delete(int mid)
        {

            Medicine m = _context.Medicines.FirstOrDefault(x => x.Id == mid);
            m.Quantity = 0;
            if (_context.SaveChanges() >= 0)
            {
                ViewBag.Medicine = _context.Medicines.Include(x => x.Category).Include(x => x.Type).Include(x => x.Country).ToList();
                return View("List");
            }
            ViewBag.Medicine = _context.Medicines.Include(x => x.Category).Include(x => x.Type).Include(x => x.Country).ToList();
            _hubContext.Clients.All.SendAsync("LoadMedicines");

            return View("List");

        }


        [HttpGet]
        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var medicines = _context.Medicines.Include(x => x.Country).Include(x => x.Category).Include(x => x.Type).ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Medicines");

                // Adding headers
                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Image";
                worksheet.Cells[1, 3].Value = "Category";
                worksheet.Cells[1, 4].Value = "Type";
                worksheet.Cells[1, 5].Value = "Country";
                worksheet.Cells[1, 6].Value = "Price";
                worksheet.Cells[1, 7].Value = "Quantity";
                worksheet.Cells[1, 8].Value = "ExpiredDate";
                worksheet.Cells[1, 9].Value = "MinAge";

                // Adding data
                int row = 2;
                foreach (var item in medicines)
                {
                    worksheet.Cells[row, 1].Value = item.Name;
                    worksheet.Cells[row, 2].Value = item.Image; // Image URL as a hyperlink
                    worksheet.Cells[row, 3].Value = item.Category.Name;
                    worksheet.Cells[row, 4].Value = item.Type.Name;
                    worksheet.Cells[row, 5].Value = item.Country.Name;
                    worksheet.Cells[row, 6].Value = item.Price;
                    worksheet.Cells[row, 7].Value = item.Quantity;
                    worksheet.Cells[row, 8].Value = item.ExpiredDate.Value.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 9].Value = item.MinAge;
                    row++;
                }

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "medicines_data.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        [HttpGet]
        public IActionResult GetMedicines()
        {
            var medicines = _context.Medicines
       .Select(x => new
       {
           x.Id,
           x.Name,
           x.Image,
           x.Price,
           x.Quantity,
           ExpiredDate = x.ExpiredDate.ToString(),
           x.MinAge,
           CategoryName = x.Category.Name,
           CountryName = x.Country.Name,
           TypeName = x.Type.Name

       })
       .ToList();

            return Ok(medicines);
        }
    }
}
