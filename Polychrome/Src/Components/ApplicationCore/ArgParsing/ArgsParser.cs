using System;
using System.Collections.Generic;
using System.Linq;
using LightLogs;

namespace ApplicationCore.ArgParsing
{
    public class ArgsParser : IArgsParser
    {
        private const string Config = "-config";
        private const string Logs = "-logs";
        private const string DisabledLogs = "disabled";

        public AppArgs ParsedArgs { get; private set; }

        public bool HasError { get; private set; }

        public string ErrorMessage { get; private set; }

        // <nothing>
        // -config "c:hre"
        // -log disabled
        //  -config "c:/here" -log info
        public void Parse(ICollection<string> args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            
            var argsStack = new Stack<string>(args.Reverse());
            
            ResetState();
            ParseConfig(argsStack);
            ParseLogs(argsStack);
            EnsureEmpty(argsStack);
        }

        private void EnsureEmpty(Stack<string> argsStack)
        {
            if (HasError)
            {
                return;
            }

            if (argsStack.Count == 0)
            {
                return;
            }

            HasError = true;
            string unknownArg = argsStack.Pop();
            ErrorMessage = $"Unknown arg '{unknownArg}'.";
        }

        private void ParseConfig(Stack<string> argsStack)
        {
            if (HasError)
            {
                return;
            }

            if (!argsStack.TryPop(out string firstArg))
            {
                // empty stack
                return;
            }

            if (firstArg != Config)
            {
                // not the 'config' option, but can be something else
                argsStack.Push(firstArg);
                return;
            }

            if (!argsStack.TryPop(out string secondArg) || string.IsNullOrEmpty(secondArg))
            {
                // config file path is missing
                HasError = true;
                ErrorMessage = $"File path is missing after '{Config}'.";
                return;
            }

            ParsedArgs.ConfigPath = secondArg;
        }

        private void ParseLogs(Stack<string> argsStack)
        {
            if (HasError)
            {
                return;
            }

            if (!argsStack.TryPop(out string firstArg))
            {
                // empty stack
                return;
            }

            if (firstArg != Logs)
            {
                // not the 'logs' option, but can be something else
                argsStack.Push(firstArg);
                return;
            }

            if (!argsStack.TryPop(out string secondArg) || string.IsNullOrEmpty(secondArg))
            {
                // log mode is missing
                HasError = true;
                ErrorMessage = $"Log mode is missing after '{Logs}'.";
                return;
            }

            if (Enum.TryParse(typeof(LogLevel), secondArg, ignoreCase: true, out object minLogLevel))
            {
                ParsedArgs.MinLogLevel = (LogLevel) minLogLevel;
            }
            else if (secondArg == DisabledLogs)
            {
                ParsedArgs.DisableLogs = true;
            }
            else
            {
                HasError = true;
                ErrorMessage = $"Log mode '{secondArg}' is not handled. Try {string.Join(", ", Enum.GetNames(typeof(LogLevel)))} or {DisabledLogs}.";
            }            
        }

        private void ResetState()
        {
            HasError = false;
            ErrorMessage = string.Empty;
            ParsedArgs = new AppArgs();
        }
    }
}