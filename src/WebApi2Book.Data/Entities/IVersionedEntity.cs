// IVersionedEntity.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Data.Entities
{
    public interface IVersionedEntity
    {
        byte[] Version { get; set; }
    }
}