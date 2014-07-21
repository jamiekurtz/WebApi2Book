namespace WebApi2Book.Common.Security
{
    public interface IUserSession
    {
        string Firstname { get; }
        string Lastname { get; }
        string Username { get; }
        bool IsInRole(string roleName);
    }
}