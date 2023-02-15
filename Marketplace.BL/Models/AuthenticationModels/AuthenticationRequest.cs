using System.ComponentModel.DataAnnotations;

namespace MarketplaceBL.Models.AuthenticationModels
{
    public class AuthenticationRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
