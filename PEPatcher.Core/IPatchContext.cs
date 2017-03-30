using System;
using System.Collections.Generic;
using System.Reflection;
using Mono.Cecil;

namespace PEPatcher.Core
{
    public interface IPatchContext
    {
        AssemblyDefinition AssemblyDefinition { get; }
        void LoadInjectors(IEnumerable<IInjector> injectors);
        void RunInjectors();
        void Run(Func<Assembly, MethodInfo> getEntryPoint, object[] arguments);
    }
}
