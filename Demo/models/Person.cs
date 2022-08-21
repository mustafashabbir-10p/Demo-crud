using System.ComponentModel.DataAnnotations;

namespace Demo.models
{
    public class Person
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; } 
        public string username { get; set; }    
        public byte[] passwordHash { get; set; }    
        public byte[] passwordSalt { get; set; }
        public int age { get; set; }
        public string job { get; set; } 
    }
}
