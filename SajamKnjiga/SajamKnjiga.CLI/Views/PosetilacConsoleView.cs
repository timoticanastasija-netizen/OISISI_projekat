
using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Model;


namespace SajamKnjiga.CLI.Views;

// po uzoru na vezbe 3 CRUDExample - Views/VehicleConsoleView.cs

 class PosetilacConsoleView
{
    private readonly PosetilacDAO _posetilacDAO;
    private readonly KupovinaDAO _kupovinaDAO;
    private readonly ListaZeljaDAO _listaZeljaDAO;

    private readonly KnjigaDAO _knjigaDAO;

    public PosetilacConsoleView(PosetilacDAO posetilacDAO, KupovinaDAO kupovinaDAO, ListaZeljaDAO listaZeljaDAO, KnjigaDAO knjigaDAO)
    {
        _posetilacDAO = posetilacDAO;
        _kupovinaDAO = kupovinaDAO;
        _listaZeljaDAO = listaZeljaDAO;
        _knjigaDAO = knjigaDAO;
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

    public void ShowMenu()
    {
        System.Console.WriteLine("Meni Posetioca:");
        System.Console.WriteLine("1. Prikazi sve posetioce");
        System.Console.WriteLine("2. Dodaj posetioca");
        System.Console.WriteLine("3. Izmeni posetioca");
        System.Console.WriteLine("4. Obrisi posetioca");
        System.Console.WriteLine("5. Dodaj knjigu u listu zelja");
        System.Console.WriteLine("6. Prikazi listu zelja posetioca");
        System.Console.WriteLine("0. Povratak na glavni meni");
        System.Console.Write("Izaberite opciju: ");
    }

    private void HandleMenuInput(string userInput)
    {
        switch (userInput)
        {
            case "1":
                ShowAllPosetioci();
                break;
            case "2":
                AddPosetilac();
                break;
            case "3":
                UpdatePosetilac();
                break;
            case "4":
                RemovePosetilac();
                break;
            case "5":
                AddNaListuZelja();
                break;
            case "6":
                ShowListaZelja();
                break;
            default:
                System.Console.WriteLine("Nepoznata opcija.");
                break;
        }
    }

    private void ShowAllPosetioci()
    {
        System.Console.WriteLine("=== Lista posetilaca ===");
        foreach (Posetilac p in _posetilacDAO.NadjiSvePosetioce())
        {
            System.Console.WriteLine(p);
        }
    }
        private void AddNaListuZelja()
    {
        int posetilacId = ConsoleViewUtils.InputInt("Unesite ID posetioca: ");
        Posetilac? posetilac = _posetilacDAO.NadjiPosetiocaPoId(posetilacId);
        if (posetilac == null)
        {
            System.Console.WriteLine("Posetilac sa datim ID-jem ne postoji.");
            return;
        }

        int knjigaId = ConsoleViewUtils.InputInt("Unesite ID knjige: ");
        Knjiga? knjiga = _knjigaDAO.NadjiKnjigu(knjigaId);
        if (knjiga == null)
        {
            System.Console.WriteLine("Knjiga sa datim ID-jem ne postoji.");
            return;
        }

        if (_listaZeljaDAO.DodajNaListuZelja(posetilac, knjiga))
            System.Console.WriteLine("Knjiga je dodata na listu zelja.");
        else
            System.Console.WriteLine("Knjiga je vec na listi zelja ili ju je posetilac vec kupio.");
    }

    private void ShowListaZelja()
    {
        int posetilacId = ConsoleViewUtils.InputInt("Unesite ID posetioca: ");
        Posetilac? posetilac = _posetilacDAO.NadjiPosetiocaPoId(posetilacId);
        if (posetilac == null)
        {
            System.Console.WriteLine("Posetilac sa datim ID-jem ne postoji.");
            return;
        }

        System.Console.WriteLine("=== Lista zelja ===");
        foreach (Knjiga k in posetilac.ListaZelja)
        {
            System.Console.WriteLine(k);
        }
    }

    private Posetilac InputPosetilac()
    {
        Posetilac posetilac = new Posetilac();
        posetilac.Ime = ConsoleViewUtils.InputString("Unesite ime: ");
        posetilac.Prezime = ConsoleViewUtils.InputString("Unesite prezime: ");
        posetilac.DatumRodjenja = ConsoleViewUtils.InputDate("Unesite datum rođenja: ");
        posetilac.Adresa = ConsoleViewUtils.InputAdresa();
        posetilac.Telefon = ConsoleViewUtils.InputString("Unesite kontakt telefon: ");
        posetilac.Email = ConsoleViewUtils.InputString("Unesite email: ");
        posetilac.BrojClanskeKarte = ConsoleViewUtils.InputString("Unesite broj članske karte (npr. SK-1-2023): ");
        posetilac.TrenutnaGodinaClanstva = ConsoleViewUtils.InputInt("Unesite trenutnu godinu članstva: ");
        posetilac.Status = InputStatus();

        return posetilac;
    }

    private StatusPosetioca InputStatus()
    {
        while (true)
        {
            string statusInput = ConsoleViewUtils.InputString("Unesite status posetioca (R - Redovan, V - VIP): ");
            if (statusInput.ToUpper() == "R")
                return StatusPosetioca.R;
            else if (statusInput.ToUpper() == "V")
                return StatusPosetioca.V;
            else
                System.Console.WriteLine("Nepoznat status. Pokušajte ponovo.");
        }
    }

    private void UpdatePosetilac()
    {
        int id = ConsoleViewUtils.InputInt("Unesite ID posetioca za izmenu: ");
        Posetilac? existingPosetilac = _posetilacDAO.NadjiPosetiocaPoId(id);
        if (existingPosetilac is null)
        {
            System.Console.WriteLine($"Posetilac sa ID {id} ne postoji.");
            return;
        }
        Posetilac posetilac = InputPosetilac();
        posetilac.Id = id;
        _posetilacDAO.IzmeniPosetioca(posetilac);
        System.Console.WriteLine("Posetilac je uspešno izmenjen.");
    }

    private void AddPosetilac()
    {
        Posetilac posetilac = InputPosetilac();
        _posetilacDAO.DodajPosetioca(posetilac);
        System.Console.WriteLine("Posetilac je uspešno dodat.");
    }


    private void RemovePosetilac()
    {
        int id = ConsoleViewUtils.InputInt("Unesite ID posetioca koga želite da obrišete: ");
        Posetilac? posetilac = _posetilacDAO.NadjiPosetiocaPoId(id);
        if (posetilac is null)
        {
            System.Console.WriteLine("Posetilac sa datim ID-jem ne postoji.");
            return;
        }

        // Referencijalni integritet: uz posetioca se brisu njegove kupovine i lista želja
        foreach (Kupovina kupovina in posetilac.Kupovine)
        {
            kupovina.Knjiga?.Kupci.Remove(posetilac);
        }
        _kupovinaDAO.ObrisiKupovinePosetioca(posetilac.Id);
        _listaZeljaDAO.UkloniSveStavkeZaPosetioca(posetilac);
        _posetilacDAO.ObrisiPosetioca(posetilac.Id);

        System.Console.WriteLine("Posetilac je uspešno obrisan.");
    }




}
