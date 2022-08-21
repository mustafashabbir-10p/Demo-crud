using Demo.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Person person = new Person();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration config)
        {
            _configuration = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> register(AddPerson req)
        {
            createPasswordHash(req.password, out byte[] passwordHash, out byte[] passwordSalt);

            person.username = req.username;
            person.passwordSalt = passwordSalt;
            person.passwordHash = passwordHash;
            person.id = Guid.NewGuid();
            person.name = req.name;
            person.job = req.job;
            person.age = req.age;

            return Ok(person);
        }
        [HttpPost("login")]
        public async Task<IActionResult> login(Login req)
        {
            if(person.username != req.username)
            {
                return BadRequest("wrong username");
            }
            if (!verifyPassword(req.password, person.passwordHash, person.passwordSalt))
            {
                return BadRequest("wrong password");
            }
            string token = createToken(person);
            return Ok(token);

        }

        private string createToken(Person person)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, person.name),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        private bool verifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
               var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
