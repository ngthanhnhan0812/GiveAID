using GiveAID.DAO;
using GiveAID.Models;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;

namespace GiveAID.Controllers
{
    public class ContactController : Controller
    {
        // Inject the email service
        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        // Post the contact form data and send an email reply
        [HttpPost]
        public async Task<IActionResult> Contact(Contact model, DateTime created_at, Int16 status)
        {
          

            //// Check if the model state is valid
            //if (ModelState.IsValid)
            //{
            //    // Send an email to the sender with a thank you message
            //    var subject = "Thank you for contacting us";
            //    var body = $"Hello {model.Member.mem_username},\n\nWe have received your message and we will get back to you soon.\n\nYour message:\n{model.contact_desc}\n\nBest regards,\nThe Team";
            //    try
            //    {
            //        await _emailService.SendAsync(model.Member.email, subject, body);
            //    }
            //    catch (Exception)
            //    {
            //        ModelState.AddModelError("","An error occured while sending the email. Please try again later.");
            //        return View(model);
            //    }
            //    return RedirectToAction("Confirmation");
            //}

            // Return the same view with validation errors
            return View(model);
        }
        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
