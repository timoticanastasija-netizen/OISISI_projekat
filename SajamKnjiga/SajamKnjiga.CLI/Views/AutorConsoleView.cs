using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Model;

namespace SajamKnjiga.CLI.Views;

//Po Vezbi 3

class AutorConsoleView
{
    private readonly AutorDAO _autorDAO;

    public AutorConsoleView(AutorDAO autorDAO)
    {
        _autorDAO = autorDAO;
    }

    public void RunMenu()
    {
        while (true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0")
            {
                break;
            }
            HandleMenuInput(userInput);
        }
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("=== Autor Menu ===");
        System.Console.WriteLine("1. Prikazi sve autore");
        System.Console.WriteLine("2. Dodaj novog autora");
        System.Console.WriteLine("3. Izmeni autora");
        System.Console.WriteLine("4. Obrisi autora");
        System.Console.WriteLine("0. Nazad");
        System.Console.Write("Izaberite opciju: ");
    }

    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                ShowAllAutori();
                break;
            case "2":
                AddAutor();
                break;
            case "3":
                UpdateAutor();
                break;
            case "4":
                RemoveAutor();
                break;
            default:
                System.Console.WriteLine("Nepoznata opcija. Pokusajte ponovo.");
                break;
        }
    }
    private void ShowAllAutori()
    {
        System.Console.WriteLine("=== Lista Autora ===");
        foreach (Autor a in _autorDAO.NadjiSveAutore())
        {
            System.Console.WriteLine(a);
        }
    }
    private Autor InputAutor()
    {
        Autor autor = new Autor();
        autor.Ime = ConsoleViewUtils.InputString("Unesite ime autora: ");
        autor.Prezime = ConsoleViewUtils.InputString("Unesite prezime autora: ");
        autor.DatumRodjenja = ConsoleViewUtils.InputDate("Unesite datum rođenja autora (dd.MM.yyyy): ");
        autor.Adresa = ConsoleViewUtils.InputAdresa();
        autor.Email = ConsoleViewUtils.InputString("Unesite email autora: ");
        autor.Telefon = ConsoleViewUtils.InputString("Unesite telefon autora: ");
        autor.BrojLicneKarte = ConsoleViewUtils.InputString("Unesite broj lične karte autora: ");
        autor.GodineIskustva = ConsoleViewUtils.InputInt("Unesite godine iskustva autora: ");

        return autor;

    }

    private void AddAutor()
    {
        Autor autor = InputAutor();
        _autorDAO.DodajAutora(autor);
        System.Console.WriteLine("Autor je uspešno dodat.");
    }
    private void UpdateAutor()
    {
        int id = ConsoleViewUtils.InputInt("Unesite ID autora koji želite da izmenite: ");
        if (_autorDAO.NadjiAutoraPoId(id) == null)
        {
            System.Console.WriteLine("Autor sa datim ID-jem ne postoji.");
            return;
        }

        Autor autor = InputAutor();
        autor.Id = id; // Obezbediti da ID ostane isti
        _autorDAO.IzmeniAutora(autor);
        System.Console.WriteLine("Autor je uspešno izmenjen.");
    }

    private void RemoveAutor()
    {
        int id = ConsoleViewUtils.InputInt("Unesite ID autora koji želite da obrišete: ");
        Autor? obrisan= _autorDAO.NadjiAutoraPoId(id);
        if (obrisan == null)
        {
            System.Console.WriteLine("Autor sa datim ID-jem ne postoji.");
            return;
        }
        _autorDAO.ObrisiAutora(id);

        System.Console.WriteLine("Autor je uspešno obrisan.");
    }
}