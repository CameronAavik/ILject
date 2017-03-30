using System;
using System.Collections.Generic;
using PEPatcher.Core;

namespace PEPatcher.SamplePatch
{
    internal class Program
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once SuggestBaseTypeForParameter
        private static void Main(string[] args)
        {
            var arguments = ValidateArguments(args);
            if (arguments.IsValid)
            {
                var context = new PatchContext(arguments.ExecutableName);
                context.LoadInjectors(new[] {new SampleInjector()});
                context.RunInjectors();
                context.Run();
            }
            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }

        private static Arguments ValidateArguments(IReadOnlyList<string> args)
        {
            var arguments = new Arguments {IsValid = args.Count == 1};
            if (arguments.IsValid)
            {
                arguments.ExecutableName = args[0];
            }
            else
            {
                Console.Error.WriteLine("Invalid usage. Correct usage is \"PEPatcher filename\"");
            }
            return arguments;
        }

        internal class Arguments
        {
            public bool IsValid { get; set; }
            public string ExecutableName { get; set; }
        }
    }
}
