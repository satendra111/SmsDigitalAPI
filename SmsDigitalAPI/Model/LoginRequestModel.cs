using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class LoginRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
