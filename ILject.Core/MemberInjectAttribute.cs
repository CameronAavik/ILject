using System;
using Mono.Cecil;

namespace ILject.Core
{
    public enum MemberType
    {
        Type,
        Field,
        Event,
        Property,
        Method
    }

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
}
