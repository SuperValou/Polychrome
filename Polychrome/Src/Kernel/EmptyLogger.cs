using System;

namespace Kernel
{
    /// <summary>
    /// An empty logger doing nothing. Use it to disable logs, or for tests.
    /// </summary>
    public class EmptyLogger : ILogger
    {
        public virtual ILogger CreateSubLogger(string subLoggerName)
        {
            return this;
        }

        public virtual void Debug(string message)
        {
            // do nothing
        }

        public virtual void Error(string message, Exception exception)
        {
            // do nothing
        }

        public virtual void Error(string message)
        {
            // do nothing
        }

        public virtual void Fatal(string message)
        {
            // do nothing
        }

        public virtual void Fatal(string message, Exception exception)
        {
            // do nothing
        }

        public virtual void Info(string message)
        {
            // do nothing
        }

        public virtual void Trace(string message)
        {
            // do nothing
        }

        public virtual void Warn(string message)
        {
            // do nothing
        }

        public virtual void Warn(string message, Exception exception)
        {
            // do nothing
        }
    }
}
