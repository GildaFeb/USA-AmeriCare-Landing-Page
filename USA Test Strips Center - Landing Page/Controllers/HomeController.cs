using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using USA_Test_Strips_Center___Landing_Page.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Text;


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

        [HttpPost]
        public IActionResult SendEmail(IFormCollection form)
        {
            try { 
            var firstName = form["firstName"];
            var lastName = form["lastName"];
            var phoneNumber = form["phoneNumber"];
            var email = form["email"];
            var streetAddress = form["streetAddress"];
            var city = form["city"];
            var state = form["state"];
            var zipCode = form["zipCode"];
            var itemType = form["itemType"];
            var condition = form["condition"];
            var itemCount = form["itemCount"];
            var photoUpload = form.Files["photoUpload"];
            var expirationMonth = form["expirationMonth"];
            var expirationYear = form["expirationYear"];
            var paymentMethod = form["paymentMethod"];
            var accountName = form["accountName"];
            var note = form["itemCount"];

            // Prepare email content
            var sb = new StringBuilder();
            sb.AppendLine($"First Name: {firstName}");
            sb.AppendLine($"Last Name: {lastName}");
            sb.AppendLine($"Phone Number: {phoneNumber}");
            sb.AppendLine($"Email Address: {email}");
            sb.AppendLine($"Street Address: {streetAddress}");
            sb.AppendLine($"City: {city}");
            sb.AppendLine($"State: {state}");
            sb.AppendLine($"Zip Code: {zipCode}");
            sb.AppendLine($"Item Type: {itemType}");
            sb.AppendLine($"Condition: {condition}");
            sb.AppendLine($"Item Count: {itemCount}");
            sb.AppendLine($"Expiration Month: {expirationMonth}");
            sb.AppendLine($"Expiration Year: {expirationYear}");
            sb.AppendLine($"Payment Method: {paymentMethod}");
            sb.AppendLine($"Account Name: {accountName}");
            sb.AppendLine($"Note: {note}");

            // Create email message
            var message = new MailMessage();
            message.From = new MailAddress("gildafebl@gmail.com"); 
            message.To.Add("gildafebl@gmail.com");
            message.Subject = "New Quote Form Submission";
            message.Body = sb.ToString();

            // Send the email
            using (var smtpClient = new SmtpClient("smtp.gmail.com")) 
            {
                smtpClient.Port = 587;  
                smtpClient.Credentials = new NetworkCredential("gildafebl@gmail.com", "ydqq tlqr sevw miun"); 
                smtpClient.EnableSsl = true;
                smtpClient.Send(message);
            }

            TempData["SuccessMessage"] = "Your message has been sent successfully!";
            return RedirectToAction("SellNow", "Home");
                    }
                        catch (Exception ex)
                        {
                            TempData["ErrorMessage"] = "Failed to send your message. Error: " + ex.ToString();
                            return RedirectToAction("SellNow", "Home");
                }
            }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}

