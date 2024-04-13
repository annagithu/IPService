using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPService.Classes
{
    public class AddressProcessor
    {
        //сортировка адресов по условиям времени и диапазону
        public static IEnumerable<string> FilterAddresses(Arguments arguments)
        {
            if (string.IsNullOrEmpty(arguments.LogFilePath))
            {
                throw new ArgumentNullException(nameof(arguments.LogFilePath), $"path {arguments.LogFilePath} is null or empty. try again, or enter 'help' to see the commands");
            }

            var addresses = new List<string>();
            try
            {
                using StreamReader reader = new(arguments.LogFilePath);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var address = line.Split(':')[0];
                    var timeParts = string.Join(":", line.Split(':').Skip(1)).Split(' ');
                    var allTime = $"{timeParts[0]} {timeParts[1]}";
                    var time = DateTime.ParseExact(allTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                    if (time >= arguments.StartTime && time <= arguments.EndTime && IPInRange(address, arguments.StartAddress, arguments.Mask))
                    {
                        addresses.Add(address);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"an error occurred while reading the log file: {ex.Message}");
            }

            return addresses;
        }


        //получаем, входит ли адрес в диапазон маски подсети
        static bool IPInRange(string ip, string startAddress, int? mask)
        {
            if (!mask.HasValue)
            {
                return true;
            }

            var ipAddress = System.Net.IPAddress.Parse(ip).GetAddressBytes();
            var startIpAddress = System.Net.IPAddress.Parse(startAddress).GetAddressBytes();

            byte[] maskBytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                if (mask > 8)
                {
                    maskBytes[i] = 255;
                    mask -= 8;
                }
                else if (mask == 8)
                {
                    maskBytes[i] = 255;
                    mask = 0;
                }
                else
                {
                    maskBytes[i] = (byte)(255 - (Math.Pow(2, 8 - (int)mask) - 1));
                    mask = 0;
                }
            }

            for (int i = 0; i < startIpAddress.Length; i++)
            {
                startIpAddress[i] = (byte)(startIpAddress[i] & maskBytes[i]);
            }

            for (int i = 0; i < ipAddress.Length; i++)
            {
                if (ipAddress[i] != startIpAddress[i])
                {
                    return false;
                }
            }
            return true;
        }


        //получаем количество обращений с адреса
        public static Dictionary<string, int> CountIPAddresses(IEnumerable<string> addresses)
        {
            var ipCounts = new Dictionary<string, int>();
            foreach (var address in addresses)
            {
                if (!ipCounts.ContainsKey(address))
                    ipCounts[address] = 0;
                ipCounts[address]++;
            }
            return ipCounts;
        }
    }
}
