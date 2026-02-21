using System;
using Tavera_ScenarioC.Interfaces;

namespace Tavera_ScenarioC.AbstractClasses
{
    /// <summary>
    /// ABSTRACT CLASS: Hindi ito pwedeng gawing object directly.
    /// Ito ang blueprint o template para sa lahat ng power sources (solar at wind)
    /// Ipinapakita ang ABSTRACTION (pagtatago ng complex details) at INHERITANCE (mana)
    /// </summary>
    public abstract class PowerSource : IPowerSource
    {
        // PRIVATE FIELDS - ENCAPSULATION: Nakatagong data
        // Ito ay parang mga sikretong impormasyon na hindi basta-basta makikita ng iba
        private string _sourceID;      // ID ng power source (nakatago)
        private double _baseOutput;     // Base output sa kW (nakatago)

        // PUBLIC PROPERTIES with validation - ENCAPSULATION
        // Ito ang pinto para ma-access ang mga private fields
        // May check para masiguradong tama ang data
        public string SourceID
        {
            get { return _sourceID; } // Kunin ang ID
            protected set // Pwedeng i-access ng anak (solar/wind) pero hindi ng iba
            {
                // Siguraduhing hindi blanko ang ID
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Hindi pwedeng walang laman ang Source ID!");
                _sourceID = value; // Pag okay, i-save na
            }
        }

        public double BaseOutput
        {
            get { return _baseOutput; } // Kunin ang base output
            protected set // Pwedeng i-access ng anak pero hindi ng iba
            {
                // Siguraduhing positive number ang base output
                if (value <= 0)
                    throw new ArgumentException("Ang Base Output ay dapat positive number!");
                _baseOutput = value; // ✅ TAMA: Sine-save sa _baseOutput (double variable)
            }
        }

        // CONSTRUCTOR: Taga-gawa ng bagong PowerSource
        // Sinisigurado na valid ang object pagkalik pa lang
        protected PowerSource(string sourceId, double baseOutput)
        {
            SourceID = sourceId;      // I-set ang ID (dadaan sa property, may check)
            BaseOutput = baseOutput;  // I-set ang base output (dadaan sa property, may check)
        }

        // ABSTRACT METHOD: Walang laman ito dito
        // Ang mga anak (solar/wind) ang magbibigay ng sarili nilang version
        // Parang resipe na walang instruction - yung anak ang magsasabi kung paano lutuin
        public abstract double CalculateCurrentOutput();

        // VIRTUAL METHOD: May sariling version pero pwedeng palitan ng anak
        // Ipinapakita ang POLYMORPHISM (iba't ibang porma)
        public virtual string GenerateReport(string reportType)
        {
            // Default report - simple lang
            return $"Power Source Report\n" +
                   $"ID: {SourceID}\n" +
                   $"Base Output: {BaseOutput} kW\n" +
                   $"Kuryente Ngayon: {CalculateCurrentOutput():F2} kW";
        }

        // OVERLOADED METHOD: Pareho ang pangalan, iba ang parameters
        // Isa pang klase ng POLYMORPHISM (compile-time)
        public string GenerateReport()
        {
            // Pag walang sinabing report type, summary ang default
            return GenerateReport("summary");
        }
    }
}