using SajamKnjiga.Core.Model;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO;

public class KnjigaDAO{
    private readonly List<Knjiga> _knjige;
    private readonly Storage<Knjiga> _storage;
    private readonly List<AutorKnjiga> _veze;
    private readonly Storage<AutorKnjiga> _vezeStorage;

    public KnjigaDAO() {
        _storage = new Storage<Knjiga>("knjige.txt");
        _knjige = _storage.Load();
        _vezeStorage = new Storage<AutorKnjiga>("autoriKnjiga.txt");
        _veze = _vezeStorage.Load();
    }
    public void Povezi(List<Autor> autori)
    {
        foreach (AutorKnjiga veza in _veze)
        {
            Autor? autor = autori.Find(a => a.Id == veza.AutorId);
            Knjiga? knjiga = NadjiKnjigu(veza.KnjigaId);
            if (autor is null || knjiga is null) continue;

            knjiga.Autori.Add(autor);
            autor.Knjige.Add(knjiga);
        }
    }

    public void PoveziKupce(List<Kupovina> kupovine)
    {
        foreach (Kupovina kupovina in kupovine)
        {
            Knjiga? knjiga = NadjiKnjigu(kupovina.KnjigaId);
            if (knjiga is null || kupovina.Posetilac is null) continue;

            if(!knjiga.Kupci.Contains(kupovina.Posetilac))
            {
                knjiga.Kupci.Add(kupovina.Posetilac);
            }
        }
    }

    private void SacuvajVeze()
    {
        _veze.Clear();
        foreach (Knjiga knjiga in _knjige)
        {
            foreach (Autor autor in knjiga.Autori)
            {
                _veze.Add(new AutorKnjiga(autor.Id, knjiga.Id));
            }
        }

        _vezeStorage.Save(_veze);
    }
    private int GenerateId(){
        if (_knjige.Count == 0) return 1;
        return _knjige.Max(k => k.Id) + 1;
    }

    public Knjiga DodajKnjigu(Knjiga knjiga){
        knjiga.Id = GenerateId();
        _knjige.Add(knjiga);
        foreach (Autor autor in knjiga.Autori) autor.Knjige.Add(knjiga);
        _storage.Save(_knjige);
        SacuvajVeze();
        return knjiga;
    }

    public Knjiga? IzmeniKnjigu(Knjiga knjiga)
    {
        Knjiga? stara = NadjiKnjigu(knjiga.Id);
        if (stara is null) return null;

        foreach (Autor autor in stara.Autori)
        {
            autor.Knjige.Remove(stara);
        }

        stara.Isbn = knjiga.Isbn;
        stara.Naziv = knjiga.Naziv;
        stara.Zanr = knjiga.Zanr;
        stara.GodinaIzdavanja = knjiga.GodinaIzdavanja;
        stara.Cena = knjiga.Cena;
        stara.BrojStrana = knjiga.BrojStrana;
        stara.IzdavacId = knjiga.IzdavacId;

        stara.Autori = knjiga.Autori;
        foreach (Autor autor in stara.Autori)
        {
            autor.Knjige.Add(stara);
        }

        _storage.Save(_knjige);
        SacuvajVeze();
        return stara;
    }

    public Knjiga? ObrisiKnjigu(int id)
    {
        Knjiga? knjiga = NadjiKnjigu(id);
        if (knjiga is null) return null;

        foreach (Autor autor in knjiga.Autori)
        {
            autor.Knjige.Remove(knjiga);
        }

        _knjige.Remove(knjiga);
        _storage.Save(_knjige);
        SacuvajVeze();
        return knjiga;
    }

    public Knjiga? NadjiKnjigu(int id)
    {
        return _knjige.Find(k => k.Id == id);
    }

    public List<Knjiga> SveKnjige()
    {
        return _knjige;
    }

    public void UkloniAutoraSaSvihKnjiga(Autor autor)
    {
        foreach (Knjiga knjiga in autor.Knjige)
        {
            knjiga.Autori.Remove(autor);
        }
        SacuvajVeze();
    }
}