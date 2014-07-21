namespace WebApi2Book.Common.TypeMapping
{
    public interface IAutoMapper
    {
        T Map<T>(object objectToMap);
    }
}