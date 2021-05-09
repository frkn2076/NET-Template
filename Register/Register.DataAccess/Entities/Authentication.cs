using System.ComponentModel.DataAnnotations;

namespace Register.DataAccess.Entities
{
    public class Authentication : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
