

using SajamKnjiga.Core.Model;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO;

//po uzoru na vezbe 3 CRUDExample - DAO/VehiclesDAO.cs

public class IzdavacDAO
{
    private readonly List<Izdavac> _izdavaci;
    private readonly Storage<Izdavac> _storage;

    public IzdavacDAO()
    {
        _storage = new Storage<Izdavac>("izdavaci.txt");
        _izdavaci = _storage.Load();
    }

    public void Povezi(List<Autor> autori, List<Knjiga> knjige)
    {
        foreach (Izdavac izdavac in _izdavaci)
        {
            izdavac.Sef = autori.Find(a => a.Id == izdavac.SefId);
            izdavac.Knjige = knjige.FindAll(k => k.IzdavacId == izdavac.Id);
        }
    }

    private int GenerateId()
    {
        if (_izdavaci.Count == 0)
            return 1;
        return _izdavaci.Max(i => i.Id) + 1;
    }

    public Izdavac DodajIzdavaca(Izdavac izdavac)
    {
        izdavac.Id = GenerateId();
        _izdavaci.Add(izdavac);
        _storage.Save(_izdavaci);
        return izdavac;
    }

    public Izdavac? NadjiIzdavacaPoId(int id)
    {
        return _izdavaci.Find(i => i.Id == id);
    }

    public Izdavac? IzmeniIzdavaca(Izdavac izdavac)
    {
        Izdavac? stari = NadjiIzdavacaPoId(izdavac.Id);
        if (stari is null)
            return null;
        stari.Sifra = izdavac.Sifra;
        stari.Naziv = izdavac.Naziv;
        stari.SefId = izdavac.SefId;
        stari.Sef = izdavac.Sef;
        _storage.Save(_izdavaci);
        return stari;
    }

    public Izdavac? ObrisiIzdavaca(int id)
    {
        Izdavac? izdavac = NadjiIzdavacaPoId(id);
        if (izdavac is null || izdavac.Knjige.Count > 0)
            return null;
        _izdavaci.Remove(izdavac);
        _storage.Save(_izdavaci);
        return izdavac;
    }

    public void UkloniSefa(int sefId)
    {
        foreach (Izdavac izdavac in _izdavaci)
        {
            if (izdavac.SefId == sefId)
            {
                izdavac.SefId = null;
                izdavac.Sef = null;
            }
        }
        _storage.Save(_izdavaci);
    }

    public List<Izdavac> NadjiSveIzdavace()
    {
        return _izdavaci;

    }

}
