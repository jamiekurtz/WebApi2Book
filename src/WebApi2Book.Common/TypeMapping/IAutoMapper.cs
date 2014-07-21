// IAutoMapper.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Common.TypeMapping
{
    public interface IAutoMapper
    {
        T Map<T>(object objectToMap);
    }
}