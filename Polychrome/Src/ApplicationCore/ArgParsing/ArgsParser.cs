using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.ArgParsing
{
    public static class ArgsParser
    {
        // <nothing>
        // -config "c:hre"
        // -log muted
        //  -config "c:/here" -log verbose
        public static AppArgs Parse(ICollection<string> args)
        {
            switch (args.Count)
            {
                case 0:
                    break;
            }

            throw new NotImplementedException();
        }
    }

    public class AppArgs
    {
        public string ArgString { get; }
        public object MinLogLevel { get; }
    }
}
