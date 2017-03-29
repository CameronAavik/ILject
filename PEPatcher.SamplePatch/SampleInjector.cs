using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using PEPatcher.Core;

namespace PEPatcher.SamplePatch
{
    public class SampleInjector : IInjector
    {
        public int ExecutionPriority { get; set; } = 0;

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
