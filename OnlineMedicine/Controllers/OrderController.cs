using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMedicine.Models;
using System.Security.Claims;
using System.Security.Principal;


namespace OnlineShopMedicine.Controllers
{

    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        public int GetId()
        {
            var userClaims = HttpContext.User.Claims;
            var sidClaim = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Sid);
            if (sidClaim == null)
            {
                return 0;
            }
            return Convert.ToInt32(sidClaim.Value);
        }
        public OrderController()
        {
            _context = new AppDbContext();
        }
        public IActionResult Index()
        {
            int n = GetId();
            Account account = _context.Accounts.Include(x => x.Role).FirstOrDefault(u => u.Id == n);
            if (account == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<Order> orders = new List<Order>();
            orders = _context.Orders.Where(x => x.AccountId == account.Id).Include(x => x.Account).ToList();
            ViewBag.Orders = orders;
            return View("Index");
        }

        [HttpGet]
        public IActionResult Search(DateTime fromDate, DateTime toDate, int sort)
        {
            int n = GetId();
            Account account = _context.Accounts.Include(x => x.Role).FirstOrDefault(u => u.Id == n);
            if (account == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<Order> orders = new List<Order>();
            orders = _context.Orders.Where(x => x.AccountId == account.Id)
            .Include(x => x.Account).ToList();
            if (fromDate.Year >= 2000)
            {
                orders = orders.Where(x => x.AccountId == account.Id && fromDate.CompareTo(x.CreatedDate) <= 0).ToList();
                ViewBag.from = fromDate.ToString("yyyy-MM-dd");
            }
            if (toDate.Year >= 2000)
            {
                orders = orders.Where(x => x.AccountId == account.Id && toDate.CompareTo(x.CreatedDate) >= 0).ToList();
                ViewBag.to = toDate.ToString("yyyy-MM-dd");
            }
            if (sort == 1)
            {
                orders = orders.OrderByDescending(x => x.CreatedDate).ToList();
            }
            if (sort == 2)
            {
                orders = orders.OrderBy(x => x.CreatedDate).ToList();
            }
            ViewBag.Sort = sort;
            ViewBag.Orders = orders;
            return View("Index");
        }

        public IActionResult ListOrderByAdmin()
        {
            List<Order> orders = new List<Order>();
            orders = _context.Orders.Include(x => x.Account).OrderByDescending(x => x.CreatedDate).ToList();
            ViewBag.Orders = orders;
            return View("OrderList");
        }

        [HttpPost]
        public IActionResult Filter(DateTime fromDate, DateTime toDate, int sort)
        {
            List<Order> orders = new List<Order>();
            orders = _context.Orders.Include(x => x.Account).OrderByDescending(x => x.CreatedDate).ToList();
            if (fromDate.Year >= 2000)
            {
                orders = orders.Where(x => fromDate.CompareTo(x.CreatedDate) <= 0).ToList();
                ViewBag.from = fromDate.ToString("yyyy-MM-dd");
            }
            if (toDate.Year >= 2000)
            {
                orders = orders.Where(x => toDate.CompareTo(x.CreatedDate) >= 0).ToList();
                ViewBag.to = toDate.ToString("yyyy-MM-dd");
            }
            if (sort == 1)
            {
                orders = orders.OrderByDescending(x => x.CreatedDate).ToList();
            }
            if (sort == 2)
            {
                orders = orders.OrderBy(x => x.CreatedDate).ToList();
            }
            ViewBag.Sort = sort;
            ViewBag.Orders = orders;
            return View("OrderList");
        }
    }
}
