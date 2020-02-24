using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Exceptions;
using LightLogs.API;

namespace LightLogs.LogsManagement
{
    internal class LogFlusher : ILogFlusher, IDisposable
    {
        private readonly LogLevel _minLogLevel;

        private readonly object _lock = new object();
        private readonly ICollection<LogEvent> _logEvents = new List<LogEvent>();
        private readonly ICollection<ITarget> _targets = new List<ITarget>();

        private Task _flushTask;

        private volatile bool _disabled = false;

        internal LogFlusher(IEnumerable<ITarget> targets, LogLevel minLogLevel)
        {
            if (targets == null)
            {
                throw new ArgumentNullException(nameof(targets));
            }

            if (!Enum.IsDefined(typeof(LogLevel), minLogLevel))
            {
                throw new InvalidEnumArgumentException(nameof(minLogLevel), (int) minLogLevel, typeof(LogLevel));
            }

            _minLogLevel = minLogLevel;

            foreach (ITarget target in targets)
            {
                if (target == null)
                {
                    throw new ArgumentException("One of the target was null.", nameof(targets));
                }

                _targets.Add(target);
            }
        }

        public void Initialize()
        {
            if (_flushTask != null)
            {
                throw new AlreadyInitializedException(nameof(LogFlusher));
            }

            _flushTask = Task.Factory.StartNew(FlushRoutine, TaskCreationOptions.LongRunning);
            _flushTask.ConfigureAwait(false);
        }

        public virtual void Dispose()
        {
            _disabled = true;
            _flushTask.Wait();

            _flushTask.Dispose();

            foreach (ITarget target in _targets)
            {
                target.Dispose();
            }
        }

        public void AddLogEvent(LogEvent logEvent)
        {
            if (_disabled)
            {
                return;
            }

            if (logEvent.Level < _minLogLevel)
            {
                return;
            }

            lock (_lock)
            {
                _logEvents.Add(logEvent);
            }
        }

        private async Task FlushRoutine()
        {
            while (!_disabled)
            {
                await FlushToTargets();
            }

            await FlushToTargets();
        }

        private async Task FlushToTargets()
        {
            ICollection<LogEvent> logEvents;
            lock (_lock)
            {
                logEvents = new List<LogEvent>(_logEvents);
                _logEvents.Clear();
            }

            logEvents = logEvents.OrderBy(l => l.Timestamp).ToList();

            foreach (LogEvent logEvent in logEvents)
            {
                char[] log = GetLogChars(logEvent);
                foreach (ITarget target in _targets)
                {
#if DEBUG
                    await target.Write(logEvent.Level, log);
#else
                    try
                    {
                        await target.Write(logEvent.Level, log);
                    }
                    catch (Exception)
                    {
                        // suppress target errors
                    }
#endif
                }
            }
        }

        private static char[] GetLogChars(LogEvent logEvent)
        {
            StringBuilder builder = new StringBuilder();

            DateTime timestamp = logEvent.Timestamp;
            builder.Append(timestamp.Year);
            builder.Append('-');
            builder.Append(timestamp.Month.ToString("00"));
            builder.Append('-');
            builder.Append(timestamp.Day.ToString("00"));
            builder.Append(' ');
            builder.Append(timestamp.Hour.ToString("00"));
            builder.Append(':');
            builder.Append(timestamp.Minute.ToString("00"));
            builder.Append(':');
            builder.Append(timestamp.Second.ToString("00"));
            builder.Append(':');
            builder.Append(timestamp.Millisecond.ToString("000"));

            builder.Append(' ');

            builder.Append('[');
            builder.Append(logEvent.Level);
            builder.Append(']');

            builder.Append('\t');

            builder.Append('[');
            builder.Append(logEvent.Owner ?? string.Empty);
            builder.Append(']');

            builder.Append(' ');

            builder.Append(logEvent.Message ?? string.Empty);

            builder.AppendLine();

            if (logEvent.Exception != null)
            {
                builder.Append(logEvent.Exception.ToString());
                builder.AppendLine();
            }
            
            char[] output = new char[builder.Length];
            builder.CopyTo(0, output, builder.Length);

            return output;
        }
    }
}