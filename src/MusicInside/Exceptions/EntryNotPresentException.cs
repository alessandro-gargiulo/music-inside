using System;

namespace MusicInside.Exceptions
{
    [Serializable]
    internal class EntryNotPresentException : Exception
    {
        public EntryNotPresentException()
        {
        }

        public EntryNotPresentException(string message) : base(message)
        {
        }

        public EntryNotPresentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}