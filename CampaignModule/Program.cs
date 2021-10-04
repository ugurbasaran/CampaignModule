using System;
using System.IO;
using System.Reflection;
using System.Text;
using CampaignModule.Command;

namespace CampaignModule
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] splittedCommands = ReadCommandFile();
                CommandExecutor commandExecutor = new CommandExecutor();
                foreach (string commandQuery in splittedCommands)
                {
                    commandExecutor.Execute(commandQuery);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        private static string[] ReadCommandFile()
        {
            string commandFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Command.txt");
            string[] commandLine = File.ReadAllLines(commandFilePath);
            return commandLine;
        }
    }
}
