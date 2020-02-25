using System;

namespace ApplicationCore
{
    public interface IApp : IDisposable
    {
        string AppName { get; }
        string AppVersion { get; }

        bool IsInitialized { get; }
        bool IsRunning { get; }
    }
}