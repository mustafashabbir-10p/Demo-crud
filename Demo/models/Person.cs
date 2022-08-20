using System.ComponentModel.DataAnnotations;

namespace Demo.models
{
    public class Person
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; } 
        public int age { get; set; }
        public string job { get; set; } 
    }
}
