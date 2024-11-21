using ACW1.Core.System;

namespace ACW1.Core.CLI.SimpleCommand;

public interface ISimpleCommand<in TIn, out TReturn>
{
    public string CommandName { get; }

    // todo pass services here?
    public TReturn? Run(TIn arg);
}
