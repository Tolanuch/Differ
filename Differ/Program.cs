using System;
using System.IO;

namespace Differ
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CommandProcess command = new Snapshot();
                if (args[0].ToString() == "snapshot")                
                    command = new Snapshot();                
                if (args[0].ToString() == "compare")                
                    command = new Compare();
                else
                {
                    Console.WriteLine("The \""+args[0]+"\" is incorrect command for program.");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }
                command.process(args);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("The application did not receive the arguments.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
