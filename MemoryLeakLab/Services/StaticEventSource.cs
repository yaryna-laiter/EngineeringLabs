namespace MemoryLeakLab.Services
{
    public static class StaticEventSource
    {
        public static event EventHandler? SomethingHappened;

        public static int SubscriberCount =>
            SomethingHappened?.GetInvocationList().Length ?? 0;

        public static void Raise()
        {
            SomethingHappened?.Invoke(null, EventArgs.Empty);
        }
    }
}
