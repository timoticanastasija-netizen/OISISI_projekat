using SajamKnjiga.Core.DAO;
using SajamKnjiga.CLI.Views;

namespace SajamKnjiga.CLI
{
    class Program
    {
        static void Main()
        {
            KnjigaDAO knjigaDAO = new KnjigaDAO();
            AutorDAO autorDAO = new AutorDAO(knjigaDAO);

            knjigaDAO.Povezi(autorDAO.NadjiSveAutore());

            AutorConsoleView autorView = new AutorConsoleView(autorDAO);
            KnjigaConsoleView knjigaView = new KnjigaConsoleView(knjigaDAO, autorDAO);
            
            RunMainMenu(autorView, knjigaView);
        }

        private static void RunMainMenu(AutorConsoleView autorView, KnjigaConsoleView knjigaView)
        {
            // Implementation for running the main menu
            while (true)
            {
                ShowMainMenu();
                string userInput = System.Console.ReadLine() ?? "0";
                if (userInput == "0") break;

                switch (userInput)
                {
                    case "1":
                        autorView.RunMenu();
                        break;
                    case "2":
                        knjigaView.RunMenu();
                        break;
                    default:
                        System.Console.WriteLine("Nepoznata opcija.");
                        break;
                }
                
            }
        }

        private static void ShowMainMenu()
        {
            System.Console.WriteLine("Glavni Meni Uprave Sajma Knjiga");
            System.Console.WriteLine("1. Autori");
            System.Console.WriteLine("2. Knjige");
            System.Console.WriteLine("0. Izlaz");
        }
    }
}
