using Demo.Data;
using Demo.models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Service
{
    public class PersonData : IPersonData
    {
        private readonly PersonDbContext dbContext;
        public PersonData(PersonDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Person> addPerson(AddPerson add)
        {
            var person = new Person()
            {
                id = Guid.NewGuid(),
                name = add.name,
                age = add.age,
                job = add.job,
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

        public async Task<Person> updatePerson(AddPerson upd, Guid id)
        {
            var person = await dbContext.Persons.FindAsync(id);
            if (person != null)
            {
                person.name = upd.name;
                person.age = upd.age;
                person.job = upd.job;

                await dbContext.SaveChangesAsync();
                return person;
            }
            return null;
            
        }
    }
}
