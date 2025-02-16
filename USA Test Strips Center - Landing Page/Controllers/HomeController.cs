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
                var smtpClient = new SmtpClient("mail.smarterasp.net")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("info@usamericareteststrips.com", "Welcome@1"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("info@usamericareteststrips.com"),
                    Subject = "New Contact Form Submission",
                    Body = $"<p><strong>Name:</strong> {name}</p>" +
                           $"<p><strong>Email:</strong> {email}</p>" +
                           $"<p><strong>Phone:</strong> {phone}</p>" +
                           $"<p><strong>Message:</strong> {message}</p>",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(new MailAddress("info@usamericareteststrips.com"));
                mailMessage.ReplyToList.Add(new MailAddress(email));

                await smtpClient.SendMailAsync(mailMessage);

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(message))
                {
                    TempData["ErrorMessage"] = "All fields are required.";
                    return RedirectToAction("Contact", "Home");
                }
                else
                {
                    TempData["SuccessMessage"] = "Your message has been sent successfully!";
                    return RedirectToAction("Contact", "Home");
                }
               
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Failed to send your message. Please try again later.";
                return RedirectToAction("Contact", "Home");
            }
        }


        public IActionResult SellNow()
        {
            return View();
        }

        public IActionResult SendEmail(IFormCollection form)
        {
            try
            {
                // Retrieve form data
                var firstName = form["firstName"];
                var lastName = form["lastName"];
                var phoneNumber = form["phoneNumber"];
                var email = form["email"];
                var streetAddress = form["streetAddress"];
                var city = form["city"];
                var state = form["state"];
                var suite = form["aptNumber"];
                var zipCode = form["zipCode"];
                

                var itemType = form["itemType"];
                var condition = form["condition"];
                var itemTotal = form["itemTotal"];
                var expirationMonth = form["expirationMonth"];
                var expirationYear = form["expirationYear"];
                var itemCount = form["itemCount"];
                var expirationMonthSecond = form["expirationMonth_second"];
                var expirationYearSecond = form["expirationYear_second"];
                var itemCountSecond = form["itemCount_second"];
                var photoUpload = form.Files["photoUpload"];

                var itemType1 = form["itemType1"];
                var condition1 = form["condition1"];
                var itemTotal1 = form["itemTotal1"];
                var expirationMonth1 = form["expirationMonth1"];
                var expirationYear1 = form["expirationYear1"];
                var itemCount1 = form["itemCount1"];
                var expirationMonthSecond1 = form["expirationMonth1_second"];
                var expirationYearSecond1 = form["expirationYear1_second"];
                var itemCountSecond1 = form["itemCount1_second"];
                var photoUpload1 = form.Files["photoUpload1"];


                var paymentMethod = form["paymentMethod"];
                var accountName = form["accountName"];
                var note = form["note"];


                // Construct email body using string interpolation
                var emailBody = $@"
                <html><body>
                <h1><b>New Quote Form Submission</b></h1>
                    
                <h3><b>GENERAL INFORMATION</b></h3>
                <p><b>First Name:</b> {firstName}</p>
                <p><b>Last Name:</b> {lastName}</p>
                <p><b>Phone Number:</b> {phoneNumber}</p>
                <p><b>Email Address:</b> {email}</p>
                <p><b>Street Address:</b> {streetAddress}</p>
                <p><b>City:</b> {city}</p>
                <p><b>State:</b> {state}</p>
                <p><b>Suite Number:</b> {suite}</p>
                <p><b>Zip Code:</b> {zipCode}</p>
                <br />
                <h3><b>ITEM DETAILS</b></h3>
                <p><b>Item Type:</b> {itemType}</p>
                <p><b>Condition:</b> {condition}</p>
                <p><b>Total Item:</b> {itemTotal}</p>
                <p>- Expiration Date - </p>
                <p><b>Expiration Month:</b> {expirationMonth}</p>
                <p><b>Expiration Year:</b> {expirationYear}</p>
                <p><b>Item Count:</b> {itemCount}</p>
                <p>- Other Expiration Date - </p>
                <p><b>Expiration Month:</b> {expirationMonthSecond}</p>
                <p><b>Expiration Year:</b> {expirationYearSecond}</p>
                <p><b>Item Count:</b> {itemCountSecond}</p>
                <br />

                <h3><b>ADDITIONAL ITEMS</b></h3>
                <p><b>Item Type:</b> {itemType1}</p>
                <p><b>Condition:</b> {condition1}</p>
                <p><b>Total Item:</b> {itemTotal1}</p>
                <p>- Expiration Date - </p>
                <p><b>Expiration Month:</b> {expirationMonth1}</p>
                <p><b>Expiration Year:</b> {expirationYear1}</p>
                <p><b>Item Count:</b> {itemCount1}</p>
                <p>- Other Expiration Date - </p>
                <p><b>Expiration Month:</b> {expirationMonthSecond1}</p>
                <p><b>Expiration Year:</b> {expirationYearSecond1}</p>
                <p><b>Item Count:</b> {itemCountSecond1}</p>
                <br />
                <h3><b>PAYMENT DETAILS</b></h3>
                <p><b>Payment Method:</b> {paymentMethod}</p>
                <p><b>Account Name:</b> {accountName}</p>
                <p><b>Note:</b> {note}</p>
                </body></html>";

                // Create email message
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("info@usamericareteststrips.com"),
                    Subject = "New Quote Form Submission",
                    Body = emailBody,
                    IsBodyHtml = true
                };
                mailMessage.To.Add("info@usamericareteststrips.com");

                // Attach file if present
                if (photoUpload != null && photoUpload.Length > 0)
                {
                    var stream = photoUpload.OpenReadStream();
                    var attachment = new Attachment(stream, photoUpload.FileName);
                    mailMessage.Attachments.Add(attachment);
                }

                if (photoUpload1 != null && photoUpload1.Length > 0)
                {
                    var stream = photoUpload1.OpenReadStream();
                    var attachment = new Attachment(stream, photoUpload1.FileName);
                    mailMessage.Attachments.Add(attachment);
                }

                // Configure SMTP Client
                using (var smtpClient = new SmtpClient("mail.smarterasp.net", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("info@usamericareteststrips.com", "ydqq tlqr sevw miun");
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                }

                TempData["SuccessMessage"] = "Your form has been sent successfully!";
                return RedirectToAction("SellNow", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Failed to send your form. Error: " + ex.Message;
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

