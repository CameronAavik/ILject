using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Mono.Cecil;

namespace PEPatcher.Core
{
    public class PatchContext : IPatchContext
    {
        #region Initialising the context

        public AssemblyDefinition AssemblyDefinition { get; }

        public PatchContext(string executableName) : this(AssemblyDefinition.ReadAssembly(executableName)) {}

        public PatchContext(AssemblyDefinition assemblyDefinition)
        {
            AssemblyDefinition = assemblyDefinition;
        }

        #endregion

        #region Loading the injectors

        private List<IInjector> Injectors { get; } = new List<IInjector>();

        public void LoadInjectors(IEnumerable<IInjector> injectors)
        {
            Injectors.AddRange(injectors);
        }

        #endregion

        #region Running the injectors

        public void RunInjectors()
        {
            Injectors.OrderByDescending(i => i.ExecutionPriority).ToList().ForEach(RunInjector);
        }

        private void RunInjector(IInjector injector)
        {
            injector.GetType()
                    .GetRuntimeMethods()
                    .Select(m => new {Method = m, Attributes = m.GetCustomAttributes().OfType<MemberInjectAttribute>()})
                    .Where(m => m.Attributes.Count() == 1)
                    .ToList()
                    .ForEach(m => RunInjectionMethod(injector, m.Method, m.Attributes.Single()));
        }

        private void RunInjectionMethod(IInjector injector, MethodBase injectionMethod, MemberInjectAttribute attribute)
        {
            injectionMethod.Invoke(injector, new object[] {attribute.GetMember(AssemblyDefinition)});
        }

        #endregion

        #region Running the assembly

        public void Run(Func<Assembly, MethodInfo> getEntryPoint = null, object[] arguments = null)
        {
            using (var assemblyStream = new MemoryStream())
            {
                AssemblyDefinition.MainModule.Write(assemblyStream);
                assemblyStream.Seek(0, SeekOrigin.Begin);
                var assembly = AssemblyLoadContext.Default.LoadFromStream(assemblyStream);
                GetEntryPoint(assembly, getEntryPoint).Invoke(null, arguments ?? new object[] {new string[0]});
            }
        }

        private static MethodInfo GetEntryPoint(Assembly assembly, Func<Assembly, MethodInfo> getEntryPoint)
        {
            if (getEntryPoint != null)
            {
                return getEntryPoint(assembly);
            }
            var entryPointField = assembly.GetType().GetRuntimeField("EntryPoint");
            if (entryPointField != null)
            {
                return (MethodInfo) entryPointField.GetValue(assembly);
            }
            var mainMethods = assembly.DefinedTypes.SelectMany(t => t.DeclaredMethods).Where(m => m.Name == "Main").ToList();
            if (mainMethods.Count != 1)
            {
                throw new Exception("Unable to find single Main method.");
            }
            return mainMethods.Single();
        }

        #endregion
    }
}
