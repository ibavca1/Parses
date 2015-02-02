using System;
using System.Collections.Generic;

namespace WebParsingFramework.Runtime.Messaging
{
    public interface IMessageQueue
    {
        string Name { get; }

        int Count { get; }

        void Clear();

        void Send(QueueMessage message, TimeSpan? initialVisibilityDelay = null);

        QueueMessage Receive(TimeSpan? visibilityTimeout = null);

        IEnumerable<QueueMessage> Receive(int count, TimeSpan? visibilityTimeout = null);

        void Complete(QueueMessage message);

        QueueMessage Peek();

        IEnumerable<QueueMessage> Peek(int count);
    }
}