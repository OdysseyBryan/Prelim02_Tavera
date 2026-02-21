using System;
using Tavera_ScenarioC.AbstractClasses;

namespace Tavera_ScenarioC.Models
{
    /// <summary>
    /// SOLAR PANEL: Derived from PowerSource
    /// Demonstrates INHERITANCE and POLYMORPHISM (method overriding)
    /// </summary>
    public class SolarPanel : PowerSource
    {
        // Private field for encapsulation
        private double _sunlightPercentage;

        // Property with validation
        public double SunlightPercentage
        {
            get { return _sunlightPercentage; }
            set
            {
                // ROBUSTNESS: Validate input
                if (value < 0)
                    throw new ArgumentException("Sunlight percentage cannot be negative!");
                if (value > 100)
                    throw new ArgumentException("Sunlight percentage cannot exceed 100%!");

                _sunlightPercentage = value;
            }
        }

        // Constructor with base() call - INHERITANCE
        public SolarPanel(string sourceId, double baseOutput, double sunlightPercentage)
            : base(sourceId, baseOutput)
        {
            SunlightPercentage = sunlightPercentage;
        }

        // OVERRIDE abstract method - POLYMORPHISM (runtime)
        public override double CalculateCurrentOutput()
        {
            // Solar panels produce based on sunlight percentage
            return BaseOutput * (SunlightPercentage / 100.0);
        }

        // OVERRIDE virtual method - POLYMORPHISM
        public override string GenerateReport(string reportType)
        {
            if (reportType.ToLower() == "detailed")
            {
                return $"SOLAR PANEL DETAILED REPORT\n" +
                       $"═══════════════════════════════════\n" +
                       $"ID: {SourceID}\n" +
                       $"Type: Photovoltaic Solar Panel\n" +
                       $"Base Capacity: {BaseOutput} kW\n" +
                       $"Current Sunlight: {SunlightPercentage}%\n" +
                       $"Current Output: {CalculateCurrentOutput():F2} kW\n" +
                       $"Efficiency: {(CalculateCurrentOutput() / BaseOutput * 100):F1}%\n" +
                       $"Status: {(SunlightPercentage > 10 ? "[ACTIVE]" : "[LOW LIGHT]")}";
            }
            else // Summary report
            {
                return base.GenerateReport(reportType) + $" | Sun: {SunlightPercentage}%";
            }
        }
    }
}