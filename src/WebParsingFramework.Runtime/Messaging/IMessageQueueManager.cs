namespace WebParsingFramework.Runtime.Messaging
{
    public interface IMessageQueueManager
    {
        IMessageQueue GetQueue(string queueName);

        void CreateQueue(string queueName);

        void DeleteQueue(string queueName);

        bool QueueExists(string queueName);
    }
}