// LoginController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApi2BookSPA.Models;

namespace WebApi2BookSPA.Controllers
{
    public class LoginController : Controller
    {
        public const string SymmetricKey = "cXdlcnR5dWlvcGFzZGZnaGprbHp4Y3Zibm0xMjM0NTY=";
        public const string Issuer = "corp";
        public const string Audience = "http://www.example.com";

        public ActionResult Index()
        {
            return View("LoginView");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            SetAuthCookie(model.UserId, model.Role);
            return RedirectToAction("Index", "Tasks");
        }

        private void SetAuthCookie(string userId, string role)
        {
            var token = CreateJwt(userId, role);
            var cookie = new HttpCookie("UserToken", token) {HttpOnly = false};
            Response.SetCookie(cookie);
        }

        private string CreateJwt(string userId, string role)
        {
            var key = Convert.FromBase64String(SymmetricKey);
            var credentials = new SigningCredentials(
                new InMemorySymmetricSecurityKey(key),
                "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                "http://www.w3.org/2001/04/xmlenc#sha256");

            var expiration = DateTime.UtcNow.AddMinutes(20).ToLongTimeString();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("exp", expiration)
                }),
                TokenIssuerName = Issuer,
                AppliesToAddress = Audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}