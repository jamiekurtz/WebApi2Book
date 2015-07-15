namespace EFCommonContext
{
    public interface IWebContextFactory
    {
        bool ContextExists { get; }
        IDbContext GetCurrentContext();
        IDbContext GetNewOrCurrentContext();
        void Reset();
    }
}