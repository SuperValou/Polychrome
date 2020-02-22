using System;

namespace Kernel.Exceptions
{
    public class AlreadyInitializedException : InvalidOperationException
    {
        public AlreadyInitializedException(string alreadyInitializedObjectName)
        : base($"'{alreadyInitializedObjectName}' is already initialized.")
        {

        }
    }
}