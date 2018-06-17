using System;

namespace MusicInside.Managers.Exceptions
{
    [Serializable]
    internal class EntryNotPresentException : Exception
    {
        public EntryNotPresentException() : base("Can't found an album with chosen id") { }

        public EntryNotPresentException(int id) : base(string.Format("Can't found an album with id={0}", id)) { }

        public EntryNotPresentException(int id, Exception innerException) : base(string.Format("Can't found an album with id={0}", id), innerException) { }
    }
}