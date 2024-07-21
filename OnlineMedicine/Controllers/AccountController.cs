using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineMedicine.Models;
using OnlineMedicine.ViewModels;

namespace OnlineMedicine.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Account
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Accounts.Include(a => a.Role);
            return View(await appDbContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = "")
        {
            LoginModel model = new LoginModel();
            model.ReturnUrl = ReturnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var acc = _context.Accounts
                    .Where(a => a.Username ==  model.Username
                                && a.Password == model.Password)
                    .Include(a => a.Role)
                    .FirstOrDefault();
                string rolename = "";
                if(acc != null)
                {
                    rolename = acc.Role.Name.Trim();
                    //A claim is a statement about a subject by an issuer and    
                    //represent attributes of the subject that are useful in the context of authentication and authorization operations.
                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.Sid, acc.Id.ToString()),
                        new Claim(ClaimTypes.Name, acc.Username),
                        new Claim(ClaimTypes.Role, rolename),
                    };
                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = model.RememberLogin
                    });
                    //Redirect to last url before login
                    if(model.ReturnUrl != null && model.ReturnUrl.Length > 0)
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Accounts or passwords incorrectly";
                    return View(acc);
                }
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page   
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            Account? acc = _context.Accounts
                .Where(a => a.Username == model.Username)
                .FirstOrDefault();
            if(acc != null)
            {
                ViewData["Message"] = "Username existed!";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                Account account = new Account()
                {
                    Username = model.Username,
                    Password = model.Password,
                    Wallet = 0,
                    RoleId = 2,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                };
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            else
            {
                ViewData["Message"] = "Something is wrong?";
            }
            return View(model);
        }

        // GET: Account/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,RoleId,Address,PhoneNumber")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Name", account.RoleId);
            return View(account);
        }

        // GET: Account/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            int id = GetAccountId();
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", account.RoleId);
            return View(account);
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Username,Password,Wallet,RoleId,Address,PhoneNumber")] Account account)
        {
            int id = GetAccountId();

            if (id != account.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(account);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(account.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Medicine");
        }

        // GET: Account/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'AppDbContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private int GetAccountId()
        {
            var userClaims = HttpContext.User.Claims;
            var sidClaim = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Sid);
            return Convert.ToInt32(sidClaim.Value);
        }

        private bool AccountExists(int id)
        {
          return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
