using Remove.Interfaces;
using System;
using System.IO;

namespace Remove.Classes
{
    /// <summary>
    /// Команда удаления папки.
    /// </summary>
    public class RemoveDirectoryCommand : ICommand
    {
        /// <summary>
        /// Создание команды удаления папки.
        /// </summary>
        public RemoveDirectoryCommand() { }

        /// <summary>
        /// Удалить папку.
        /// </summary>
        /// <param name="itemPath">Путь к папке.</param>
        void ICommand.Execute(string itemPath)
        {
            Directory.Delete(itemPath, true);
            Console.WriteLine(itemPath);
        }
    }
}
