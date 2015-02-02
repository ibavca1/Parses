using System;

namespace WebParsingFramework.Exceptions
{
    [Serializable]
    public class UnsupporedWebCityException : Exception
    {
        public UnsupporedWebCityException(string message)
            : base(message)
        {
        }

        public UnsupporedWebCityException(WebCompany company, WebCity city)
            
        {
        }
    }
}