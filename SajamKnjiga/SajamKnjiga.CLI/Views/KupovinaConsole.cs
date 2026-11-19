
using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Model;


namespace SajamKnjiga.CLI.Views;

//po uzoru na vezbe 3 CRUDExample - Console/VehicleConsoleView.cs
 class KupovinaConsoleView
{
    private readonly KupovinaDAO _kupovinaDAO;
    private readonly PosetilacDAO _posetilacDAO;
    private readonly KnjigaDAO _knjigaDAO;
    private readonly ListaZeljaDAO _listaZeljaDAO;
    public KupovinaConsoleView(KupovinaDAO kupovinaDAO, PosetilacDAO posetilacDAO, KnjigaDAO knjigaDAO, ListaZeljaDAO listaZeljaDAO)
    {
        _kupovinaDAO = kupovinaDAO;
        _posetilacDAO = posetilacDAO;
        _knjigaDAO = knjigaDAO;
        _listaZeljaDAO = listaZeljaDAO;
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
        System.Console.WriteLine("Meni Kupovine:");
        System.Console.WriteLine("1. Prikazi sve kupovine");
        System.Console.WriteLine("2. Dodaj kupovinu");
        System.Console.WriteLine("3. Izmeni kupovinu");
        System.Console.WriteLine("4. Obrisi kupovinu");
        System.Console.WriteLine("0. Povratak na glavni meni");
        System.Console.Write("Izaberite opciju: ");
    }

    private void HandleMenuInput(string userInput)
    {
        switch (userInput)
        {
            case "1":
                ShowAllKupovine();
                break;
            case "2":
                AddKupovina();
                break;
            case "3":
                UpdateKupovina();
                break;
            case "4":
                RemoveKupovina();
                break;
            default:
                System.Console.WriteLine("Nepoznata opcija.");
                break;
        }
    }

    private void ShowAllKupovine()
    {
        System.Console.WriteLine("=== Lista kupovina ===");
        foreach (Kupovina k in _kupovinaDAO.NadjiSveKupovine())
        {
            System.Console.WriteLine(k);
        }
    }

    private void InputPodaciKupovine(Kupovina kupovina)
    {
        kupovina.DatumKupovine = InputDatumKupovine();
        kupovina.Ocena = InputOcena();
        kupovina.Komentar = ConsoleViewUtils.InputString("Unesite komentar: ");
    }

    private DateTime InputDatumKupovine()
    {
        while (true)
        {
            DateTime datum = ConsoleViewUtils.InputDate("Unesite datum kupovine: ");
            if (datum <= DateTime.Today)
                return datum;
            
                System.Console.WriteLine("Neispravan format datuma. Pokušajte ponovo.");
            
        }
    }

    private int InputOcena()
    {
        while (true)
        {
            int ocena = ConsoleViewUtils.InputInt("Unesite ocenu (1-5): ");
            if (ocena >= 1 && ocena <= 5)
                return ocena;
           
                System.Console.WriteLine("Ocena mora biti između 1 i 5. Pokušajte ponovo.");
            
        }
    }

    private void AddKupovina()
    {
        System.Console.WriteLine("=== Dodavanje nove kupovine ===");
        int posetilacId = ConsoleViewUtils.InputInt("Unesite ID posetioca: ");
        Posetilac? posetilac = _posetilacDAO.NadjiPosetiocaPoId(posetilacId);
        if (posetilac is null)
        {
            System.Console.WriteLine("Posetilac sa ID {posetilacId} ne postoji.");
            return;
        }

        int knjigaId = ConsoleViewUtils.InputInt("Unesite ID knjige: ");
        Knjiga? knjiga = _knjigaDAO.NadjiKnjigu(knjigaId);
        if (knjiga is null)
        {
            System.Console.WriteLine("Knjiga sa datim ID-jem ne postoji.");
            return;
        }

        if (posetilac.Kupovine.Exists(k => k.KnjigaId == knjiga.Id))
        {
            System.Console.WriteLine("Posetilac je već kupio ovu knjigu.");
            return;
        }

        Kupovina novaKupovina = new Kupovina();
        novaKupovina.PosetilacId = posetilacId;
        novaKupovina.KnjigaId = knjigaId;
        novaKupovina.Posetilac = posetilac;
        novaKupovina.Knjiga = knjiga;
        InputPodaciKupovine(novaKupovina);

        _kupovinaDAO.DodajKupovinu(novaKupovina);
        knjiga.Kupci.Add(posetilac);
        _listaZeljaDAO.ObrisiSaListeZelja(posetilac, knjiga);

        System.Console.WriteLine("Kupovina je uspešno dodata.");
    }

    private void UpdateKupovina()
    {
        int id = ConsoleViewUtils.InputInt("Unesite ID kupovine za izmenu: ");
        if (_kupovinaDAO.NadjiKupovinuPoId(id) is null)
        {
            System.Console.WriteLine($"Kupovina sa datim ID-jem ne postoji.");
            return;
        }

        Kupovina kupovina = new Kupovina();
        kupovina.Id = id;
        InputPodaciKupovine(kupovina);
        _kupovinaDAO.IzmeniKupovinu(kupovina);
        System.Console.WriteLine("Kupovina je uspešno izmenjena.");
    }

    private void RemoveKupovina()
    {
        int id = ConsoleViewUtils.InputInt("Unesite ID kupovine koju želite da obrišete: ");
        Kupovina? kupovina = _kupovinaDAO.NadjiKupovinuPoId(id);
        if (kupovina is null)
        {
            System.Console.WriteLine("Kupovina sa datim ID-jem ne postoji.");
            return;
        }

        if (kupovina.Posetilac is not null)
        {
            kupovina.Knjiga?.Kupci.Remove(kupovina.Posetilac);
        }
        _kupovinaDAO.ObrisiKupovinu(id);
        System.Console.WriteLine("Kupovina je uspešno obrisana.");
    }






}
