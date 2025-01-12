using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace lab1.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _sendGridApiKey;

        public HomeController()
        {
            _sendGridApiKey = "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";
        }

        [HttpGet]
        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string email, string subject, string message)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
            {
                ViewBag.Error = "Все поля обязательны для заполнения.";
                return View();
            }

            var result = await SendEmailAsync(email, subject, message);
            if (result)
            {
                ViewBag.Success = "Email успішно відправлено.";
            }
            else
            {
                ViewBag.Error = "Помилка при відправці Email.";
            }

            return View();
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) }
            );

            return LocalRedirect(returnUrl ?? "/");
        }
        private async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var client = new SendGridClient(_sendGridApiKey);
                var from = new EmailAddress("1tzcrashe1@gmail.com", "taintedgqd");
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
                var response = await client.SendEmailAsync(msg);

                return response.StatusCode == System.Net.HttpStatusCode.Accepted;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при відправці Email: {ex.Message}");
                return false;
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
