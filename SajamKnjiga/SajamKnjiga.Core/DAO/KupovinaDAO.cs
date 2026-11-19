
using SajamKnjiga.Core.Model;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO;

//po uzoru na vezbe 3 CRUDExample - DAO/VehiclesDAO.cs

public class KupovinaDAO
{
    private readonly List<Kupovina> _kupovine;
    private readonly Storage<Kupovina> _storage;
    public KupovinaDAO()
    {
        _storage = new Storage<Kupovina>("kupovine.txt");
        _kupovine = _storage.Load();
    }

    public void PoveziKupovineSaPosetiocima(List<Posetilac> posetioci, List<Knjiga> knjige)
    {
        foreach (Kupovina kupovina in _kupovine)
        {
            kupovina.Posetilac = posetioci.Find(p => p.Id == kupovina.PosetilacId);
            kupovina.Knjiga = knjige.Find(k => k.Id == kupovina.KnjigaId);
            kupovina.Posetilac?.Kupovine.Add(kupovina);
        }
    }
    private int GenerateId()
    {
        if (_kupovine.Count == 0)
            return 1;
        return _kupovine.Max(k => k.Id) + 1;
    }
    public Kupovina DodajKupovinu(Kupovina kupovina)
    {
        kupovina.Id = GenerateId();
        _kupovine.Add(kupovina);
        kupovina.Posetilac?.Kupovine.Add(kupovina);
        _storage.Save(_kupovine);
        return kupovina;
    }
    public Kupovina? IzmeniKupovinu(Kupovina kupovina)
    {
        Kupovina? stara = NadjiKupovinuPoId(kupovina.Id);
        if (stara is null)
            return null;
        stara.DatumKupovine = kupovina.DatumKupovine;
        stara.Ocena = kupovina.Ocena;
        stara.Komentar = kupovina.Komentar;
        _storage.Save(_kupovine);
        return stara;
    }

    public Kupovina? ObrisiKupovinu(int id)
    {
        Kupovina? kupovina = NadjiKupovinuPoId(id);
        if (kupovina is null)
            return null;
        kupovina.Posetilac?.Kupovine.Remove(kupovina);
        _kupovine.Remove(kupovina);
        _storage.Save(_kupovine);
        return kupovina;
    }

    public void ObrisiKupovinePosetioca(int posetilacId)
    {
        _kupovine.RemoveAll(k => k.PosetilacId == posetilacId);
        _storage.Save(_kupovine);
    }

    public void ObrisiKupovineKnjige(int knjigaId)
    {
        List<Kupovina> zaBrisanje = _kupovine.FindAll(k => k.KnjigaId == knjigaId);
        foreach (Kupovina kupovina in zaBrisanje)
        {
            kupovina.Posetilac?.Kupovine.Remove(kupovina);
            _kupovine.Remove(kupovina);
        }
        _storage.Save(_kupovine);
    }
    public Kupovina? NadjiKupovinuPoId(int id)
    {
        return _kupovine.Find(k => k.Id == id);

    }

    public List<Kupovina> NadjiSveKupovine()
    {
        return _kupovine;
    }

}
