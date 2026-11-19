using SajamKnjiga.Core.Serialization;
using SajamKnjiga.Core.Utils;

namespace SajamKnjiga.Core.Model;

public class Posetilac : ISerializable
{

    public int Id { get; set; }
    public string Ime { get; set; } = string.Empty;
    public string Prezime { get; set; } = string.Empty;
    public DateTime DatumRodjenja { get; set; }
    public Adresa Adresa { get; set; } = new Adresa();
    public string Telefon { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string BrojClanskeKarte { get; set; } = string.Empty;
    public int TrenutnaGodinaClanstva { get; set; }
    public StatusPosetioca Status { get; set; }
    public Posetilac() { }
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
            Telefon,
            Email,
            BrojClanskeKarte,
            TrenutnaGodinaClanstva.ToString(),
            Status.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ime = values[1];
        Prezime = values[2];
        DatumRodjenja = CsvFormat.ParsirajDatum(values[3]);
        Adresa = new Adresa
        {
            Ulica = values[4],
            Broj = values[5],
            Grad = values[6],
            Drzava = values[7]
        };
        Telefon = values[8];
        Email = values[9];

        BrojClanskeKarte = values[10];
        TrenutnaGodinaClanstva = int.Parse(values[11]);
        Status = Enum.Parse<StatusPosetioca>(values[12]);
    }

    public override string ToString()
    {
        return $"ID: {Id}|Karta: {BrojClanskeKarte}|Ime: {Ime} {Prezime}|" +
              $"Datum rođenja: {DatumRodjenja:dd.MM.yyyy}|Adresa: {Adresa}|" +
              $"Telefon: {Telefon}|Email: {Email}|Godina članstva: {TrenutnaGodinaClanstva}|" +
              $"Status: {(Status == StatusPosetioca.V ? "VIP" : "Redovan")}";
    }
}
