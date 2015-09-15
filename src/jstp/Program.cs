using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Console734.Connector;
using ManyConsole;
using Mono.Options;
using NLog;

namespace jstp
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static int Main(string[] args)
        {
            PrintWelcome();
            var commands = ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
            try
            {
                if (args.Length == 0)
                {
                    while (true)
                    {
                        Console.Write(">");
                        var readLine = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(readLine))
                        {
                            args = readLine.Split(' ');
                            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
                        }
                    }
                }
                return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unhandled exception");
                return -1;
            }
        }

        private static void PrintWelcome()
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("       __   _____    ______   ____ ");
            Console.WriteLine(@"      / /  / ___/   /_  __/  / __ \");
            Console.WriteLine(@" __  / /   \__ \     / /    / /_/ /");
            Console.WriteLine(@"/ /_/ /   ___/ /    / /    / ____/ ");
            Console.WriteLine(@"\____/   /____/    /_/    /_/      ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("JSon Text Protocol generator {0}",AppInfo.Version);
            Console.WriteLine(AppInfo.CopyrightHolder);
            Console.WriteLine();
            Console.ForegroundColor = old;
                                   
        }

    }
}
