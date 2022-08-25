using Demo.Data;
using Demo.models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Web.Providers.Entities;

namespace Demo.Service
{
    public class PersonData : IPersonData
    {
        private readonly PersonDbContext dbContext;
        
        public PersonData(PersonDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Person> addPerson(Person add)
        {
            var person = new Person()
            {
                id = Guid.NewGuid(),
                name = add.name,
                age = add.age,
                job = add.job,
                passwordHash = add.passwordHash,
                passwordSalt = add.passwordSalt,
                username = add.username,
            };

            await dbContext.Persons.AddAsync(person);
            await dbContext.SaveChangesAsync();

            return person;
        }

        public async Task<Person> deletePerson(Guid id)
        {
            var person = await dbContext.Persons.FindAsync(id);
            if (person != null)
            {
                dbContext.Remove(person);
                await dbContext.SaveChangesAsync();
                return person;
            }
            return null;
        }

        public async Task<Person> getPerson(Guid id)
        {
            var person = await dbContext.Persons.FindAsync(id);
            if (person != null)
            {
                return person;
            }
            return null;
        }

        public async Task<List<Person>> getPersons()
        {
            return await dbContext.Persons.ToListAsync();
        }

        public async Task<Person> updatePerson(Person req, Guid id)
        {
            var person = await dbContext.Persons.FindAsync(id);
            if (person != null)
            {
                person.username = req.username;
                person.passwordSalt = req.passwordSalt;
                person.passwordHash = req.passwordHash;
                person.id = Guid.NewGuid();
                person.name = req.name;
                person.job = req.job;
                person.age = req.age;

                await dbContext.SaveChangesAsync();
                return person;
            }
            return null;
            
        }
         public bool verifyUser(string user, Guid id)
        {
            if (user.Equals(id.ToString()))
                return true;
            return false;
        }
    }
}
