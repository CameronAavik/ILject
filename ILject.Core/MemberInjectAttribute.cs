using System;
using System.Linq;
using Mono.Cecil;
using Mono.Collections.Generic;

namespace ILject.Core
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class MemberInjectAttribute : Attribute
    {
        public Func<AssemblyDefinition, IMemberDefinition> GetMember;

        public MemberInjectAttribute(string fullName, MemberType memberType)
        {
            switch (memberType)
            {
                case MemberType.Type:
                    GetMember = a => new MemberDefinitionResolver<TypeDefinition>(fullName).GetMember(a);
                    break;
                case MemberType.Field:
                    GetMember = a => new MemberDefinitionResolver<FieldDefinition>(fullName, t => t.Fields).GetMember(a);
                    break;
                case MemberType.Event:
                    GetMember = a => new MemberDefinitionResolver<EventDefinition>(fullName, t => t.Events).GetMember(a);
                    break;
                case MemberType.Property:
                    GetMember = a => new MemberDefinitionResolver<PropertyDefinition>(fullName, t => t.Properties).GetMember(a);
                    break;
                case MemberType.Method:
                    GetMember = a => new MemberDefinitionResolver<MethodDefinition>(fullName, t => t.Methods).GetMember(a);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(memberType), memberType, null);
            }
        }
    }

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

    public enum MemberType
    {
        Type,
        Field,
        Event,
        Property,
        Method
    }
}
