using System;
using System.Threading.Tasks;
using ApplicationCore.Tasks;
using Kernel;
using MediaDatabase.Service;
using SwapFusion.Configurations;

namespace SwapFusion.Tasks
{
    public class GenerateSwapsTask : WorkingDirectoryTask
    {
        private readonly GenerateSwapsTaskSetup _setup;
        private readonly IMediaDatabaseService _mediaDatabaseService;

        public GenerateSwapsTask(ILogger logger, GenerateSwapsTaskSetup setup, IMediaDatabaseService mediaDatabaseService)
            : base(logger)
        {
            _setup = setup ?? throw new ArgumentNullException(nameof(setup));
            _mediaDatabaseService = mediaDatabaseService ?? throw new ArgumentNullException(nameof(mediaDatabaseService));
        }

        public override Task Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}