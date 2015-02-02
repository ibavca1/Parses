using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebParsingFramework.Runtime.Messaging.Impl;

namespace WebParsingFramework.Runtime.Messaging.InMemory
{
    internal class InMemoryMessageQueue : MessageQueueBase
    {
        private readonly ReaderWriterLock _lock = new ReaderWriterLock();
        private readonly List<Message> _queue = new List<Message>();

        public InMemoryMessageQueue(string name)
            : base(name)
        {
            OperationTimeout = TimeSpan.FromSeconds(5);
        }

        public TimeSpan OperationTimeout { get; private set; }

        protected override void DoClear()
        {
            try
            {
                _lock.AcquireWriterLock(OperationTimeout);
                try
                {
                    _queue.Clear();
                }
                finally
                {
                    _lock.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
                throw new TimeoutException();
            }
        }

        protected override void DoSend(Message message, TimeSpan initialVisibilityDelay)
        {
            try
            {
                _lock.AcquireWriterLock(OperationTimeout);
                try
                {
                    DateTime now = DateTime.UtcNow;
                    var msg = new Message
                    {
                        Content = message.Content,
                        DeliveryCount = 0,
                        EnqueuedTimeUtc = now,
                        ExpiresAtUtc = now.Add(message.TimeToLive),
                        Id = Guid.NewGuid().ToString(),
                        LockedUntilUtc = now.Add(initialVisibilityDelay)
                    };
                    _queue.Add(msg);
                }
                finally
                {
                    _lock.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
                throw new TimeoutException();
            }
        }

        protected override IEnumerable<Message> DoReceive(int count, TimeSpan visibilityTimeout)
        {
            return DoReceive(count, visibilityTimeout, false);
        }

        protected override void DoComplete(Message message)
        {
            try
            {
                _lock.AcquireWriterLock(OperationTimeout);
                try
                {
                    for (int i = 0; i < _queue.Count; i++)
                    {
                        if (_queue[i].Id == message.Id)
                        {
                            _queue.RemoveAt(i);
                            break;
                        }
                    }
                }
                finally
                {
                    _lock.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
                throw new TimeoutException();
            }
        }

        protected override IEnumerable<Message> DoPeek(int count)
        {
            return DoReceive(count, TimeSpan.Zero, true);
        }

        protected override int DoCount()
        {
            try
            {
                _lock.AcquireReaderLock(OperationTimeout);
                try
                {
                    return _queue.Count;
                }
                finally
                {
                    _lock.ReleaseReaderLock();
                }
            }
            catch (ApplicationException)
            {
                throw new TimeoutException();
            }
        }

        private IEnumerable<Message> DoReceive(int count, TimeSpan visibilityTimeout, bool peek)
        {
            try
            {
                _lock.AcquireWriterLock(OperationTimeout);
                try
                {
                    var messages = new List<Message>();
                    for (int i = 0; i < count; i++)
                    {
                        Message message = GetMessage();
                        if (message == null)
                            break;
                        if (!peek)
                        {
                            message.LockedUntilUtc = DateTime.UtcNow.Add(visibilityTimeout);
                            message.DeliveryCount++;
                        }
                        messages.Add(message);
                    }

                    return messages;
                }
                finally
                {
                    _lock.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
                throw new TimeoutException();
            }
        }

        private Message GetMessage()
        {
            DateTime now = DateTime.UtcNow;
            return _queue.FirstOrDefault(message => message.ExpiresAtUtc > now && message.LockedUntilUtc <= now);
        }
    }
}