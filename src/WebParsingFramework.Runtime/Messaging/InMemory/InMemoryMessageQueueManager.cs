using System;
using System.Collections.Generic;
using System.Linq;
using WebParsingFramework.Runtime.Messaging.Impl;

namespace WebParsingFramework.Runtime.Messaging.InMemory
{
    public class InMemoryMessageQueueManager : MessageQueueManagerBase
    {
        private readonly List<InMemoryMessageQueue> _queues = new List<InMemoryMessageQueue>();
        private readonly object _lock = new object();

        protected override IMessageQueue DoGetQueue(string queueName)
        {
            lock (_lock)
            {
                InMemoryMessageQueue queue = GetWithNoLock(queueName);
                if (queue == null)
                    throw new InvalidOperationException("Queue not found");
                return queue;
            }
        }

        protected override void DoCreateQueue(string queueName)
        {
            lock (_lock)
            {
                InMemoryMessageQueue queue = GetWithNoLock(queueName);
                if (queue == null)
                {
                    queue = new InMemoryMessageQueue(queueName);
                    _queues.Add(queue);
                }
            }
        }

        protected override void DoDeleteQueue(string queueName)
        {
            lock (_lock)
            {
                InMemoryMessageQueue queue = GetWithNoLock(queueName);
                if (queue != null)
                {
                    _queues.Remove(queue);
                }
            }
        }

        protected override bool DoQueueExists(string queueName)
        {
            lock (_lock)
            {
                InMemoryMessageQueue queue = GetWithNoLock(queueName);
                return queue != null;
            }
        }

        private InMemoryMessageQueue GetWithNoLock(string name)
        {
            return 
                _queues.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}