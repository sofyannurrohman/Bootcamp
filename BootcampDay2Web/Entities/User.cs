namespace Entities.User;
using System.ComponentModel.DataAnnotations;

    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; } = "";
       
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Password { get; set; } = ""; 
    }
