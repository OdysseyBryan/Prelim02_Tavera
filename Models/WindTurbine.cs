using System;
using Tavera_ScenarioC.AbstractClasses;

namespace Tavera_ScenarioC.Models
{
    /// <summary>
    /// WIND TURBINE: Derived from PowerSource
    /// Demonstrates INHERITANCE and POLYMORPHISM
    /// </summary>
    public class WindTurbine : PowerSource
    {
        // Private field for encapsulation
        private double _windSpeed;

        // Property with validation
        public double WindSpeed
        {
            get { return _windSpeed; }
            set
            {
                // ROBUSTNESS: Validate input
                if (value < 0)
                    throw new ArgumentException("Wind speed cannot be negative!");

                _windSpeed = value;
            }
        }

        // Constructor with base() call
        public WindTurbine(string sourceId, double baseOutput, double windSpeed)
            : base(sourceId, baseOutput)
        {
            WindSpeed = windSpeed;
        }

        // OVERRIDE abstract method
        public override double CalculateCurrentOutput()
        {
            // Wind turbines have a minimum and maximum wind speed for operation
            if (WindSpeed < 3) // Too low
                return 0;
            if (WindSpeed > 25) // Too high (safety shutdown)
                return 0;

            // Output increases with wind speed (simplified calculation)
            return BaseOutput * (WindSpeed / 12.0); // 12 m/s is optimal
        }

        // OVERRIDE virtual method
        public override string GenerateReport(string reportType)
        {
            if (reportType.ToLower() == "detailed")
            {
                string status = WindSpeed < 3 ? "[OFFLINE] Wind too low" :
                               WindSpeed > 25 ? "❌ Wind too high (safety)" :
                               "✅ Operating";

                return $"WIND TURBINE DETAILED REPORT\n" +
                       $"═══════════════════════════════════\n" +
                       $"ID: {SourceID}\n" +
                       $"Type: Horizontal Axis Wind Turbine\n" +
                       $"Base Capacity: {BaseOutput} kW\n" +
                       $"Current Wind Speed: {WindSpeed} m/s\n" +
                       $"Current Output: {CalculateCurrentOutput():F2} kW\n" +
                       $"Status: {status}\n" +
                       $"Optimal Range: 3 - 25 m/s";
            }
            else // Summary report
            {
                return base.GenerateReport(reportType) + $" | Wind: {WindSpeed} m/s";
            }
        }
    }
}