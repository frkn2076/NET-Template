using Infra.Core.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace Register.DataAccess.Entities
{
    public class Registration : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
