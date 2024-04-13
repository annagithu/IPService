# IPService
Дан  файл, содержащий список IP-адресов протокола IPv4 из журнала доступа. На каждой строке записан адрес и время, в которое с него пришёл запрос.

Необходимо разработать консольное приложение, которое  выводит в файл список IP-адресов из файла журнала, входящих в указанный диапазон с количеством обращений с этого адреса в указанный интервал времени.

## commands:
```
+ --file-log — путь к файлу с логами
+ --file-output — путь к файлу с результатом
+ --address-start —  нижняя граница диапазона адресов, необязательный параметр, по умолчанию обрабатываются все адреса
+ --address-mask — маска подсети, задающая верхнюю границу диапазона десятичное число. Необязательный параметр. В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start.
+ --time-start —  нижняя граница временного интервала.
+ --time-end — верхняя граница временного интервала.
+ help - выводит список команд.
+ exit - выходит из консольного приложения.
```
для чтения файла конфигурации необходимо просто написать путь к этому файлу.

## examples
примеры файлов лежат в папке journals
* test.log - входные данные
* output.txt - выходные данные
* config.txt - файл конфигурации, в котором лежит команда
