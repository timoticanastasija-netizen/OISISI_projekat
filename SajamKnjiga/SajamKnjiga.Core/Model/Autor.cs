using SajamKnjiga.Core.Serialization;
using SajamKnjiga.Core.Utils;

namespace SajamKnjiga.Core.Model;

public class Autor : ISerializable
{
    public int Id { get; set; }
    public string Ime { get; set; } = "";
    public string Prezime { get; set; } = "";
    public DateTime DatumRodjenja { get; set; }
    public Adresa Adresa { get; set; } = new Adresa();
    public string Email { get; set; } = "";
    public string Telefon { get; set; } = "";
    public string BrojLicneKarte { get; set; } = "";
    public int GodineIskustva { get; set; }
    
      public List<Knjiga> Knjige { get; set; } = new List<Knjiga>();

    public Autor()
    {
    }


    public string[] ToCSV()
    {
        string[] csvValues = 
        {
            Id.ToString(),
            Ime,
            Prezime,
            CsvFormat.Datum(DatumRodjenja),
            Adresa.Ulica,
            Adresa.Broj,
            Adresa.Grad,
            Adresa.Drzava,
            Email,
            Telefon,
            BrojLicneKarte,
            GodineIskustva.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ime = values[1];
        Prezime = values[2];
        DatumRodjenja = CsvFormat.ParsirajDatum(values[3]);
        Adresa = new Adresa(values[4], values[5], values[6], values[7]);
        Email = values[8];
        Telefon = values[9];
        BrojLicneKarte = values[10];
        GodineIskustva = int.Parse(values[11]);
    }

    public override string ToString()
    {
        return $"ID: {Id}|Ime: {Ime} {Prezime}|Datum rođenja: {DatumRodjenja:dd.MM.yyyy}|Adresa: {Adresa}|"+
        $"Email: {Email}| Telefon: {Telefon}|Broj lične karte: {BrojLicneKarte}|Godine iskustva: {GodineIskustva}|"+
        $"Knjige: {string.Join(", ", Knjige.Select(k => k.Naziv))}";
    }
}
