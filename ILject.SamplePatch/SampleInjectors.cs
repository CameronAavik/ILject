using System.Linq;
using ILject.Core;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ILject.SamplePatch
{
    public class SampleCoreInjector : IInjector
    {
        public int ExecutionPriority { get; set; } = 0;

        [MemberInject("System.Int32 ILject.SamplePatchTarget.Program::Add(System.Int32,System.Int32)", MemberType.Method)]
        public void InjectProgramAdd(MethodDefinition addMethod)
        {
            var processor = addMethod.Body.GetILProcessor();
            var addInsn = addMethod.Body.Instructions.Single(i => i.OpCode == OpCodes.Add);
            var loadOne = processor.Create(OpCodes.Ldc_I4_1);
            var add = processor.Create(OpCodes.Add);
            processor.InsertAfter(addInsn, loadOne);
            processor.InsertAfter(loadOne, add);
        }
    }

    public class SampleFrameworkInjector : IInjector
    {
        public int ExecutionPriority { get; set; } = 0;

        [MemberInject("System.Int32 ILject.SampleFrameworkPatchTarget.Program::Add(System.Int32,System.Int32)", MemberType.Method)]
        public void InjectProgramAdd(MethodDefinition addMethod)
        {
            var processor = addMethod.Body.GetILProcessor();
            var addInsn = addMethod.Body.Instructions.Single(i => i.OpCode == OpCodes.Add);
            var loadOne = processor.Create(OpCodes.Ldc_I4_1);
            var add = processor.Create(OpCodes.Add);
            processor.InsertAfter(addInsn, loadOne);
            processor.InsertAfter(loadOne, add);
        }
    }
}
