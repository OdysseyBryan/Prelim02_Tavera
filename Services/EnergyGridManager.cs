using System;
using System.Collections.Generic;
using System.IO;
using Tavera_ScenarioC.Models;
using Tavera_ScenarioC.AbstractClasses;

namespace Tavera_ScenarioC.Services
{
    /// <summary>
    /// SERVICE CLASS: Ito yung nagma-manage ng lahat ng power sources
    /// at nagpapakita ng menu sa user. Parang supervisor ng buong system.
    /// </summary>
    public class EnergyGridManager
    {
        // Listahan ng lahat ng power sources (solar panels at wind turbines)
        // Parang notebook kung saan naka-list lahat ng nilagay mo
        private List<PowerSource> powerSources = new List<PowerSource>();

        // EXTRA FEATURE: Dito sine-save ang history ng mga ginawa mo
        // Para may record ka ng lahat ng reports
        private readonly string logFilePath = "EnergyReportHistory.txt";

        // Constructor - ito yung unang tumatakbo pag gumawa ka ng EnergyGridManager
        // Parang pagbukas ng store, naghahanda ng resibo
        public EnergyGridManager()
        {
            // Kung wala pang history file, gumawa ng bago
            if (!File.Exists(logFilePath))
                File.Create(logFilePath).Close();
        }

        /// <summary>
        /// Main menu - dito umiikot ang buong program
        /// Paulit-ulit hanggang mag-exit ang user
        /// </summary>
        public void Run()
        {
            while (true) // Habang totoo (forever hanggang mag-6)
            {
                try
                {
                    DisplayMenu(); // Ipakita ang menu
                    string choice = Console.ReadLine(); // Kunin ang sagot ng user

                    // Kung ano pinili ni user, yun ang gagawin
                    switch (choice)
                    {
                        case "1":
                            AddSolarPanel(); // Mag-add ng solar panel
                            break;
                        case "2":
                            AddWindTurbine(); // Mag-add ng wind turbine
                            break;
                        case "3":
                            ViewAllReports(); // Tingnan lahat ng reports
                            break;
                        case "4":
                            GenerateDetailedReport(); // Gumawa ng detailed report
                            break;
                        case "5":
                            ViewHistory(); // Tingnan ang history log
                            break;
                        case "6":
                            Console.WriteLine("\nThank you for using Eco-Grid Manager!");
                            Console.WriteLine("System Shutting down...");
                            return; // Exit na sa program
                        default:
                            // Pag hindi 1-6 ang pinili, error
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex) // Pag may error, hulihin dito para hindi mag-crash
                {
                    Console.WriteLine($"\n[ERROR] {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        // Ipinapakita ang menu sa screen
        private void DisplayMenu()
        {
            Console.Clear(); // Linisin muna ang screen
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║    ECO-GRID ENERGY MANAGER    ║");
            Console.WriteLine("╠════════════════════════════════╣");
            Console.WriteLine("║ 1. Add Solar Panel            ║");
            Console.WriteLine("║ 2. Add Wind Turbine           ║");
            Console.WriteLine("║ 3. View All Summary Reports   ║");
            Console.WriteLine("║ 4. Generate Detailed Report   ║");
            Console.WriteLine("║ 5. View History Log           ║");
            Console.WriteLine("║ 6. Exit                        ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.Write("\nEnter your choice: ");
        }

        // Magdagdag ng bagong Solar Panel
        private void AddSolarPanel()
        {
            Console.Clear();
            Console.WriteLine("🔆 ADD SOLAR PANEL");
            Console.WriteLine("──────────────────");

            try
            {
                // Tanungin ang user ng details
                Console.Write("Enter Solar Panel ID: ");
                string id = Console.ReadLine();

                Console.Write("Enter Base Output (kW): ");
                double baseOutput = double.Parse(Console.ReadLine()); // I-convert sa number

                Console.Write("Enter Sunlight Percentage (0-100): ");
                double sunlight = double.Parse(Console.ReadLine());

                // Gumawa ng bagong SolarPanel gamit ang input ng user
                var solar = new SolarPanel(id, baseOutput, sunlight);
                powerSources.Add(solar); // Isama sa listahan

                // I-save sa history na may bagong solar panel
                LogToFile($"ADDED: Solar Panel {id} - Base: {baseOutput}kW");

                Console.WriteLine("\n[SUCCESS] Solar Panel added successfully!");
            }
            catch (FormatException) // Pag mali ang format (letra instead na numero)
            {
                throw new ArgumentException("Invalid number format. Please enter numeric values.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // Magdagdag ng bagong Wind Turbine
        private void AddWindTurbine()
        {
            Console.Clear();
            Console.WriteLine("💨 ADD WIND TURBINE");
            Console.WriteLine("───────────────────");

            try
            {
                Console.Write("Enter Wind Turbine ID: ");
                string id = Console.ReadLine();

                Console.Write("Enter Base Output (kW): ");
                double baseOutput = double.Parse(Console.ReadLine());

                Console.Write("Enter Wind Speed (m/s): ");
                double windSpeed = double.Parse(Console.ReadLine());

                // Gumawa ng bagong WindTurbine
                var wind = new WindTurbine(id, baseOutput, windSpeed);
                powerSources.Add(wind);

                // I-save sa history
                LogToFile($"ADDED: Wind Turbine {id} - Base: {baseOutput}kW");

                Console.WriteLine("\n[SUCCESS] Wind Turbine added successfully!");
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid number format. Please enter numeric values.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // Tingnan ang summary ng lahat ng power sources
        private void ViewAllReports()
        {
            Console.Clear();
            Console.WriteLine("ALL POWER SOURCES - SUMMARY REPORT");
            Console.WriteLine("══════════════════════════════════════\n");

            // Kung walang laman ang listahan
            if (powerSources.Count == 0)
            {
                Console.WriteLine("No power sources in the system yet.");
            }
            else
            {
                double totalOutput = 0; // Para sa total ng lahat ng output

                // Tingnan isa-isa ang bawat power source sa listahan
                foreach (var source in powerSources)
                {
                    // POLYMORPHISM: Pareho ang tawag (GenerateReport) pero iba ang output
                    // depende kung SolarPanel or WindTurbine ito
                    Console.WriteLine(source.GenerateReport("summary"));
                    totalOutput += source.CalculateCurrentOutput(); // Kunin ang output at i-add sa total
                    Console.WriteLine(new string('─', 50)); // Guhit na pampaganda
                }

                Console.WriteLine($"\n[TOTAL] GRID OUTPUT: {totalOutput:F2} kW");

                // I-save sa history ang summary
                LogToFile($"SUMMARY: Total Output = {totalOutput:F2}kW from {powerSources.Count} sources");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // Gumawa ng detailed report para sa isang specific na power source
        private void GenerateDetailedReport()
        {
            Console.Clear();
            Console.WriteLine("GENERATE DETAILED REPORT");
            Console.WriteLine("════════════════════════════\n");

            if (powerSources.Count == 0)
            {
                Console.WriteLine("No power sources to report.");
            }
            else
            {
                // Ipakita kung ano-ano ang pwedeng pagpilian
                Console.WriteLine("Select power source:");
                for (int i = 0; i < powerSources.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {powerSources[i].SourceID} ({powerSources[i].GetType().Name})");
                }

                Console.Write("\nEnter number: ");
                // Kunin ang pinili ng user at i-check kung valid
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= powerSources.Count)
                {
                    var source = powerSources[choice - 1]; // -1 kasi 0 ang starting index sa listahan

                    // POLYMORPHISM: Detailed version ng report
                    string detailedReport = source.GenerateReport("detailed");
                    Console.WriteLine($"\n{detailedReport}");

                    // I-save sa history ang detailed report
                    LogToFile($"DETAILED: {source.SourceID} - {source.GenerateReport("summary")}");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // Tingnan ang history log (record ng lahat ng ginawa)
        private void ViewHistory()
        {
            Console.Clear();
            Console.WriteLine("HISTORY LOG");
            Console.WriteLine("═══════════════\n");

            try
            {
                // Basahin ang lahat ng lines sa history file
                string[] history = File.ReadAllLines(logFilePath);

                if (history.Length == 0)
                {
                    Console.WriteLine("No history available.");
                }
                else
                {
                    // I-print isa-isa ang bawat entry sa history
                    foreach (string entry in history)
                    {
                        Console.WriteLine(entry);
                    }
                }
            }
            catch (Exception ex) // Pag may error sa pagbasa ng file
            {
                Console.WriteLine($"Error reading history: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // Mag-save ng message sa history file
        private void LogToFile(string message)
        {
            try
            {
                // Kunin ang current date and time (para may timestamp)
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                // Idagdag sa file ang message kasama ang timestamp
                File.AppendAllText(logFilePath, $"[{timestamp}] {message}\n");
            }
            catch
            {
                // Pag nag-error sa pag-save, huwag na ipaalam sa user
                // Para hindi maistorbo ang gamit nila ng program
            }
        }
    }
}