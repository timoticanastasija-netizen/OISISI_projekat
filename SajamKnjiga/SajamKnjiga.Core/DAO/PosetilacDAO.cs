using SajamKnjiga.Core.Model;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO;

//po uzoru na vezbe 3 CRUDExample - DAO/VehiclesDAO.cs

public class PosetilacDAO
{

    private readonly List<Posetilac> _posetioci;
    private readonly Storage<Posetilac> _storage;

    public PosetilacDAO()
    {
        _storage = new Storage<Posetilac>("posetioci.txt");
        _posetioci = _storage.Load();
    }

    private int GenerateId()
    {
        if (_posetioci.Count == 0)
            return 1;
        return _posetioci.Max(p => p.Id) + 1;
    }

    public Posetilac DodajPosetioca(Posetilac posetilac)
    {
        posetilac.Id = GenerateId();
        _posetioci.Add(posetilac);
        _storage.Save(_posetioci);
        return posetilac;
    }

    public Posetilac? IzmeniPosetioca(Posetilac posetilac)
    {
        Posetilac? stari = NadjiPosetiocaPoId(posetilac.Id);
        if (stari is null)
            return null;
        stari.Ime = posetilac.Ime;
        stari.Prezime = posetilac.Prezime;
        stari.DatumRodjenja = posetilac.DatumRodjenja;
        stari.Adresa = posetilac.Adresa;
        stari.Telefon = posetilac.Telefon;
        stari.Email = posetilac.Email;
        stari.BrojClanskeKarte = posetilac.BrojClanskeKarte;
        stari.TrenutnaGodinaClanstva = posetilac.TrenutnaGodinaClanstva;
        stari.Status = posetilac.Status;
        _storage.Save(_posetioci);
        return stari;
    }

    public Posetilac? NadjiPosetiocaPoId(int id)
    {
        return _posetioci.Find(p => p.Id == id);
    }

    public Posetilac? ObrisiPosetioca(int id)
    {
        Posetilac? posetilac = NadjiPosetiocaPoId(id);
        if (posetilac is null)
            return null;
        _posetioci.Remove(posetilac);
        _storage.Save(_posetioci);
        return posetilac;
    }

    public List<Posetilac> NadjiSvePosetioce()
    {
        return _posetioci;

    }


}
