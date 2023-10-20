namespace ErrorQueue.Services
{
    public interface IQueueService
    {
        void Enqueue<T>(T message);
        T Dequeue<T>();
    }
}
