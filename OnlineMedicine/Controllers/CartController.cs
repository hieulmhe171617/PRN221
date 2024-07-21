using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMedicine.Models;
using PayPal;
using PayPal.Api;
using System.Collections.Generic;
using System.Security.Claims;

namespace OnlineMedicine.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        public IActionResult AddToCart(int medicineId, int quantity, decimal unitPrice)
        {
            int accId = GetId();
            Cart c;

            if (_context.Carts.FirstOrDefault(x => x.AccountId == accId && x.MedicineId == medicineId) == null)
            {
                c = new Cart
                {
                    AccountId = accId,
                    Quantity = quantity,
                    MedicineId = medicineId,
                    Price = quantity * unitPrice
                };
                //trong cart chưa có thì add
                _context.Carts.Add(c);
                _context.SaveChanges();
            }
            else
            {
                //có rồi thì update lại thông tin
                c = _context.Carts.FirstOrDefault(x => x.AccountId == accId && x.MedicineId == medicineId);
                c.AccountId = accId;
                c.Quantity = quantity;
                c.MedicineId = medicineId;
                c.Price = quantity * unitPrice;
                _context.Carts.Update(c);
                _context.SaveChanges();
            }

            return Redirect("/Medicine/Details/" + medicineId);

        }

        public IActionResult Details(int? id)
        {
            int accId = GetId();
            List<Cart> list = _context.Carts.Include(x => x.Account).
                Include(x => x.Medicine).Include(x => x.Medicine.Type).Include(x => x.Medicine.Category).
                Include(x => x.Medicine.Country).
                Where(x => x.AccountId == accId).ToList();
            ViewBag.list = list;
            return View();
        }

        public IActionResult UpdateQuantity(int quantity, int mediId, decimal unitPrice)
        {
            int accId = GetId();

            Cart c = _context.Carts.FirstOrDefault(x => x.AccountId == accId && x.MedicineId == mediId);
            c.Quantity = quantity;
            c.Price = unitPrice * quantity;
            _context.Carts.Update(c);
            _context.SaveChanges();


            return Redirect("/Cart/Details/" + accId);
        }

        public IActionResult DeleteOneItemInCart(int id)
        {
            int accId = GetId();
            Cart c = _context.Carts.FirstOrDefault(x => x.AccountId == accId && x.MedicineId == id);
            _context.Carts.Remove(c);
            _context.SaveChanges();

            return Redirect("/Cart/Details/" + accId);

        }

        public int GetId()
        {
            var userClaims = HttpContext.User.Claims;
            var sidClaim = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Sid);
            return Convert.ToInt32(sidClaim.Value);
        }
        public IActionResult CheckOut()
        {
            
            int accountId = GetId();
            if (HttpContext.Session.GetString("listId") == null)
            {
                return Redirect("/Cart/Details/" + accountId);
            }
            var listId = HttpContext.Session.GetString("listId");
            string[] s = listId.Split('-');
            
            List<Cart> carts = _context.Carts.Where(x => x.AccountId == accountId && s.Contains(x.MedicineId.ToString())).ToList();
            Models.Order order = new Models.Order();


            decimal totalMoney = 0;
            foreach (Cart c in carts)
            {
                Medicine product = _context.Medicines.FirstOrDefault(x => x.Id == c.MedicineId);
                if (product.Quantity < c.Quantity)
                {
                    ViewBag.Error = "Some medicines are out stock.";
                    return View("List");
                }
                totalMoney += c.Price;
            }
            order.TotalMoney = totalMoney;
            order.CreatedDate = DateTime.Now;
            order.AccountId = accountId;
            order.CustomerName = HttpContext.Session.GetString("customerName");
            order.Address = HttpContext.Session.GetString("customerAddress");
            order.PhoneNumber = HttpContext.Session.GetString("customerPhoneNumber");
            _context.Orders.Add(order);
            if (_context.SaveChanges() > 0)
            {
                List<OrderDetail> listDetail = new List<OrderDetail>();
                List<Medicine> listProduct = new List<Medicine>();
                int orderId = _context.Orders.OrderBy(order => order.Id).Last().Id;
                foreach (Cart c in carts)
                {
                    OrderDetail detail = new OrderDetail();
                    detail.OrderId = orderId;
                    detail.MedicineId = c.MedicineId;
                    detail.Quantity = c.Quantity;
                    detail.Price = c.Price;
                    listDetail.Add(detail);
                    Medicine product = _context.Medicines.FirstOrDefault(x => x.Id == detail.MedicineId);
                    product.Quantity = product.Quantity - c.Quantity;
                    listProduct.Add(product);
                }
                _context.OrderDetails.AddRange(listDetail);
                _context.SaveChanges();
                _context.Medicines.UpdateRange(listProduct);
                _context.SaveChanges();

                _context.Carts.RemoveRange(carts);
                _context.SaveChanges();


            }
            ViewBag.Order = _context.Orders.Where(x => x.AccountId == order.AccountId).Include(x => x.Account).OrderBy(order => order.Id).Last();
            ViewBag.OrderDetails = _context.OrderDetails.Where(x => x.OrderId == order.Id)
                .Include(x => x.Medicine).ToList();
            HttpContext.Session.Remove("listId");
            HttpContext.Session.Remove("customerName");
            HttpContext.Session.Remove("customerAddress");
            HttpContext.Session.Remove("customerPhoneNumber");
            return View();
        }
        public IActionResult FailureView()
        {
            return View();
        }
        public IActionResult SuccessView()
        {
            return View();
        }
        public IActionResult PaypalPayment(string listId = null) {
            int accountId = GetId();
            if (string.IsNullOrEmpty(listId))
            {
                return Redirect("/Cart/Details/" + accountId);
            }

            HttpContext.Session.SetString("listId", listId);
            return RedirectToAction("CheckOutInfo");
        }
        public IActionResult CheckOutInfo(string name = null, string address = null, string phoneNumber = null)
        {
            if(name == null || address == null || phoneNumber == null)
            {
                int accountId = GetId();
                Models.Account acc = _context.Accounts.FirstOrDefault(x => x.Id == accountId);
                ViewBag.Account = acc;

                if (HttpContext.Session.GetString("listId") == null)
                {
                    return Redirect("/Cart/Details/" + accountId);
                }
                var listId = HttpContext.Session.GetString("listId");
                string[] s = listId.Split('-');

                List<Cart> carts = _context.Carts.Where(x => x.AccountId == accountId && s.Contains(x.MedicineId.ToString()))
                    .Include(x => x.Medicine)
                    .Include(x => x.Medicine.Type)
                    .Include(x => x.Medicine.Category)
                    .Include(x => x.Medicine.Country).ToList();
                ViewBag.Cart = carts;

                return View();
            }
            else
            {
                HttpContext.Session.SetString("customerName", name);
                HttpContext.Session.SetString("customerAddress", address);
                HttpContext.Session.SetString("customerPhoneNumber", phoneNumber);
                return RedirectToAction("PaymentWithPayPal");
            }

        }
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Query["PayerID"].ToString();
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}/Cart/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid
                    HttpContext.Session.SetString(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Query["guid"].ToString();
                    var executedPayment = ExecutePayment(apiContext, payerId, HttpContext.Session.GetString(guid) as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return RedirectToAction("CheckOut");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            List<Cart> carts = new List<Cart>();
            if (HttpContext.Session.GetString("listId") != null)
            {
                var listId = HttpContext.Session.GetString("listId");
                int accountId = GetId();
                string[] s = listId.Split('-');

                carts = _context.Carts.Where(x => x.AccountId == accountId && s.Contains(x.MedicineId.ToString()))
                    .Include(x => x.Medicine).ToList();
            }
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };

            //Adding Item Details like name, currency, price etc
            decimal totalMoney = 0;
            foreach (Cart c in carts)
            {
                totalMoney += c.Price;
                itemList.items.Add(new Item()
                {
                    name = c.Medicine.Name,
                    currency = "USD",
                    price = decimal.Round(c.Medicine.Price, 0).ToString(),
                    quantity = c.Quantity.ToString(),
                    sku = "sku"
                });
            }
            totalMoney = decimal.Round(totalMoney, 0);
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = totalMoney.ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = totalMoney.ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(), //Generate an Invoice No    
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }

    
}
}
