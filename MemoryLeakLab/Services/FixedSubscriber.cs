namespace MemoryLeakLab.Services
{
    public class FixedSubscriber : IDisposable
    {
        private readonly byte[] _payload;
        private bool _disposed;

        public FixedSubscriber()
        {
            _payload = new byte[100 * 1024];

            StaticEventSource.SomethingHappened += OnSomethingHappened;
        }

        private void OnSomethingHappened(object? sender, EventArgs e)
        {
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            StaticEventSource.SomethingHappened -= OnSomethingHappened;

            _disposed = true;
        }
    }
}
