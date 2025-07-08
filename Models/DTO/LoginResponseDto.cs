using Microsoft.AspNetCore.Identity;

namespace Authentication.Models.DTO
{
    public class LoginResponseDto
    {
        public string jwtToken { get; set; }

        public IdentityUser user { get; set; }
    }
}
