using SajamKnjiga.Core.Model;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO;
//Nakon izmene cuva se u fajl, a nakon brisanja se uklanja autor sa svih knjiga i uklanja se sef iz izdavaca, pa tek onda iz liste.
    public class AutorDAO
    {
        private readonly List<Autor> _autori;
        private readonly Storage<Autor> _storage;
        private readonly KnjigaDAO _knjigaDAO;
        private readonly IzdavacDAO _izdavacDAO;

        public AutorDAO(KnjigaDAO knjigaDAO, IzdavacDAO izdavacDAO)
        {
            _knjigaDAO = knjigaDAO;
            _izdavacDAO = izdavacDAO;
            _storage = new Storage<Autor>("autori.txt");
            _autori = _storage.Load();
        }

        private int GenerateId()
        {
            if (_autori.Count == 0)
                return 1;
            return _autori.Max(a => a.Id) + 1;
        }

        public Autor DodajAutora(Autor autor)
        {
            autor.Id = GenerateId();
            _autori.Add(autor);
            _storage.Save(_autori);
            return autor;
        }

        public Autor? IzmeniAutora(Autor autor)
        {
            Autor? stari = NadjiAutoraPoId(autor.Id);
            if (stari is null)
                return null;

            stari.Ime = autor.Ime;
            stari.Prezime = autor.Prezime;
            stari.DatumRodjenja = autor.DatumRodjenja;
            stari.Adresa = autor.Adresa;
            stari.Email = autor.Email;
            stari.Telefon = autor.Telefon;
            stari.BrojLicneKarte = autor.BrojLicneKarte;
            stari.GodineIskustva = autor.GodineIskustva;

            _storage.Save(_autori);
            return stari;

        }

        public Autor? NadjiAutoraPoId(int id)
        {
            return _autori.Find(a => a.Id == id);
        }
        
        public Autor? ObrisiAutora(int id)
        {
            Autor? autor = NadjiAutoraPoId(id);
            if (autor is null)
                return null;

            _knjigaDAO.UkloniAutoraSaSvihKnjiga(autor);
            _izdavacDAO.UkloniSefa(autor.Id);
            _autori.Remove(autor);
            _storage.Save(_autori);
            return autor;
        }
        public List<Autor> NadjiSveAutore()
        {
            return _autori;
        }
    }