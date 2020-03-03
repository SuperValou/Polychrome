﻿using Kernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskSystem;

namespace HelloWorldApp.Operations
{
    public class SayHelloWorldOperation : AbstractTask
    {
        public SayHelloWorldOperation(ILogger logger) : base(logger)
        {

        }

        public override Task Execute(IProgressReporter reporter)
        {
            
            // 1- get "hello" from configuration
            // 2- get user input to confirm continuation
            // 3- get "world" from an async method

            throw new NotImplementedException();
        }
    }
}
