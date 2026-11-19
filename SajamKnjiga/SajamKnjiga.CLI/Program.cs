using SajamKnjiga.Core.DAO;
using SajamKnjiga.CLI.Views;

namespace SajamKnjiga.CLI
{
    class Program
    {
        static void Main()
        {   KupovinaDAO kupovinaDAO = new KupovinaDAO();
            ListaZeljaDAO listaZeljaDAO = new ListaZeljaDAO();
            KnjigaDAO knjigaDAO = new KnjigaDAO(kupovinaDAO, listaZeljaDAO);
            IzdavacDAO izdavacDAO = new IzdavacDAO();
            AutorDAO autorDAO = new AutorDAO(knjigaDAO, izdavacDAO);
            PosetilacDAO posetilacDAO = new PosetilacDAO();

            knjigaDAO.Povezi(autorDAO.NadjiSveAutore());
            kupovinaDAO.PoveziKupovineSaPosetiocima(posetilacDAO.NadjiSvePosetioce(), knjigaDAO.SveKnjige());
            knjigaDAO.PoveziKupce(kupovinaDAO.NadjiSveKupovine());
            izdavacDAO.Povezi(autorDAO.NadjiSveAutore(), knjigaDAO.SveKnjige());
            listaZeljaDAO.Povezi(posetilacDAO.NadjiSvePosetioce(), knjigaDAO.SveKnjige());

            AutorConsoleView autorView = new AutorConsoleView(autorDAO);
            KnjigaConsoleView knjigaView = new KnjigaConsoleView(knjigaDAO, autorDAO);
            PosetilacConsoleView posetilacView = new PosetilacConsoleView(posetilacDAO, kupovinaDAO, listaZeljaDAO, knjigaDAO);
            KupovinaConsoleView kupovinaView = new KupovinaConsoleView(kupovinaDAO, posetilacDAO, knjigaDAO, listaZeljaDAO);
            IzdavacConsoleView izdavacView = new IzdavacConsoleView(izdavacDAO, autorDAO);

            RunMainMenu(autorView, knjigaView, posetilacView, kupovinaView, izdavacView);
        }

        private static void RunMainMenu(AutorConsoleView autorView, KnjigaConsoleView knjigaView, PosetilacConsoleView posetilacView, KupovinaConsoleView kupovinaView, IzdavacConsoleView izdavacView)
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
                    case "3":
                        posetilacView.RunMenu();
                        break;
                    case "4":
                        kupovinaView.RunMenu();
                        break;
                    case "5":
                        izdavacView.RunMenu();
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
            System.Console.WriteLine("3. Posetioci");
            System.Console.WriteLine("4. Kupovine");
            System.Console.WriteLine("5. Izdavaci");
            System.Console.WriteLine("0. Izlaz");
        }
    }
}

