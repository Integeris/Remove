using Remove.Interfaces;
using System;

namespace Remove.Classes
{
    /// <summary>
    /// Команды вывода.
    /// </summary>
    public class ViewCommand : ICommand
    {
        /// <summary>
        /// Создание команды вывода.
        /// </summary>
        public ViewCommand() { }

        /// <summary>
        /// Вывести объект.
        /// </summary>
        /// <param name="itemPath">Путь к объекту.</param>
        void ICommand.Execute(string itemPath)
        {
            Console.WriteLine(itemPath);
        }
    }
}
