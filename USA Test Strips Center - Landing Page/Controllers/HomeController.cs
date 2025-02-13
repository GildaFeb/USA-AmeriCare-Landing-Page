using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using USA_Test_Strips_Center___Landing_Page.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace USA_Test_Strips_Center___Landing_Page.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Instructions()
        {
            return View();
        }
        public IActionResult Faqs()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Contact(string name, string email, string phone, string message)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,  
                    Credentials = new NetworkCredential("gildafebl@gmail.com", "ydqq tlqr sevw miun"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(email),
                    Subject = "New Contact Form Submission",
                    Body = $"<p><strong>Name:</strong> {name}</p>" +
                           $"<p><strong>Email:</strong> {email}</p>" +
                           $"<p><strong>Phone:</strong> {phone}</p>" +
                           $"<p><strong>Message:</strong> {message}</p>",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(new MailAddress("gildafebl@gmail.com")); 

                mailMessage.ReplyToList.Add(new MailAddress(email));

                await smtpClient.SendMailAsync(mailMessage);

                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction("Contact", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Failed to send your message. Error: " + ex.ToString();
                return RedirectToAction("Contact", "Home");
            }
        }

        public IActionResult SellNow()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
