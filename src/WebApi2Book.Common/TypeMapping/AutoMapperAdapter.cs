// AutoMapperAdapter.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using AutoMapper;

namespace WebApi2Book.Common.TypeMapping
{
    public class AutoMapperAdapter : IAutoMapper
    {
        public T Map<T>(object objectToMap)
        {
            return Mapper.Map<T>(objectToMap);
        }
    }
}