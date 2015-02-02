using System;

namespace WebParsingFramework.Runtime.Messaging.Impl
{
    public abstract class MessageQueueManagerBase : IMessageQueueManager
    {
        public IMessageQueue GetQueue(string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            return DoGetQueue(queueName);
        }

        public void CreateQueue(string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            DoCreateQueue(queueName);
        }

        public void DeleteQueue(string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            DoDeleteQueue(queueName);
        }

        public bool QueueExists(string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            return DoQueueExists(queueName);
        }

        protected abstract IMessageQueue DoGetQueue(string queueName);

        protected abstract void DoCreateQueue(string queueName);

        protected abstract void DoDeleteQueue(string queueName);

        protected abstract bool DoQueueExists(string queueName);
    }
}