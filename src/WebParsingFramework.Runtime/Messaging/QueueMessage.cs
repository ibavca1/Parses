using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace WebParsingFramework.Runtime.Messaging
{
    public class QueueMessage
    {
        public static readonly TimeSpan MaxTimeToLive = TimeSpan.FromDays(7);
        public static readonly TimeSpan DefaultVisabilityTimeout = TimeSpan.FromSeconds(30);

        public static readonly Func<Type, XmlObjectSerializer> DefaultSerializer =
            t => new DataContractSerializer(t);

        protected internal QueueMessage()
        {
            TimeToLive = MaxTimeToLive;
            IsEnqueued = false;
        }

        public QueueMessage(object serializableObject)
            : this()
        { 
            if (serializableObject == null)
                throw new ArgumentNullException("serializableObject");

            var serializer = DefaultSerializer(serializableObject.GetType());
            using (var output = new StringWriter())
            using(var writer = new XmlTextWriter(output){Formatting = Formatting.Indented})
            {
                serializer.WriteObject(writer, serializableObject);
                writer.Flush();
                ContentInternal = Encoding.UTF8.GetBytes(output.GetStringBuilder().ToString());
            }
        }

        public QueueMessage(object serializableObject, XmlObjectSerializer serializer)
            : this()
        {
            if (serializableObject == null)
                throw new ArgumentNullException("serializableObject");
            if (serializer == null)
                throw new ArgumentNullException("serializer");

            using (var output = new StringWriter())
            using (var writer = new XmlTextWriter(output) { Formatting = Formatting.Indented })
            {
                serializer.WriteObject(writer, serializableObject);
                writer.Flush();
                ContentInternal = Encoding.UTF8.GetBytes(output.GetStringBuilder().ToString());
            }
        }

        public int DeliveryCount
        {
            get
            {
                ThrowIfNotEnqueued();
                return DeliveryCountIntenal;
            }
        }

        public DateTime ExpiresAtUtc
        {
            get
            {
                ThrowIfNotEnqueued();
                return ExpiresAtUtcInternal;
            }
        }

        public DateTime EnqueuedTimeUtc
        {
            get
            {
                ThrowIfNotEnqueued();
                return EnqueuedTimeUtcInternal;
            }
        }

        public DateTime LockedUntilUtc
        {
            get
            {
                ThrowIfNotEnqueued();
                return LockedUntilUtcInternal;
            }
        }

        public string MessageId
        {
            get
            {
                ThrowIfNotEnqueued();
                return MessageIdInternal;
            }
        }

        public TimeSpan TimeToLive
        {
            get
            {
                if (IsEnqueued)
                    return ExpiresAtUtc.Subtract(EnqueuedTimeUtc);
                return TimeToLiveInternal;
            }
            set
            {
                ThrowIfEnqueued();
                if (value.TotalMilliseconds <= 0 || value > MaxTimeToLive)
                    throw new ArgumentOutOfRangeException("TimeToLive");
                TimeToLiveInternal = value;
            }
        }

        public T GetBody<T>()
        {
            return GetBody<T>(DefaultSerializer(typeof(T)));
        }

        public T GetBody<T>(XmlObjectSerializer serializer)
        {
            using (var stream = new MemoryStream(ContentInternal, false))
            {
                return (T)serializer.ReadObject(stream);
            }
        }

        protected internal byte[] ContentInternal { get; set; }
        protected internal int DeliveryCountIntenal { get; set; }
        protected internal DateTime ExpiresAtUtcInternal { get; set; }
        protected internal DateTime EnqueuedTimeUtcInternal { get; set; }
        protected internal DateTime LockedUntilUtcInternal { get; set; }
        protected internal string MessageIdInternal { get; set; }
        protected internal TimeSpan TimeToLiveInternal { get; set; }
        protected internal bool IsEnqueued { get; set; }

        private void ThrowIfNotEnqueued()
        {
            if (!IsEnqueued)
                throw new InvalidOperationException();
        }

        private void ThrowIfEnqueued()
        {
            if (IsEnqueued)
                throw new InvalidOperationException();
        }
    }
}