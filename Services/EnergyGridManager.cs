using System;
using System.Collections.Generic;
using System.IO;
using Tavera_ScenarioC.Models;
using Tavera_ScenarioC.AbstractClasses;

namespace Tavera_ScenarioC.Services
{
    /// <summary>
    /// SERVICE CLASS: Manages all power sources and provides user interface
    /// Demonstrates SEPARATION OF CONCERNS
    /// </summary>
    public class EnergyGridManager
    {
        // Collection to store all power sources
        private List<PowerSource> powerSources = new List<PowerSource>();

        // SMALL FEATURE: History log file path
        private readonly string logFilePath = "EnergyReportHistory.txt";

        // Constructor
        public EnergyGridManager()
        {
            // Initialize log file
            if (!File.Exists(logFilePath))
                File.Create(logFilePath).Close();
        }

        /// <summary>
        /// Main menu loop
        /// </summary>
        public void Run()
        {
            while (true)
            {
                try
                {
                    DisplayMenu();
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddSolarPanel();
                            break;
                        case "2":
                            AddWindTurbine();
                            break;
                        case "3":
                            ViewAllReports();
                            break;
                        case "4":
                            GenerateDetailedReport();
                            break;
                        case "5":
                            ViewHistory();
                            break;
                        case "6":
                            Console.WriteLine("\nThank you for using Eco-Grid Manager!");
                            Console.WriteLine("System Shutting down...");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERROR] {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private void DisplayMenu()
        {
            Console.Clear();
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

        private void AddSolarPanel()
        {
            Console.Clear();
            Console.WriteLine("🔆 ADD SOLAR PANEL");
            Console.WriteLine("──────────────────");

            try
            {
                Console.Write("Enter Solar Panel ID: ");
                string id = Console.ReadLine();

                Console.Write("Enter Base Output (kW): ");
                double baseOutput = double.Parse(Console.ReadLine());

                Console.Write("Enter Sunlight Percentage (0-100): ");
                double sunlight = double.Parse(Console.ReadLine());

                var solar = new SolarPanel(id, baseOutput, sunlight);
                powerSources.Add(solar);

                // Log the addition
                LogToFile($"ADDED: Solar Panel {id} - Base: {baseOutput}kW");

                Console.WriteLine("\n[SUCCESS] Solar Panel added successfully!");
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid number format. Please enter numeric values.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

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

                var wind = new WindTurbine(id, baseOutput, windSpeed);
                powerSources.Add(wind);

                // Log the addition
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

        private void ViewAllReports()
        {
            Console.Clear();
            Console.WriteLine("ALL POWER SOURCES - SUMMARY REPORT");
            Console.WriteLine("══════════════════════════════════════\n");

            if (powerSources.Count == 0)
            {
                Console.WriteLine("No power sources in the system yet.");
            }
            else
            {
                double totalOutput = 0;
                foreach (var source in powerSources)
                {
                    // POLYMORPHISM in action: same method call, different behavior
                    Console.WriteLine(source.GenerateReport("summary"));
                    totalOutput += source.CalculateCurrentOutput();
                    Console.WriteLine(new string('─', 50));
                }

                Console.WriteLine($"\n[TOTAL] GRID OUTPUT: {totalOutput:F2} kW");

                // SMALL FEATURE: Auto-save summary to history
                LogToFile($"SUMMARY: Total Output = {totalOutput:F2}kW from {powerSources.Count} sources");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

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
                Console.WriteLine("Select power source:");
                for (int i = 0; i < powerSources.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {powerSources[i].SourceID} ({powerSources[i].GetType().Name})");
                }

                Console.Write("\nEnter number: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= powerSources.Count)
                {
                    var source = powerSources[choice - 1];

                    // POLYMORPHISM: Detailed report
                    string detailedReport = source.GenerateReport("detailed");
                    Console.WriteLine($"\n{detailedReport}");

                    // SMALL FEATURE: Save to history
                    LogToFile($"DETAILED: {source.SourceID} - {source.GenerateReport("summary")}");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void ViewHistory()
        {
            Console.Clear();
            Console.WriteLine("HISTORY LOG");
            Console.WriteLine("═══════════════\n");

            try
            {
                string[] history = File.ReadAllLines(logFilePath);
                if (history.Length == 0)
                {
                    Console.WriteLine("No history available.");
                }
                else
                {
                    foreach (string entry in history)
                    {
                        Console.WriteLine(entry);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading history: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void LogToFile(string message)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                File.AppendAllText(logFilePath, $"[{timestamp}] {message}\n");
            }
            catch
            {
                // Silent fail - don't interrupt user if logging fails
            }
        }
    }
}