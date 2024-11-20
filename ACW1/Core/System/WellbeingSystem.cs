using ACW1.Core.CLI.CommandReader;

namespace ACW1.Core.System;

public sealed class WellbeingSystem(ICommandReader commandReader)
{
    private bool _isInitialized = false;

    // todo
    private List<object> _users = [];

    public void Initialize()
    {
        _isInitialized = true;
    }

    public void Run(bool dryRun = false)
    {
        if (!_isInitialized) 
            throw new InvalidOperationException("System is not initialized.");
        
        if (dryRun) return;
        
        
    }
}
