using System;
using Tavera_ScenarioC.AbstractClasses;

namespace Tavera_ScenarioC.Models
{
    /// <summary>
    /// SOLAR PANEL: Galing ito sa PowerSource (parang anak)
    /// Ipinapakita ang INHERITANCE (mana) at POLYMORPHISM (iba't ibang porma)
    /// </summary>
    public class SolarPanel : PowerSource
    {
        // Private field - nakatago ito, hindi direktang maa-access ng ibang class
        // Para protektado ang data (ENCAPSULATION)
        private double _sunlightPercentage;

        // Property - ito ang daan para ma-access ang _sunlightPercentage
        // May validation para masiguradong tama ang input
        public double SunlightPercentage
        {
            get { return _sunlightPercentage; } // Kunin ang value
            set
            {
                // ROBUSTNESS: Siguraduhing tama ang input ng user
                // Pag negative ang nilagay, bawal
                if (value < 0)
                    throw new ArgumentException("Hindi pwedeng negative ang sunlight percentage!");

                // Pag lampas naman ng 100%, bawal din
                if (value > 100)
                    throw new ArgumentException("Hindi pwedeng lampas 100% ang sunlight!");

                // Pag okay ang input, i-save na sa _sunlightPercentage
                _sunlightPercentage = value;
            }
        }

        // Constructor - gumagawa ng bagong SolarPanel
        // : base() ibig sabihin tatawagin din ang constructor ng PowerSource (magulang)
        // Ito ang INHERITANCE - ginagamit ang constructor ng magulang
        public SolarPanel(string sourceId, double baseOutput, double sunlightPercentage)
            : base(sourceId, baseOutput)
        {
            SunlightPercentage = sunlightPercentage; // I-set ang sunlight percentage
        }

        // OVERRIDE abstract method - ginagawa natin ang abstract method ng magulang
        // Dito natin sinasabi kung paano mag-compute ng output ng solar panel
        // Ito ay POLYMORPHISM - pareho ang pangalan, iba ang ginagawa (kumpara sa WindTurbine)
        public override double CalculateCurrentOutput()
        {
            // Ang solar panel ay gumagawa ng kuryente base sa dami ng sikat ng araw
            // Formula: BaseOutput * (sunlight percentage / 100)
            // Halimbawa: 100kW * (75% / 100) = 75kW na kuryente
            return BaseOutput * (SunlightPercentage / 100.0);
        }

        // OVERRIDE virtual method - binabago natin ang GenerateReport ng magulang
        // Para mas detalyado ang report ng solar panel
        // Ito rin ay POLYMORPHISM
        public override string GenerateReport(string reportType)
        {
            // Kung "detailed" ang gusto ng user
            if (reportType.ToLower() == "detailed")
            {
                // COMPUTE EFFICIENCY: Kung gaano kahusay ang solar panel ngayon
                double efficiency = (CalculateCurrentOutput() / BaseOutput * 100);

                // Alamin ang status base sa sunlight
                string status;
                if (SunlightPercentage > 10)
                    status = "[ACTIVE] Normal na operasyon";
                else
                    status = "[LOW LIGHT] Kaunti ang sikat ng araw";

                // Detailed report - maraming impormasyon para sa user
                return $"SOLAR PANEL DETAILED REPORT\n" +
                       $"═══════════════════════════════════\n" +
                       $"ID: {SourceID}\n" +
                       $"Uri: Photovoltaic Solar Panel\n" +
                       $"Base Capacity: {BaseOutput} kW\n" +
                       $"Sikat ng Araw Ngayon: {SunlightPercentage}%\n" +
                       $"Kuryente Ngayon: {CalculateCurrentOutput():F2} kW\n" +
                       $"Kahusayan: {efficiency:F1}%\n" +
                       $"Status: {status}";
            }
            else // Kung summary report lang ang gusto
            {
                // Kunin ang base report galing sa magulang, tapos dagdagan ng info tungkol sa araw
                return base.GenerateReport(reportType) + $" | Araw: {SunlightPercentage}%";
            }
        }
    }
}