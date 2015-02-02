using System;

namespace WebParsingFramework.Runtime.Messaging.Impl
{
    public class Message
    {
        public string Id;

        public byte[] Content;

        public int DeliveryCount;

        public DateTime ExpiresAtUtc;

        public DateTime EnqueuedTimeUtc;

        public DateTime LockedUntilUtc;

        public TimeSpan TimeToLive;

        public static Message FromMessage(QueueMessage message)
        {
            return new Message
                       {
                           Id = message.IsEnqueued ? message.MessageId : null,
                           Content = message.ContentInternal,
                           TimeToLive = message.TimeToLive
                       };
        }

        public QueueMessage ToMessage()
        {
            return new QueueMessage
                       {
                           ContentInternal = Content,
                           DeliveryCountIntenal = DeliveryCount,
                           EnqueuedTimeUtcInternal = EnqueuedTimeUtc,
                           ExpiresAtUtcInternal = ExpiresAtUtc,
                           IsEnqueued = true,
                           LockedUntilUtcInternal = LockedUntilUtc,
                           MessageIdInternal = Id,
                           TimeToLiveInternal = TimeToLive
                       };
        }
    }
}