namespace NZwalks.API.Models.Domain
{
    public class Walk
    {

        // Calss Walks 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double  Length { get; set; }

        //Call Class Region

        public Guid RegionId { get; set; }
        public Guid WalksDifficultyId { get; set; }



        //Navigation Properties

        public Region Region { get; set; }
        public WalkDifficulty walkDifficulty { get; set; }
    }
}
