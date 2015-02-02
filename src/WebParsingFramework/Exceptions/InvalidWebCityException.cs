using System;
using System.Runtime.Serialization;

namespace WebParsingFramework.Exceptions
{
    [Serializable]
    public class InvalidWebCityException : Exception
    {
        public InvalidWebCityException(string expected, string actual)
        {
            Expected = expected;
            Actual = actual;
        }

        public InvalidWebCityException(string message, string expected, string actual)
            : base(message)
        {
            Expected = expected;
            Actual = actual;
        }

        public string Expected { get; private set; }

        public string Actual { get; private set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Expected", Expected);
            info.AddValue("Actual", Actual);
        }
    }
}