using IPService.Classes;
using System.Globalization;

namespace IPService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter parameters or, to read a config file, enter path");
            CommandHelper commandHelper = new();
            while (true)
            {
                var input = Console.ReadLine().Replace("\"", "");

                if (commandHelper.IsSpecificInput(input)) { commandHelper.SpecificInputHandle(input); continue; } // если введенное слово - команда help или exit 

                if (commandHelper.IsUnvalidInput(input)) { commandHelper.UnvalidInputHandle(); continue; } // если ввели что-то неправильное

                if (commandHelper.IsConfigPath(input)) input = commandHelper.ReadFromConfig(input); // если ввели путь к файлу конфигурации

                Arguments commandArgs = commandHelper.ParseArguments(input);

                Process(commandArgs);
            }
        }

        static void Process(Arguments arguments)
        {
            CommandHelper commandHelper = new();
            try
            {
                AddressProcessing addressProcessing = new();
                var filteredAddresses = AddressProcessing.FilterAddresses(arguments);
                var ipCounts = AddressProcessing.CountIPAddresses(filteredAddresses);
                commandHelper.WriteResultsToFile(arguments.OutputFilePath, ipCounts);

                Console.WriteLine("successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
