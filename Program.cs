using System;
using Tavera_ScenarioC.Services;

namespace Tavera_ScenarioC
{
    class Program
    {
        // MAIN METHOD - dito nagsisimula ang lahat
        // Parang pintuan ng bahay, dito pumapasok ang program
        static void Main(string[] args)
        {
            // try-catch-finally - pang-hawak ng error para hindi mag-crash ang program
            // Ito ang ROBUSTNESS (matibay na programa)
            try
            {
                // Pampagandang design sa simula ng program
                Console.WriteLine("╔══════════════════════════════════════════╗");
                Console.WriteLine("║   ECO-GRID ENERGY DISTRIBUTION SYSTEM   ║");
                Console.WriteLine("║         Advanced OOP Demonstration       ║");
                Console.WriteLine("╚══════════════════════════════════════════╝");
                Console.WriteLine("\nInitializing system...\n");

                // Gumagawa ng bagong EnergyGridManager
                // Parang pagkuha ng supervisor na magpapatakbo ng system
                EnergyGridManager manager = new EnergyGridManager();

                // Paandarin na ang manager (magpakita ng menu, etc.)
                manager.Run();
            }
            catch (Exception ex) // Pag may error, hulihin dito
            {
                // Ipakita ang error message sa user
                // Para alam nila kung bakit nagka-problema
                Console.WriteLine($"\n[CRITICAL ERROR] {ex.Message}");
                Console.WriteLine("May hindi inaasahang error na nangyari.");
            }
            finally
            {
                // FINALLY - kahit magka-error o hindi, tatakbo ito
                // Parang "siguradhing" mangyayari, parang pagpapatay ng ilaw bago umalis ng bahay
                Console.WriteLine("\n═══════════════════════════════════════════");
                Console.WriteLine("System Shutdown Complete");
                Console.WriteLine("Natapos na ang programa.");
                Console.WriteLine("═══════════════════════════════════════════");

                // Hintaying mag-press ng key ang user bago magsara
                // Para mabasa nila ang mensahe
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}