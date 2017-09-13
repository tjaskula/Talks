using System.Collections.Generic;
using System.Reflection;

namespace TPLDataFlowTests
{
    public interface IMethodInfoScanner
    {
        IEnumerable<MethodInfo> GetMethodInfos(object targetToAdapt);
    }
}