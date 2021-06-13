namespace SweatSpace.Api.Persistence.Entities
{
    public abstract class BaseOwnedEntity
    {
        public int Id { get; init; }
        public int AppUserId { get; set; }
    }
}