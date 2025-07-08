using Authentication.Models.Domain;

namespace Authentication.Models.DTO
{
    public class UpdateWalkReqDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string WalkImgUrl { get; set; }

        // one to one relationship with Difficuly
        public Guid? DifficultyId { get; set; }

        public Guid? RegionId { get; set; }

    }
}
