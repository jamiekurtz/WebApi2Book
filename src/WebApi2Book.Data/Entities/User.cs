namespace WebApi2Book.Data.Entities
{
    public class User
    {
        public virtual long UserId { get; set; }
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Username { get; set; }
        public virtual byte[] Version { get; set; }
    }
}