using HRSDmgmt.Data;
using HRSDmgmt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;

namespace HRSDmgmt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Jobs()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Jobs(string Referencja, IFormFile userAttachment)
        {
            MailMessage message = new MailMessage(new MailAddress("humberto.sporer@ethereal.email"), new MailAddress("humberto.sporer@ethereal.email"));
            message.Subject = "Aplikacja na stanowisko, ref: " + Referencja;
            message.Body = "CV kandydata w załączeniu";
            message.IsBodyHtml = false;
            message.Attachments.Add(new Attachment(userAttachment.OpenReadStream(), userAttachment.FileName));

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.ethereal.email";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
            // https://ethereal.email/login strona logowania
            credential.UserName = "humberto.sporer@ethereal.email";
            credential.Password = "aHX79rZ6jqvrdXQMwB";
            // https://ethereal.email/messages skrzynka pocztowa

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credential;

            smtp.Send(message);

            ViewBag.info = "CV zostało wysłane";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var companies = await _context.Companies.ToListAsync();
            var offers = await _context.Offers.ToListAsync();
            var employees = await _context.Employees.ToListAsync();

            ViewBag.Companies = companies.Count();
            ViewBag.Offers = offers.Count();
            ViewBag.Employees = employees.Count();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}