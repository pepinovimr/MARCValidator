namespace ApplicationLayer
{
    public class ConsoleViewModel
    {
        public event EventHandler<string> Notify;

        /// <summary>
        /// Temporary
        /// </summary>
        public void PerformApplicationLogic()
        {
            Notify?.Invoke(this, "Píšu z ViewModelu");
        }
    }
}