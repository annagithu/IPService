using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPService.Classes
{
    public class CommandHelper
    {
        public bool IsSpecificInput(string input)
        {
            input = input.ToUpper();
            return (input == "EXIT" || input == "HELP");
        }

        public void SpecificInputHandle(string input)
        {
            input = input.ToUpper();
            if (input == "HELP") Console.WriteLine(String.Join("\n ", Helper.AvailableCommand));
            if (input == "EXIT") Environment.Exit(0);
        }

        public bool IsUnvalidInput(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public void UnvalidInputHandle()
        {
            Console.WriteLine("you entered invalid value. there is valid commands:");
            SpecificInputHandle("help");
        }

        
        public Arguments ParseArguments(string input)
        {
            var args = input.Split(' ');
            Arguments arguments = new();
            for (int i = 0; i < args.Length; i +=2)
            {
                switch (args[i].Trim())
                {
                    case "--file-log":
                        arguments.LogFilePath = args[i + 1];
                        break;
                    case "--file-output":
                        arguments.OutputFilePath = args[i + 1];
                        break;
                    case "--address-start":
                        arguments.StartAddress = args[i + 1];
                        break;
                    case "--address-mask":
                        arguments.Mask = int.Parse(args[i + 1]);
                        break;
                    case "--time-start":
                        arguments.StartTime = DateTime.ParseExact(args[i + 1], "dd.MM.yyyy", null);
                        break;
                    case "--time-end":
                        arguments.EndTime = DateTime.ParseExact(args[i + 1], "dd.MM.yyyy", null);
                        break;
                    default:
                        Console.WriteLine($"unknown argument: {args[i]}");
                        break;
                }
            }

            if (arguments.StartAddress == null && arguments.Mask != null)
            {
                arguments.Mask = null;
                Console.WriteLine("sorry, but if the address_start parameter is not entered, then the address_mask parameter is ignored");
            }
            return arguments;
        }

            

        public bool IsConfigPath(string path)
        {
            return File.Exists(path);
        }

        public string ReadFromConfig(string configFilePath)
        {
            try
            {
                using var reader = new StreamReader(configFilePath);
                var command = reader.ReadToEnd();
                reader.Close();
                return command;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return configFilePath;
            }

        }

        public void WriteResultsToFile(string filePath, Dictionary<string, int> ipCounts)
        {
            filePath = filePath.Replace("\"", "");
            if (!File.Exists(filePath))
            {
                filePath = IsWrongPath();
            }
            try
            {
                using var writer = new StreamWriter(filePath, true);
                writer.WriteLine(DateTime.Now + "\n");
                foreach (var pair in ipCounts)
                {
                    writer.WriteLine($"{pair.Key}: {pair.Value} \n");
                }
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"an error occurred while writing to the output file: {ex.Message}");
            }

        }

        public string IsWrongPath()
        {
            Console.WriteLine("the path to the output file is null or empty. do you wanna create output file? (enter 'yes' or 'no')");
            if (Console.ReadLine() == "yes")
            {
                Console.WriteLine("enter a new path to output file");
                var newPath = Console.ReadLine().Replace("\"", "");
                CreateNewOutputFile(newPath);
                return newPath;
            }
            else
            {
                Console.WriteLine("okay, then please enter the correct path to output file");
                return Console.ReadLine().Replace("\"", "");
            }
        }

        public void CreateNewOutputFile(string path)
        {
            FileStream fileStream = File.Create(path);
            fileStream.Close();
        }
    }
}
