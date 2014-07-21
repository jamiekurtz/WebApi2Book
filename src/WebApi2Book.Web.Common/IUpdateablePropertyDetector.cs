using System.Collections.Generic;

namespace WebApi2Book.Web.Common
{
    public interface IUpdateablePropertyDetector
    {
        IEnumerable<string> GetNamesOfPropertiesToUpdate<TTargetType>(
            object objectContainingUpdatedData);
    }
}