using System;
using System.Linq;
using Mono.Cecil;
using Mono.Collections.Generic;

namespace ILject.Core
{
    internal class MemberDefinitionResolver<T> where T : IMemberDefinition
    {
        public Func<TypeDefinition, Collection<T>> GetCollectionFunc;

        public MemberDefinitionResolver(string fullName, Func<TypeDefinition, Collection<T>> getCollFunc = null)
        {
            FullName = fullName;
            GetCollectionFunc = getCollFunc;
        }

        private string FullName { get; }
        private string TypeName
        {
            get
            {
                var fullNameSplit = FullName.Split(' ');
                return fullNameSplit.Length > 1 ? fullNameSplit[1].Split(new[] {"::"}, StringSplitOptions.None)[0] : FullName;
            }
        }

        // Don't smite me for doing this in a generic class!
        public T GetMember(AssemblyDefinition assembly)
        {
            return typeof(T) == typeof(TypeDefinition) ? (T) (object) GetType(assembly) : GetMemberInternal(assembly);
        }

        private TypeDefinition GetType(AssemblyDefinition assembly)
        {
            return assembly.Modules.Select(m => m.GetType(TypeName)).Single(t => t != null);
        }

        private T GetMemberInternal(AssemblyDefinition assembly)
        {
            return GetCollectionFunc(GetType(assembly)).Single(f => f.FullName == FullName);
        }
    }
}
