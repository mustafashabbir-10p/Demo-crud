using Demo.Data;
using Demo.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonDbContext dbContext;
        public PersonController(PersonDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> getPersons()
        {
            return Ok(await dbContext.Persons.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> addPerson(AddPerson add)
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

            return Ok(person);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> updateperson([FromRoute] Guid id, AddPerson upd)
        {
            var person = await dbContext.Persons.FindAsync(id);
            if (person != null)
            {
                person.name = upd.name;
                person.age = upd.age;
                person.job = upd.job;

                await dbContext.SaveChangesAsync();
                return Ok(person);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> getPerson([FromRoute] Guid id)
        {
            var person = await dbContext.Persons.FindAsync(id);
            if (person != null)
            {
                return Ok(person);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> deletePerson([FromRoute] Guid id)
        {
            var person = await dbContext.Persons.FindAsync(id);
            if (person != null)
            {
                dbContext.Remove(person);
                await dbContext.SaveChangesAsync();
                return Ok(person);
            }

            return NotFound();
        }
    }
}
*/