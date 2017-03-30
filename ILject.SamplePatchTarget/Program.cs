using System;

namespace ILject.SamplePatchTarget
{
    internal class Program
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args) => Console.WriteLine($"1 + 1 = {Add(1, 1)}");

        private static int Add(int a, int b) => a + b;
    }
}
