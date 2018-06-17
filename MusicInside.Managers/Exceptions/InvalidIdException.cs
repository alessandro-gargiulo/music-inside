using System;

namespace MusicInside.Managers.Exceptions
{
    [Serializable]
    public class InvalidIdException : Exception
    {
        public InvalidIdException() : base("Invalid album id value. Value must be non-negative") { }

        public InvalidIdException(int id) : base(string.Format("{0} is not a valid id album value. Value must be non-negative", id)) { }

        public InvalidIdException(int id, Exception innerException) : base(string.Format("{0} is not a valid id album value. Value must be non-negative", id), innerException) { }
    }
}