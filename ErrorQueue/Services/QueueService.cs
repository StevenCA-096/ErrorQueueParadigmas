namespace ErrorQueue.Services
{
    public class QueueService
    {
        private readonly Queue<object> _queue = new Queue<object>();

        public void Enqueue<T>(T message)
        {
            _queue.Enqueue(message);
        }

        public T Dequeue<T>()
        {
            return (T)_queue.Dequeue();
        }
    }
}
