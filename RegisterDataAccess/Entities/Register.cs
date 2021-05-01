using System.ComponentModel.DataAnnotations;

namespace RegisterDataAccess.Entities
{
    public class Register : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
