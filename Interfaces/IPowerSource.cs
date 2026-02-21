using System;

namespace Tavera_ScenarioC.Interfaces
{
    /// <summary>
    /// INTERFACE: Defines the contract that all power sources must follow
    /// This demonstrates ABSTRACTION - hiding complex details behind a simple contract
    /// </summary>
    public interface IPowerSource
    {
        // Properties that all power sources must have
        string SourceID { get; }
        double BaseOutput { get; }

        // Methods that all power sources must implement
        double CalculateCurrentOutput();
        string GenerateReport(string reportType);
    }
}