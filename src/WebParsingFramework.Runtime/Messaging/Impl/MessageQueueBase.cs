using System;
using System.Collections.Generic;
using System.Linq;

namespace WebParsingFramework.Runtime.Messaging.Impl
{
    public abstract class MessageQueueBase : IMessageQueue
    {
        protected MessageQueueBase(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            Name = name;
        }

        public string Name { get; private set; }

        public int Count
        {
            get { return DoCount(); }
        }

        public void Clear()
        {
            DoClear();
        }

        public void Send(QueueMessage queueMessage, TimeSpan? initialVisibilityDelay = null)
        {
            if (queueMessage == null)
                throw new ArgumentNullException("queueMessage");

            TimeSpan delay =
                initialVisibilityDelay ?? TimeSpan.Zero;

            Message message = Message.FromMessage(queueMessage);
            DoSend(message, delay);
        }

        public QueueMessage Receive(TimeSpan? visibilityTimeout = null)
        {
            return Receive(1, visibilityTimeout).FirstOrDefault();
        }

        public IEnumerable<QueueMessage> Receive(int count, TimeSpan? visibilityTimeout = null)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            if (count == 0)
                return Enumerable.Empty<QueueMessage>();

            TimeSpan timeout =
               visibilityTimeout ?? QueueMessage.DefaultVisabilityTimeout;
            IEnumerable<Message> messages = DoReceive(count, timeout);
            return messages.Select(msg => msg.ToMessage());
        }

        public void Complete(QueueMessage queueMessage)
        {
            if (queueMessage == null)
                throw new ArgumentNullException("queueMessage");

            Message message = Message.FromMessage(queueMessage);
            DoComplete(message);
        }

        public QueueMessage Peek()
        {
            return Peek(1).FirstOrDefault();
        }

        public IEnumerable<QueueMessage> Peek(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            if (count == 0)
                return Enumerable.Empty<QueueMessage>();

            IEnumerable<Message> messages = DoPeek(count);
            return messages.Select(msg => msg.ToMessage());
        }

        protected abstract int DoCount();

        protected abstract void DoClear();

        protected abstract void DoSend(Message message, TimeSpan initialVisibilityDelay);

        protected abstract IEnumerable<Message> DoReceive(int count, TimeSpan visibilityTimeout);

        protected abstract void DoComplete(Message message);

        protected abstract IEnumerable<Message> DoPeek(int count);
    }
}