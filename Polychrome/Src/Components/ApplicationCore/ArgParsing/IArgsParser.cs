using System.Collections.Generic;

namespace ApplicationCore.ArgParsing
{
    public interface IArgsParser
    {
        AppArgs ParsedArgs { get; }

        bool HasError { get; }
        string ErrorMessage { get; }

        void Parse(ICollection<string> args);
    }
}