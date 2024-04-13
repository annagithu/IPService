using IPService.Classes;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace IPService
{
    internal partial class Program
    {
        [GeneratedRegex(@"\s+")]
        private static partial Regex deleteWhiteSpaces();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("enter parameters or, to read a config file, enter path");
            CommandHelper commandHelper = new();
            while (true)
            {
                var input = Console.ReadLine().Replace("\"", "");
                input = deleteWhiteSpaces().Replace(input, string.Empty);

                if (commandHelper.IsSpecificInput(input)) { commandHelper.SpecificInputHandle(input); continue; } // если введенное слово - команда help или exit 

                if (commandHelper.IsUnvalidInput(input)) { commandHelper.UnvalidInputHandle(); continue; } // если ввели что-то неправильное

                if (commandHelper.IsConfigPath(input)) input = commandHelper.ReadFromConfig(input); // если ввели путь к файлу конфигурации

                Arguments commandArgs = commandHelper.ParseArguments(input);

                Process(commandArgs, commandHelper);
            }
        }

        static void Process(Arguments arguments, CommandHelper commandHelper)
        {
            try
            {
                AddressProcessor addressProcessing = new();
                var filteredAddresses = AddressProcessor.FilterAddresses(arguments);
                var ipCounts = AddressProcessor.CountIPAddresses(filteredAddresses);
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
