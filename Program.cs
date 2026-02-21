using System;
using Tavera_ScenarioC.Services;

namespace Tavera_ScenarioC
{
    class Program
    {
        static void Main(string[] args)
        {
            // MAIN METHOD with try-catch-finally for ROBUSTNESS
            try
            {
                Console.WriteLine("╔══════════════════════════════════════════╗");
                Console.WriteLine("║   ECO-GRID ENERGY DISTRIBUTION SYSTEM   ║");
                Console.WriteLine("║         Advanced OOP Demonstration       ║");
                Console.WriteLine("╚══════════════════════════════════════════╝");
                Console.WriteLine("\nInitializing system...\n");

                // Create and run the energy grid manager
                EnergyGridManager manager = new EnergyGridManager();
                manager.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[CRITICAL ERROR] {ex.Message}");
                Console.WriteLine("The application encountered an unexpected error.");
            }
            finally
            {
                // FINALLY block always executes
                Console.WriteLine("\n═══════════════════════════════════════════");
                Console.WriteLine("System Shutdown Complete");
                Console.WriteLine("═══════════════════════════════════════════");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}