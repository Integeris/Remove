using Remove.Interfaces;
using System;
using System.IO;

namespace Remove.Classes
{
    /// <summary>
    /// Команда удаления файла.
    /// </summary>
    public class RemoveFileCommand : ICommand
    {
        /// <summary>
        /// Создание команды удаления файла.
        /// </summary>
        public RemoveFileCommand() { }

        /// <summary>
        /// Удалить файл.
        /// </summary>
        /// <param name="itemPath">Путь к файлу.</param>
        void ICommand.Execute(string itemPath)
        {
            File.Delete(itemPath);
            Console.WriteLine(itemPath);
        }
    }
}
