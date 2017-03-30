using System;
using System.Collections.Generic;
using ILject.Core;

namespace ILject.SamplePatch
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
                var context = new PatchContext(arguments.CoreExecutableName);
                context.LoadInjectors(new[] {new SampleCoreInjector()});
                context.RunInjectors();
                context.Run();

                Console.WriteLine($"Finished running .NET Core injection sample.{Environment.NewLine}Press Any key to continue");
                Console.ReadKey(true);

                context = new PatchContext(arguments.FrameworkExecutableName);
                context.LoadInjectors(new[] {new SampleFrameworkInjector()});
                context.RunInjectors();
                context.Run();
                Console.WriteLine("Finished running .NET Framework injection sample.");
            }
            Console.WriteLine("Press any key to quit");
            Console.ReadKey(true);
        }

        private static Arguments ValidateArguments(IReadOnlyList<string> args)
        {
            var arguments = new Arguments {IsValid = args.Count == 2};
            if (arguments.IsValid)
            {
                arguments.CoreExecutableName = args[0];
                arguments.FrameworkExecutableName = args[1];
            }
            else
            {
                Console.Error.WriteLine("Invalid usage. Correct usage is \"ILject core-filename framework-filename\"");
            }
            return arguments;
        }

        internal class Arguments
        {
            public bool IsValid { get; set; }
            public string CoreExecutableName { get; set; }
            public string FrameworkExecutableName { get; set; }
        }
    }
}
