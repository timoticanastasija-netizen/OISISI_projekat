
using SajamKnjiga.Core.Model;
using SajamKnjiga.Core.Storage;

//po uzoru na vezbe 3 SerializationExample, Model/StudentSubject.cs


namespace SajamKnjiga.Core.DAO;
public class ListaZeljaDAO
{
    private readonly List<StavkaListeZelja> _stavke;
    private readonly Storage<StavkaListeZelja> _storage;
    public ListaZeljaDAO()
    {
        _storage = new Storage<StavkaListeZelja>("lista_zelja.txt");
        _stavke = _storage.Load();
    }
    public void Povezi(List<Posetilac> posetioci, List<Knjiga> knjige)
    {
        foreach (StavkaListeZelja stavka in _stavke)
        {
            Posetilac? posetilac = posetioci.Find(p => p.Id == stavka.PosetilacId);
            Knjiga? knjiga = knjige.Find(k => k.Id == stavka.KnjigaId);
            if (posetilac != null && knjiga != null)
            {
                posetilac.ListaZelja.Add(knjiga);
                knjiga.PosetiociListaZelja.Add(posetilac);
            }
        }
    }

    public bool DodajNaListuZelja(Posetilac posetilac, Knjiga knjiga)
    {
        if (posetilac.ListaZelja.Contains(knjiga))
            return false;
        if (posetilac.Kupovine.Exists(k => k.KnjigaId == knjiga.Id))
            return false;

        _stavke.Add(new StavkaListeZelja ( posetilac.Id, knjiga.Id ));
        posetilac.ListaZelja.Add(knjiga);
        knjiga.PosetiociListaZelja.Add(posetilac);
        _storage.Save(_stavke);
        return true;
    }

    public bool ObrisiSaListeZelja(Posetilac posetilac, Knjiga knjiga)
    {
        int obrisano = _stavke.RemoveAll(s => s.PosetilacId == posetilac.Id && s.KnjigaId == knjiga.Id);
        if (obrisano == 0)
            return false;

        posetilac.ListaZelja.Remove(knjiga);
        knjiga.PosetiociListaZelja.Remove(posetilac);
        _storage.Save(_stavke);
        return true;
    }

    public void UkloniSveStavkeZaPosetioca(Posetilac posetilac)
    {
        foreach (Knjiga knjiga in posetilac.ListaZelja)
        {
            knjiga.PosetiociListaZelja.Remove(posetilac);
        }
        posetilac.ListaZelja.Clear();
        _stavke.RemoveAll(s => s.PosetilacId == posetilac.Id);
        _storage.Save(_stavke);
    }

    public void UkloniSveStavkeZaKnjigu(Knjiga knjiga)
    {
        foreach (Posetilac posetilac in knjiga.PosetiociListaZelja)
        {
            posetilac.ListaZelja.Remove(knjiga);
        }
        knjiga.PosetiociListaZelja.Clear();
        _stavke.RemoveAll(s => s.KnjigaId == knjiga.Id);
        _storage.Save(_stavke);
    }

}

