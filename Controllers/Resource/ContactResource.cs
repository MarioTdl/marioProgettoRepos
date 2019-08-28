using System.ComponentModel.DataAnnotations;

namespace marioProgetto.Controllers.Resource
{
    public class ContactResource
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }    
        [Required]
        [StringLength(255)]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
    }
}