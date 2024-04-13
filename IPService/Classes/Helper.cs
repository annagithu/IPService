using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPService.Classes
{
    public static class Helper
    {
        private const string Help = "help - список доступных команд";
        private const string Exit = "exit - выход из приложения";
        private const string FileLog = "--file-log - путь к файлу с логами";
        private const string FileOutput = "--file-output — путь к файлу с результатом";
        private const string AddressStart = "--address-start —  нижняя граница диапазона адресов, необязательный параметр, по умолчанию обрабатываются все адреса";
        private const string AddressMask = "--address-mask — маска подсети, задающая верхнюю границу диапазона десятичное число. Необязательный параметр. В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start";
        private const string TimeStart = "--time-start —  нижняя граница временного интервала";
        private const string TimeEnd = "--time-end — верхняя граница временного интервала.";
        private const string Example = "пример: --file-log C:\\.projects\\IPService\\journals\\test.log --file-output C:\\.projects\\IPService\\journals\\output.txt --time-start 01.02.2024 --time-end 01.05.2024";
        public static readonly IReadOnlyList<string> AvailableCommand = [Help, Exit, FileLog, FileOutput, AddressStart, AddressMask, TimeStart, TimeEnd, Example];
    }
}
