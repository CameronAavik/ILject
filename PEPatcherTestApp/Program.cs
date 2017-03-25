using System;

namespace PEPatcherTestApp
{
    internal class Program
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            const int lhs = 1 + 1;
            const int rhs = 2;
            // ReSharper disable once UnreachableCode
            Console.WriteLine(lhs == rhs ? "1 + 1 = 2" : "1 + 1 ≠ 2");
            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}