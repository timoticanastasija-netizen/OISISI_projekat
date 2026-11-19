using SajamKnjiga.Core.Model;
using SajamKnjiga.Core.DAO;

namespace SajamKnjiga.CLI.Views;

class KnjigaConsoleView
{
    private readonly KnjigaDAO _knjigaDAO;
    private readonly AutorDAO _autorDAO;

    public KnjigaConsoleView(KnjigaDAO knjigaDAO, AutorDAO autorDAO)
    {
        _knjigaDAO = knjigaDAO;
        _autorDAO = autorDAO;
    }
    public void RunMenu()
    {
        while(true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            HandleMenuInput(userInput);
        }
    }
    private void ShowMenu()
    {
        System.Console.WriteLine("Knjiga Meni");
        System.Console.WriteLine("1. Prikazi sve knjige");
        System.Console.WriteLine("2. Dodaj novu knjigu");
        System.Console.WriteLine("3. Obrisi knjigu");
        System.Console.WriteLine("4. Izmeni knjigu");
        System.Console.WriteLine("0. Nazad");
    }

    private void HandleMenuInput(string input)
    {
        switch(input)
        {
            case "1":
                ShowAllKnjige();
                break;
            case "2":
                AddKnjiga();
                break;
            case "3":
                RemoveKnjiga();
                break;
            case "4":
                UpdateKnjiga();
                break;
            default:
                System.Console.WriteLine("Nepoznata opcija.");
                break;
        }
    }

    private void ShowAllKnjige()
    {
        System.Console.WriteLine("Lista svih knjiga:");
        foreach (Knjiga k in _knjigaDAO.SveKnjige())
        {
            System.Console.WriteLine(k);
        }
    }

    private Knjiga InputKnjiga()
    {
        Knjiga knjiga = new Knjiga();
        knjiga.Isbn = ConsoleViewUtils.InputString("Unesite ISBN: ");
        knjiga.Naziv = ConsoleViewUtils.InputString("Unesite naziv: ");
        knjiga.Zanr = ConsoleViewUtils.InputString("Unesite žanr: ");
        knjiga.GodinaIzdavanja = ConsoleViewUtils.InputInt("Unesite godinu izdavanja: ");
        knjiga.Cena = ConsoleViewUtils.InputDecimal("Unesite cenu: ");
        knjiga.BrojStrana = ConsoleViewUtils.InputInt("Unesite broj strana: ");

        int izdavacId = ConsoleViewUtils.InputInt("Unesite ID izdavača (0 ako nema izdavača): ");
        if (izdavacId == 0)
            knjiga.IzdavacId = null;
        else
            knjiga.IzdavacId = izdavacId;

        knjiga.Autori = InputAutori();
        return knjiga;
    }
    private List<Autor> InputAutori()
    {
        List<Autor> izabrani = new List<Autor>();

        System.Console.WriteLine("Dostupni autori: ");
        foreach (Autor a in _autorDAO.NadjiSveAutore())
        {
            System.Console.WriteLine($"{a.Id}: {a.Ime} {a.Prezime}");
        }

        while (true)
        {
            int id = ConsoleViewUtils.InputInt("Unesite ID autora (0 za kraj): ");
            if (id == 0) break;

            Autor? autor = _autorDAO.NadjiAutoraPoId(id);
            if (autor is null)
            {
                System.Console.WriteLine("Autor nije pronađen");
            }
            else if (izabrani.Contains(autor))
            {
                System.Console.WriteLine("Autor je već izabran");
            }
            else
            {
                izabrani.Add(autor);
            }
        }

        return izabrani;
    }

    private void AddKnjiga()
    {
        Knjiga knjiga = InputKnjiga();
        _knjigaDAO.DodajKnjigu(knjiga);
        System.Console.WriteLine("Knjiga je dodata.");
    }

    private void UpdateKnjiga()
    {
        int id = ConsoleViewUtils.InputInt("Unesite ID knjige za izmenu: ");
        Knjiga? knjiga = _knjigaDAO.NadjiKnjigu(id);
        if (knjiga is null)
        {
            System.Console.WriteLine("Knjiga nije pronađena.");
            return;
        }

        Knjiga updatedKnjiga = InputKnjiga();
        updatedKnjiga.Id = id; // zadržavamo isti ID
        _knjigaDAO.IzmeniKnjigu(updatedKnjiga);
        System.Console.WriteLine("Knjiga je izmenjena.");
    }

    private void RemoveKnjiga()
    {
        int id = ConsoleViewUtils.InputInt("Unesite ID knjige za brisanje: ");
        Knjiga? obrisana = _knjigaDAO.ObrisiKnjigu(id);
        if (obrisana is null)
        {
            System.Console.WriteLine("Knjiga nije pronađena.");
            return;
        }

        System.Console.WriteLine("Knjiga je obrisana.");
    }
}