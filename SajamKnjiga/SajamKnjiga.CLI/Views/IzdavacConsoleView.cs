
using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Model;

namespace SajamKnjiga.CLI.Views;

// po uzoru na vezbe 3 CRUDExample - Views/VehicleConsoleView.cs

class IzdavacConsoleView
{

    private readonly IzdavacDAO _izdavacDAO;
    private readonly AutorDAO _autorDAO;
    public IzdavacConsoleView(IzdavacDAO izdavacDAO, AutorDAO autorDAO)
    {
        _izdavacDAO = izdavacDAO;
        _autorDAO = autorDAO;
    }

    public void RunMenu()
    {
        while (true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "";
            if (userInput == "0") break;
            HandleMenuInput(userInput);
        }
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("Meni Izdavaca:");
        System.Console.WriteLine("1. Prikazi sve izdavace");
        System.Console.WriteLine("2. Dodaj izdavaca");
        System.Console.WriteLine("3. Izmeni izdavaca");
        System.Console.WriteLine("4. Obrisi izdavaca");
        System.Console.WriteLine("0. Povratak na glavni meni");
        System.Console.Write("Izaberite opciju: ");
    }

    private void HandleMenuInput(string userInput)
    {
        switch (userInput)
        {
            case "1":
                ShowAllIzdavaci();
                break;
            case "2":
                AddIzdavac();
                break;
            case "3":
                UpdateIzdavac();
                break;
            case "4":
                RemoveIzdavac();
                break;
            default:
                System.Console.WriteLine("Nepoznata opcija.");
                break;
        }
    }

    private void ShowAllIzdavaci()
    {
        System.Console.WriteLine("=== Lista izdavaca ===");
        foreach (Izdavac i in _izdavacDAO.NadjiSveIzdavace())
        {
            System.Console.WriteLine(i);
        }
    }

    private Izdavac InputIzdavac(int id)
    {
        Izdavac izdavac = new Izdavac();
        izdavac.Id = id;
        izdavac.Sifra = InputSifra(id);
        izdavac.Naziv = ConsoleViewUtils.InputString("Unesite naziv izdavaca: ");
        izdavac.Sef = InputSef();
        izdavac.SefId = izdavac.Sef?.Id;
        return izdavac;
    }

    private string InputSifra(int id)
    {
        while (true)
        {
            string sifra = ConsoleViewUtils.InputString("Unesite sifru izdavaca: ");
            bool zauzeta = _izdavacDAO.NadjiSveIzdavace().Exists(i => i.Sifra == sifra && i.Id != id);
            if (!zauzeta)
                return sifra;

            System.Console.WriteLine("Izdavac sa tom sifrom vec postoji.");
        }
    }

    private Autor? InputSef()
    {
        while (true)
        {
            int autorId = ConsoleViewUtils.InputInt(
                $"Unesite ID autora koji je sef (min. {Izdavac.MinGodinaIskustvaZaSefa} god. iskustva, 0 = bez sefa): ");
            if (autorId == 0)
                return null;

            Autor? autor = _autorDAO.NadjiAutoraPoId(autorId);
            if (autor is null)
            {
                System.Console.WriteLine("Autor sa datim ID-jem ne postoji.");
            }
            else if (!Izdavac.MozeBitiSef(autor))
            {
                System.Console.WriteLine($"Autor ima {autor.GodineIskustva} god. iskustva, a sef mora imati bar {Izdavac.MinGodinaIskustvaZaSefa}.");
            }
            else
            {
                return autor;
            }
        }
    }

    private void AddIzdavac()
    {
        Izdavac izdavac = InputIzdavac(0);
        _izdavacDAO.DodajIzdavaca(izdavac);
        System.Console.WriteLine("Izdavac je uspešno dodat.");
    }

    private void UpdateIzdavac()
    {
        int id = ConsoleViewUtils.InputInt("Unesite ID izdavaca koga želite da izmenite: ");
        if (_izdavacDAO.NadjiIzdavacaPoId(id) is null)
        {
            System.Console.WriteLine("Izdavac sa datim ID-jem ne postoji.");
            return;
        }

        Izdavac izdavac = InputIzdavac(id);
        _izdavacDAO.IzmeniIzdavaca(izdavac);
        System.Console.WriteLine("Izdavac je uspešno izmenjen.");
    }

    private void RemoveIzdavac()
    {
        int id = ConsoleViewUtils.InputInt("Unesite ID izdavaca koga želite da obrišete: ");
        Izdavac? izdavac = _izdavacDAO.NadjiIzdavacaPoId(id);
        if (izdavac is null)
        {
            System.Console.WriteLine("Izdavac sa datim ID-jem ne postoji.");
            return;
        }

        // Referencijalni integritet: izdavač koji ima knjige se ne briše
        if (izdavac.Knjige.Count > 0)
        {
            System.Console.WriteLine($"Izdavac ima {izdavac.Knjige.Count} knjiga i ne može se obrisati.");
            return;
        }

        _izdavacDAO.ObrisiIzdavaca(id);
        System.Console.WriteLine("Izdavac je uspešno obrisan.");
    }

}