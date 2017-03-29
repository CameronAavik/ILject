using System.Collections.Generic;
using Mono.Cecil;

namespace PEPatcher.Core
{
    public interface IPatchContext
    {
        AssemblyDefinition AssemblyDefinition { get; set; }
        void LoadInjectors(IEnumerable<IInjector> injectors);
        void Inject();
        void Run();
    }
}