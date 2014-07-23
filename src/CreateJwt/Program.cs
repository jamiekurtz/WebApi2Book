// Program.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Diagnostics;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace CreateJwt
{
    internal class Program
    {
        private const string SymmetricKey = "cXdlcnR5dWlvcGFzZGZnaGprbHp4Y3Zibm0xMjM0NTY=";

        private static void Main(string[] args)
        {
            var key = Convert.FromBase64String(SymmetricKey);
            var credentials = new SigningCredentials(
                new InMemorySymmetricSecurityKey(key),
                "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                "http://www.w3.org/2001/04/xmlenc#sha256");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "bhogg"),
                    new Claim(ClaimTypes.GivenName, "Boss"),
                    new Claim(ClaimTypes.Surname, "Hogg"),
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim(ClaimTypes.Role, "SeniorWorker"),
                    new Claim(ClaimTypes.Role, "JuniorWorker")
                }),
                TokenIssuerName = "corp",
                AppliesToAddress = "http://www.example.com",
                SigningCredentials = credentials,
                Lifetime = new Lifetime(DateTime.UtcNow, DateTime.UtcNow.AddYears(10))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            Console.WriteLine(tokenString);
            Debug.WriteLine(tokenString);

            Console.ReadLine();
        }
    }
}