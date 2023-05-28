using Remove.Enums;
using System;
using System.IO;

namespace Remove.Structures
{
    /// <summary>
    /// Введённые параметры командной строки.
    /// </summary>
    public readonly struct Properties
    {
        /// <summary>
        /// Директория поиска.
        /// </summary>
        private readonly string directoryPath;

        /// <summary>
        /// Режим работы утилиты.
        /// </summary>
        private readonly Mode mode;

        /// <summary>
        /// Шаблон поиска (регулярное выражение).
        /// </summary>
        private readonly string pattern;

        /// <summary>
        /// Искать ли папки.
        /// </summary>
        private readonly bool searchDirectories;

        /// <summary>
        /// Искать ли файлы.
        /// </summary>
        private readonly bool searchFiles;

        /// <summary>
        /// Рекурсивен ли поиск.
        /// </summary>
        private readonly bool recursively;

        /// <summary>
        /// Справка.
        /// </summary>
        private readonly bool help;

        /// <summary>
        /// Директория поиска.
        /// </summary>
        public string DirectoryPath
        {
            get => directoryPath;
        }

        /// <summary>
        /// Режим работы утилиты.
        /// </summary>
        public Mode Mode
        {
            get => mode;
        }

        /// <summary>
        /// Шаблон поиска (регулярное выражение).
        /// </summary>
        public string Pattern
        {
            get => pattern;
        }

        /// <summary>
        /// Искать ли папки.
        /// </summary>
        public bool SearchDirectories
        {
            get => searchDirectories;
        }

        /// <summary>
        /// Искать ли файлы.
        /// </summary>
        public bool SearchFiles
        {
            get => searchFiles;
        }

        /// <summary>
        /// Рекурсивен ли поиск.
        /// </summary>
        public bool Recursively
        {
            get => recursively;
        }

        /// <summary>
        /// Справка.
        /// </summary>
        public bool Help
        {
            get => help;
        }

        /// <summary>
        /// Создание пустых настроек.
        /// </summary>
        public Properties()
        {
            mode = Mode.Find;
        }

        /// <summary>
        /// Создание параметров, введённых в командную строку.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Properties(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "/?":
                        help = true;
                        break;
                    case "/mode":
                        if (++i >= args.Length)
                        {
                            throw new ArgumentNullException("/mode", "Отсутсвует режим для параметра.");
                        }

                        switch (args[i].ToLower())
                        {
                            case "find":
                                break;
                            case "remove":
                                break;
                            default:
                                throw new ArgumentException("Неизвестный режим праметра /mode.", args[i]);
                        }
                        break;
                    case "/p":
                        if (++i >= args.Length)
                        {
                            throw new ArgumentNullException("/p", "Отсутсвует паттерн для параметра.");
                        }

                        pattern = args[i];
                        break;
                    case "/d":
                        searchDirectories = true;
                        break;
                    case "/f":
                        searchFiles = true;
                        break;
                    case "/r":
                        recursively = true;
                        break;
                    default:
                        if (!Directory.Exists(args[i]))
                        {
                            throw new ArgumentException("Введён неизвестный параметр или директория.", args[i]);
                        }
                        else if (directoryPath != null)
                        {
                            throw new ArgumentException("Директория поиска уже казанна.", args[i]);
                        }

                        directoryPath = args[i];
                        break;
                }
            }

            if (searchDirectories == false && searchFiles == false)
            {
                searchDirectories = true;
                searchFiles = true;
            }

            directoryPath ??= Environment.CurrentDirectory;
            pattern ??= ".*";
        }
    }
}
