using Infra.Core.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace Infra.Resource.DataAccess.Entities
{
    public class Localization : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
