using GiveAID.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;

namespace GiveAID.Controllers
{
	
    public class LoginController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			if(HttpContext.Session.GetString == null)
			{
                if (Request.Cookies["LoginCookie"] == null)
                {
                    return RedirectToAction("Login");
                }
			}
            return RedirectToAction("Index","Home",new {area = ""});
		}
        [HttpGet]
        //[Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(Admin ad)
        {
            string? name = HttpContext.Request.Cookies["Name"];
            string? username = Request.Form["username"];
            string? password = Request.Form["password"];
            AIDContext context = new AIDContext();
            if (ModelState.IsValid)
            {
                var result = context.Admins?.Where(x => x.ad_username == username && x.ad_password == password).FirstOrDefault();
                if (result != null)
                {
					try
					{
                        HttpContext.Session.SetString("LoginSession", "nyan");
                        HttpContext.Response.Cookies.Append("LoginCookie","Nyan");
						EncodePassword(password);
                    }
					catch (Exception)
					{
						throw;
					}
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Username or password is incorrect, please try again";
                    return RedirectToAction("Login");
                }
            }
            return View(ad);
        }
		public static string EncodePassword(string password)
		{
			byte[] salt;
			byte[] buffer2;
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
			{
				salt = bytes.Salt;
				buffer2 = bytes.GetBytes(0x20);
			}
			byte[] dst = new byte[0x31];
			Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
			Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
			return Convert.ToBase64String(dst);
		}
	}
}
