﻿using Remove.Classes;
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

        public static void Main(string[] args)
        {
            try
            {
                properties = new Properties(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\nДля вывода спраки введите remove /?");
                return;
            }

            if (properties.Help)
            {
                // TODO: Вывод справки.
                Console.WriteLine("""
                    Программа Remove может выполнить поиск или удаление файлов и файлов в указанной папке.
                    Синтиксис:
                    remove [опции] [путь к директории]

                    Опции:
                    /mode         Режим работы.
                        find      Поиск файлов или папок.
                        remove    Удаление файлов или папок.
                    /p            Паттерн (регулярное выражение).
                    /d            Поиск директорий.
                    /f            Поиск файлов.
                    /r            Рекурсивная работа программы.
                    /?            Вывод справки.
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