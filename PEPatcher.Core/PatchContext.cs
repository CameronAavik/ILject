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
        public PatchContext(string executableName)
        {
            AssemblyDefinition = AssemblyDefinition.ReadAssembly(executableName);
        }

        public PatchContext(AssemblyDefinition assemblyDefinition)
        {
            AssemblyDefinition = assemblyDefinition;
        }

        public AssemblyDefinition AssemblyDefinition { get; set; }

        public void LoadInjectors(IEnumerable<IInjector> injectors)
        {
            throw new NotImplementedException();
        }

        public void Inject()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            using (var stream = new MemoryStream())
            {
                AssemblyDefinition.MainModule.Write(stream);
                stream.Seek(0, SeekOrigin.Begin);
                var assembly = AssemblyLoadContext.Default.LoadFromStream(stream);
                GetEntryPoint(assembly).Invoke(null, new object[] { new string[0] });
            }
        }

        private static MethodInfo GetEntryPoint(Assembly assembly)
        {
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
    }
}