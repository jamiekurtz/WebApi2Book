namespace WebApi2Book.Data.Entities
{
    public interface IVersionedEntity
    {
        byte[] Version { get; set; }
    }
}