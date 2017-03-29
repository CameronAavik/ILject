using System;

namespace PEPatcher.SamplePatchTarget
{
    internal class Program
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            Console.WriteLine(Add(1, 1) == 2 ? "1 + 1 = 2" : "1 + 1 != 2");
        }

        private static int Add(int a, int b) => a + b;
    }
}