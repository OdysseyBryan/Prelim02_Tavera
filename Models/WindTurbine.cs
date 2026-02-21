using System;
using Tavera_ScenarioC.AbstractClasses;

namespace Tavera_ScenarioC.Models
{
    /// <summary>
    /// WIND TURBINE: Galing ito sa PowerSource (parang anak)
    /// Ipinapakita ang INHERITANCE (mana) at POLYMORPHISM (iba't ibang porma)
    /// </summary>
    public class WindTurbine : PowerSource
    {
        // Private field - ito ay nakatago, hindi basta-basta makukuha ng ibang class
        // Para protektado ang data (ENCAPSULATION)
        private double _windSpeed;

        // Property - ito ang pinto para ma-access ang _windSpeed
        // May validation para hindi magkamali ang user
        public double WindSpeed
        {
            get { return _windSpeed; } // Kunin ang value
            set
            {
                // ROBUSTNESS: Siguraduhing tama ang input
                // Pag negative ang nilagay, may error
                if (value < 0)
                    throw new ArgumentException("Hindi pwedeng negative ang wind speed!");

                _windSpeed = value; // Pag okay, i-save ang value
            }
        }

        // Constructor - ito ang gumagawa ng bagong WindTurbine
        // : base() means tatawagin din ang constructor ng PowerSource (magulang)
        public WindTurbine(string sourceId, double baseOutput, double windSpeed)
            : base(sourceId, baseOutput)
        {
            WindSpeed = windSpeed; // I-set ang wind speed
        }

        // OVERRIDE abstract method - ginagawa natin ang abstract method ng magulang
        // Dito natin sinasabi kung paano mag-compute ng output ng wind turbine
        public override double CalculateCurrentOutput()
        {
            // May minimum at maximum na hangin para gumana ang wind turbine
            if (WindSpeed < 3) // Masyadong mahina ang hangin
                return 0; // Walang kuryente
            if (WindSpeed > 25) // Masyadong malakas (delikado, automatic shutdown)
                return 0; // Walang kuryente para safe

            // Pag nasa tamang range, mag-compute ng output
            // 12 m/s ang ideal na bilis ng hangin
            return BaseOutput * (WindSpeed / 12.0); // Simplified computation lang ito
        }

        // OVERRIDE virtual method - binabago natin ang GenerateReport ng magulang
        // Para mas detalyado ang report ng wind turbine
        public override string GenerateReport(string reportType)
        {
            // Kung "detailed" ang gusto ng user
            if (reportType.ToLower() == "detailed")
            {
                // Alamin muna kung anong status ng wind turbine
                string status;
                if (WindSpeed < 3)
                    status = "[OFFLINE] Mahina ang hangin";
                else if (WindSpeed > 25)
                    status = "[OFFLINE] Masyadong malakas ang hangin (safety)";
                else
                    status = "[ACTIVE] Normal na operasyon";

                // Detailed report - maraming impormasyon
                return $"WIND TURBINE DETAILED REPORT\n" +
                       $"═══════════════════════════════════\n" +
                       $"ID: {SourceID}\n" +
                       $"Uri: Horizontal Axis Wind Turbine\n" +
                       $"Base Capacity: {BaseOutput} kW\n" +
                       $"Bilis ng Hangin Ngayon: {WindSpeed} m/s\n" +
                       $"Kuryente Ngayon: {CalculateCurrentOutput():F2} kW\n" +
                       $"Status: {status}\n" +
                       $"Tamang Range: 3 - 25 m/s";
            }
            else // Kung summary report lang
            {
                // Kunin ang base report galing sa magulang, tapos dagdagan ng info tungkol sa hangin
                return base.GenerateReport(reportType) + $" | Hangin: {WindSpeed} m/s";
            }
        }
    }
}