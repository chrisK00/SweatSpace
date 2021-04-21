namespace SweatSpace.Api.Persistence.Entities
{
    public abstract class BaseOwnedEntity
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }        
    }
}