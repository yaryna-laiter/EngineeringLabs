namespace MemoryLeakLab.Services
{
    public class LeakySubscriber
    {
        private readonly byte[] _payload;

        public LeakySubscriber()
        {
            _payload = new byte[100 * 1024];

            StaticEventSource.SomethingHappened += OnSomethingHappened;
        }

        private void OnSomethingHappened(object? sender, EventArgs e)
        {
        }
    }
}
