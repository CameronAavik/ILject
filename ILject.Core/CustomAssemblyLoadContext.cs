using System.Reflection;
using System.Runtime.Loader;

namespace ILject.Core
{
    internal class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        protected override Assembly Load(AssemblyName assemblyName) => Assembly.Load(assemblyName);
    }
}
