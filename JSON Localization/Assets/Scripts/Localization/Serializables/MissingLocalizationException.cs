using System;
using System.Runtime.Serialization;

/// <summary>
/// Exception templates for when another class unsucessfully attempts to retrieve language data
/// </summary>
[Serializable]
internal class MissingLocalizationException : Exception
{
    /// <summary>
    /// No Arg Exception
    /// </summary>
    public MissingLocalizationException()
    {
    }

    /// <summary>
    /// Exception with message
    /// </summary>
    /// <param name="message"></param>
    public MissingLocalizationException(string message) : base(message)
    {
    }

    /// <summary>
    /// Exception with Inner Exception
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public MissingLocalizationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Exception with Streaming Context
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected MissingLocalizationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}