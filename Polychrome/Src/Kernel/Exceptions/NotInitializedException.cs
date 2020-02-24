using System;

namespace Kernel.Exceptions
{
    public class NotInitializedException : InvalidOperationException
    {
        public NotInitializedException(string notInitializedObjectName)
            : base($"'{notInitializedObjectName}' is not initialized. Did you forget to call the Initialize method?")
        {

        }

        public NotInitializedException(string notInitializedObjectName, string message)
            : base($"'{notInitializedObjectName}' is not initialized. Did you forget to call the Initialize method? {message}")
        {

        }
    }
}