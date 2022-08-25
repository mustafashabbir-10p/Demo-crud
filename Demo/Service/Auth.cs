using Demo.Data;
using Demo.models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Demo.Service
{
    public class Auth : IAuth
    {
        private readonly PersonDbContext dbContext;
        private readonly IConfiguration _configuration;
        public Auth(PersonDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<string> login(Login req)
        {
            var person = dbContext.Persons.Where(a => a.username == req.username).FirstOrDefault();
            
            if (person == null)
            {
                return ("wrong username");
            }
            if (!verifyPassword(req.password, person.passwordHash, person.passwordSalt))
            {
                return ("wrong password");
            }
            string token = createToken(person);
            return (token);
        }

        public async Task<Person> register(Register req)
        {
            var person = new Person();
            
            createPasswordHash(req.password, out byte[] passwordHash, out byte[] passwordSalt);

            person.username = req.username;
            person.passwordSalt = passwordSalt;
            person.passwordHash = passwordHash;
            person.id = Guid.NewGuid();
            person.name = req.name;
            person.job = req.job;
            person.age = req.age;

            await dbContext.Persons.AddAsync(person);
            await dbContext.SaveChangesAsync();

            return person;
        }

        private string createToken(Person person)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, person.username),
                new Claim(ClaimTypes.NameIdentifier, person.id.ToString()),
                new Claim(ClaimTypes.Role, "Admin")

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
