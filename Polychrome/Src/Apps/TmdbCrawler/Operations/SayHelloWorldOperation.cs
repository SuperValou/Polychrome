using System;
using System.Threading.Tasks;
using TaskSystem.Progresses;
using TaskSystem.TaskObjects;

namespace TmdbCrawler.Operations
{
    public class SayHelloWorldOperation : AbstractTask
    {
        public override Task Execute(IProgressReporter reporter)
        {
            
            // 1- get "hello" from configuration
            // 2- get user input to confirm continuation
            // 3- get "world" from an async method

            throw new NotImplementedException();
        }
    }
}
