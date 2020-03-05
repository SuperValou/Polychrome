using System;

namespace Kernel
{
    public interface IService : IDisposable
    {
        bool IsInitialized { get; }        
    }
}
