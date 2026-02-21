using System;

namespace Tavera_ScenarioC.Interfaces
{
    /// <summary>
    /// INTERFACE: Ito ay parang kontrata o kasunduan.
    /// Lahat ng gagamit nito (tulad ng SolarPanel at WindTurbine) ay dapat sumunod.
    /// Ipinapakita ang ABSTRACTION - tinatago ang complex details, ipinapakita lang ang kailangan.
    /// </summary>
    public interface IPowerSource
    {
        // PROPERTIES - Lahat ng power source ay dapat may mga ito
        // Parang requirements: dapat may ID at BaseOutput ang bawat power source

        /// <summary>
        /// ID ng power source (halimbawa: "SOL-001" o "WIND-001")
        /// </summary>
        string SourceID { get; }

        /// <summary>
        /// Base output sa kilowatts (kW) - maximum na kaya ng power source
        /// </summary>
        double BaseOutput { get; }

        // METHODS - Lahat ng power source ay dapat marunong gumawa ng mga ito
        // Parang mga action na dapat kayanin ng bawat power source

        /// <summary>
        /// Mag-compute kung gaano karaming kuryente ang ginagawa ngayon
        /// Depende ito sa uri ng power source (solar = base sa araw, wind = base sa hangin)
        /// </summary>
        double CalculateCurrentOutput();

        /// <summary>
        /// Gumawa ng report tungkol sa power source
        /// Pwedeng summary (maikli) o detailed (mahaba)
        /// </summary>
        /// <param name="reportType">"summary" o "detailed"</param>
        string GenerateReport(string reportType);
    }
}