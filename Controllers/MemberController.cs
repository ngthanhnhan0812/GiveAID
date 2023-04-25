using GiveAID.DAO;
using GiveAID.Models;
using GiveAID.Models.RazorPay;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Razorpay.Api;
using Payment = Razorpay.Api.Payment;

namespace GiveAID.Controllers
{
    public class MemberController : Controller
    {
        private readonly AIDContext ct = new();

        public IActionResult Login()
        {
            if (Request.Cookies["member"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CheckLogin(Member mem)
        {
            Member member = Dao_Member.Instance().Check_Uname_Pass(mem.mem_username??"", mem.mem_password ?? "");
            if (member != null)
            {
                string m = JsonConvert.SerializeObject(member);
                Response.Cookies.Append("member", m, new CookieOptions{Expires = DateTimeOffset.MaxValue});
                return RedirectToAction("Index");
            }
            else
			{
                TempData["Message"] = "Username Or Password Is Incorrect";
				return RedirectToAction("Login");
			}
        }


        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Donate()
        {
            var a = Request.Cookies["member"];
            if (Request.Cookies["member"] == null)
            {
                return RedirectToAction("Login");
            }
            Member? member = JsonConvert.DeserializeObject<Member>(Request.Cookies["member"]);
            ViewBag.abc = member;
            TempData["Member"] = member;
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(PaymentRequest _requestData)
        {
            // Generate random receipt number for order
            Random randomObj = new();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();
            RazorpayClient client = new("rzp_test_unAtgRERC0DP5v", "KGPEzBN6fuQfVvaMkh8oJVwh");
            Dictionary<string, object> options = new()
            {
                { "amount", _requestData.Amount * 100 },  // Amount will in paise
                { "receipt", transactionId },
                { "currency", "INR" },
                { "payment_capture", "0" } // 1 - automatic  , 0 - manual
            };
            //options.Add("notes", "-- You can put any notes here --");
            Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();
            // Create order model for return on view
            RazorPayOrder orderModel = new()
            {
                OrderId = orderResponse.Attributes["id"],
                RazorPayAPIKey = "rzp_test_unAtgRERC0DP5v",
                Amount = _requestData.Amount * 100,
                Currency = "INR",
                Name = _requestData.Name,
                Email = _requestData.Email,
            };
            // Return on PaymentPage with Order data
            return View("CreateOrder", orderModel);
        }

        [HttpPost]
        public ActionResult Complete()
        {
            // Payment data comes in url so we have to get it from url
            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            string paymentId = HttpContext.Request.Form["rzp_paymentid"].ToString();
            // This is orderId
            string orderId = HttpContext.Request.Form["rzp_orderid"].ToString();
            RazorpayClient client = new RazorpayClient("rzp_test_unAtgRERC0DP5v", "KGPEzBN6fuQfVvaMkh8oJVwh");
            Payment payment = client.Payment.Fetch(paymentId);
            // This code is for capture the payment 
            Dictionary<string, object> options = new();
            options.Add("amount", payment.Attributes["amount"]);
            Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];
            //// Check payment made successfully
            if (paymentCaptured.Attributes["status"] == "captured")
            {
                // Create these action method
                ViewBag.Message = "Paid successfully";
                ViewBag.Order = paymentCaptured.Attributes;
                return RedirectToAction("Donate");
            }
            else
            {
                ViewBag.Message = "Payment failed, something went wrong";
                return RedirectToAction("Donate");
            }
        }
    }
}
