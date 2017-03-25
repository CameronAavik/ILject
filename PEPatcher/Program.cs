using System;
using System.Collections.Generic;

namespace PEPatcher
{
    internal class Program
    {
        public Program(Arguments arguments)
        {
            Arguments = arguments;
        }

        public Arguments Arguments { get; set; }

        public void Run() {}

        #region Entry Point and Setup

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once SuggestBaseTypeForParameter
        private static void Main(string[] args)
        {
            var arguments = ValidateArguments(args);
            if (arguments.IsValid)
            {
                var program = new Program(arguments);
                program.Run();
            }
            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }

        private static Arguments ValidateArguments(IReadOnlyList<string> args)
        {
            var arguments = new Arguments {IsValid = false};
            if (args.Count != 1)
            {
                Console.Error.WriteLine("Invalid usage. Correct usage is \"PEPatcher filename\"");
                return arguments;
            }
            arguments.ExecutableName = args[0];
            return arguments;
        }

        #endregion
    }

    internal class Arguments
    {
        public bool IsValid { get; set; }
        public string ExecutableName { get; set; }
    }
}