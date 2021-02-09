using System;
using System.Runtime.Serialization;

[Serializable]
internal class MissingLocalizationException : Exception
{
    public MissingLocalizationException()
    {
    }

    public MissingLocalizationException(string message) : base(message)
    {
    }

    public MissingLocalizationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected MissingLocalizationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}