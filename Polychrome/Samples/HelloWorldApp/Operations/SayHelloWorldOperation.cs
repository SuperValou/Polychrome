using ApplicationCore.Operations;
using Kernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldApp.Operations
{
    public class SayHelloWorldOperation : AbstractOperation
    {
        public SayHelloWorldOperation(ILogger logger) : base(logger)
        {

        }

        public override async Task Execute()
        {
            // 1- get "hello" from configuration
            // 2- get user input to confirm continuation
            // 3- get "world" from an async method

            throw new NotImplementedException();
        }
    }
}
