using System.ComponentModel.DataAnnotations;

namespace Orso.Arpa.Application.AuthApplication.Model
{
    public class AdminResetPasswordDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
