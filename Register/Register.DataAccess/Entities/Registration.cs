using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Register.DataAccess.Entities
{
    [Table("registration")]
    public class Registration : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("name")]
        public string Password { get; set; }
    }
}
