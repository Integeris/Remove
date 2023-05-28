namespace Remove.Interfaces
{
    /// <summary>
    /// Команда.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Выполнить.
        /// </summary>
        public void Execute(string itemPath);
    }
}
