using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMedicine.Models;
using System.Security.Claims;
using System.Security.Principal;

namespace OnlineShopMedicine.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly AppDbContext _context;
        public OrderDetailsController()
        {
            _context = new AppDbContext();
        }
        public IActionResult Details(int id)
        {
            if (id != 0)
            {
                ViewBag.OrderDetail = _context.OrderDetails.Where(x => x.OrderId == id).Include(x => x.Medicine)
                    .Include(x => x.Medicine.Type).Include(x => x.Medicine.Category).ToList();
                return View("Details");
            }
            return RedirectToAction("Index", "Order");
        }
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
        public IActionResult DetailByAdmin(int id)
        {
            ViewBag.ListO = _context.OrderDetails.Where(x => x.OrderId == id).Include(x => x.Medicine)
                .Include(x => x.Order).ThenInclude(x => x.Account)
                .Include(x => x.Medicine.Type).Include(x => x.Medicine.Category).ToList();
            return View("Index");
        }

    }


}
