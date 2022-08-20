using Demo.models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
