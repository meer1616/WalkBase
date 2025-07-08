namespace Authentication.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set;}
        public string Description { get; set; }
        
        public double LengthInKm { get; set; }
        
        public string WalkImgUrl { get; set; }

        // one to one relationship with Difficuly
        public Guid DifficultyId { get; set; }

        public Difficulty Difficulty { get; set; }
        
        // one to one relationship with Region

        public Guid RegionId { get; set; }
        public Region Region { get; set; }

    }
}
