using Remove.Classes;
using Remove.Interfaces;
using Remove.Structures;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Remove
{
    public class Program
    {
        /// <summary>
        /// Параметры запуска.
        /// </summary>
        private static Properties properties;

        /// <summary>
        /// Команда над папками.
        /// </summary>
        private static ICommand directoryCommand;

        /// <summary>
        /// Команда над файлами.
        /// </summary>
        private static ICommand fileCommand;

        /// <summary>
        /// Количество затронутых папок.
        /// </summary>
        private static int directoriesCount;

        /// <summary>
        /// Количество затронутых файлов.
        /// </summary>
        private static int filesCount;

        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        /// <param name="args">Параметры запуска программы.</param>
        public static void Main(string[] args)
        {
            try
            {
                properties = new Properties(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\nДля вывода спраки введите remove -h");
                return;
            }

            if (properties.Help)
            {
                Console.WriteLine(
                    """
                    Программа Remove может выполнить поиск или удаление файлов и файлов в указанной папке с помощью регулярных выражений.
                    Синтиксис:
                    Remove [опции] [путь к директории]

                    Опции:
                    --mode (-m)           Режим работы.
                        find              Поиск файлов или папок (по умолчанию).
                        remove            Удаление файлов или папок.
                    --pattern (-p)        Паттерн (регулярное выражение).
                    -d                    Поиск директорий (по умолчанию, если не -f).
                    -f                    Поиск файлов (по умолчанию, если не -d).
                    --recursively (-r)    Рекурсивная работа программы.
                    --help (-h)           Вывод справки.
                    """);
                return;
            }

            switch (properties.Mode)
            {
                case Enums.Mode.Remove:
                    directoryCommand = new RemoveDirectoryCommand();
                    fileCommand = new RemoveFileCommand();
                    break;
                default:
                    directoryCommand = new ViewCommand();
                    fileCommand = new ViewCommand();
                    break;
            }

            Console.WriteLine("Начало выполнения программы.");

            DirectoryInfo directoryInfo = new DirectoryInfo(properties.DirectoryPath);
            CheckDirectory(directoryInfo);

            Console.WriteLine("\nНайдено:");

            if (properties.SearchDirectories)
            {
                Console.WriteLine($"Папок: {directoriesCount}");
            }

            if (properties.SearchFiles)
            {
                Console.WriteLine($"Файлов: {filesCount}");
            }

            Console.WriteLine("\nПрограмма завершила своё выполнение.");
        }

        /// <summary>
        /// Проверка директории на наличие подходящих файлов.
        /// </summary>
        /// <param name="directoryInfo">Директория.</param>
        private static void CheckDirectory(DirectoryInfo directoryInfo)
        {
            try
            {
                if (properties.SearchFiles)
                {
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        if (Regex.IsMatch(file.FullName, properties.Pattern))
                        {
                            fileCommand.Execute(file.FullName);
                            filesCount++;
                        }
                    }
                }

                if (properties.SearchDirectories)
                {
                    foreach (DirectoryInfo subDir in directoryInfo.GetDirectories())
                    {
                        if (Regex.IsMatch(subDir.FullName, properties.Pattern))
                        {
                            directoryCommand.Execute(subDir.FullName);
                            directoriesCount++;
                        }

                        CheckDirectory(subDir);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}