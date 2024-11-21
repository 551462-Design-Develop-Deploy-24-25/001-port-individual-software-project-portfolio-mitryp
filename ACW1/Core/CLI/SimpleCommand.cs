using ACW1.Core.CLI.SimpleCommand;
using ACW1.Core.System;

namespace ACW1.Core.CLI;

public abstract class SimpleCommand<TIn, TReturn>(string name) : ISimpleCommand<TIn, TReturn>
{
    public string CommandName { get; } = name;

    public abstract TReturn? Run(TIn arg);
}
