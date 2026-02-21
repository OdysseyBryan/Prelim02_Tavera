using System;
using Tavera_ScenarioC.Interfaces;

namespace Tavera_ScenarioC.AbstractClasses
{
    /// <summary>
    /// ABSTRACT CLASS: Cannot be instantiated directly, serves as blueprint for all power sources
    /// Demonstrates ABSTRACTION and INHERITANCE
    /// </summary>
    public abstract class PowerSource : IPowerSource
    {
        // PRIVATE FIELDS - ENCAPSULATION: Data hiding
        private string _sourceID;
        private double _baseOutput;

        // PUBLIC PROPERTIES with validation - ENCAPSULATION
        public string SourceID
        {
            get { return _sourceID; }
            protected set // Only accessible within class and derived classes
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Source ID cannot be empty!");
                _sourceID = value;
            }
        }

        public double BaseOutput
        {
            get { return _baseOutput; }
            protected set
            {
                if (value <= 0)
                    throw new ArgumentException("Base output must be positive!");
                _baseOutput = value;
            }
        }

        // CONSTRUCTOR: Ensures valid objects from the start
        protected PowerSource(string sourceId, double baseOutput)
        {
            SourceID = sourceId;
            BaseOutput = baseOutput;
        }

        // ABSTRACT METHOD: Must be implemented by derived classes
        public abstract double CalculateCurrentOutput();

        // VIRTUAL METHOD: Can be overridden, but has base implementation
        // Demonstrates POLYMORPHISM
        public virtual string GenerateReport(string reportType)
        {
            return $"Power Source Report\n" +
                   $"ID: {SourceID}\n" +
                   $"Base Output: {BaseOutput} kW\n" +
                   $"Current Output: {CalculateCurrentOutput():F2} kW";
        }

        // OVERLOADED METHOD: Same name, different parameters
        // Demonstrates POLYMORPHISM (compile-time)
        public string GenerateReport()
        {
            return GenerateReport("summary"); // Default to summary
        }
    }
}