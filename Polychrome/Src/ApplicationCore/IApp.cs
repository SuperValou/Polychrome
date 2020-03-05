using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore
{
    public interface IApp : IDisposable
    {
        string AppName { get; }
        string AppVersion { get; }

        bool IsInitialized { get; }
        bool IsRunning { get; }

        Task Initialize(ICollection<string> args);

        Task<int> Run();
    }
}