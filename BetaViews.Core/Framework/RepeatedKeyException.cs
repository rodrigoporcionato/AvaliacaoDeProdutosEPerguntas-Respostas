using System;

namespace BetaViews.Core.Framework
{
    /// <summary>
    /// Exception thrown when an entity cannot be inserted or updated because a unique key has been violated.
    /// </summary>
    public class RepeatedKeyException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatedKeyException"/> with a
        /// reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public RepeatedKeyException(Exception innerException)
            : base("There is already an object with the same key in the database.", innerException)
        {
        }
    }
}
