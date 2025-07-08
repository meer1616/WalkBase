using System.ComponentModel.DataAnnotations;

namespace Authentication.Models.DTO
{
    public class AddRegionReqDto
    {
        [Required]
        [StringLength(3, ErrorMessage = "Code must be exactly 3 characters long.", MinimumLength = 3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 character long.")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
