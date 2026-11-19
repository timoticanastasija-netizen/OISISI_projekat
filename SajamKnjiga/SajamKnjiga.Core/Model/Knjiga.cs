using SajamKnjiga.Core.Serialization;
using SajamKnjiga.Core.Utils;

namespace SajamKnjiga.Core.Model;

public class Knjiga : ISerializable
{
    public int Id { get; set; }
    public string Isbn { get; set; } = "";
    public string Naziv { get; set; } = "";
    public string Zanr { get; set; } = "";
    public int GodinaIzdavanja { get; set; }
    public decimal Cena { get; set; }
    public int BrojStrana { get; set; }

    public int? IzdavacId { get; set; } // null ako knjiga nema izdavača

    public Knjiga()
    {
    }

    public List<Autor> Autori { get; set; } = new List<Autor>();

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Isbn,
            Naziv,
            Zanr,
            GodinaIzdavanja.ToString(),
            CsvFormat.Decimalni(Cena),
            BrojStrana.ToString(),
            CsvFormat.OpcioniId(IzdavacId)
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Isbn = values[1];
        Naziv = values[2];
        Zanr = values[3];
        GodinaIzdavanja = int.Parse(values[4]);
        Cena = CsvFormat.ParsirajDecimalni(values[5]);
        BrojStrana = int.Parse(values[6]);
        IzdavacId = CsvFormat.ParsirajOpcioniId(values[7]);
    }

    public override string ToString()
    {
        return $"ID: {Id}|ISBN: {Isbn}|Naziv: {Naziv}|Žanr: {Zanr}|Godina izdavanja: {GodinaIzdavanja}|Cena: {Cena:C}|Broj strana: {BrojStrana} .str |Autori: {string.Join(", ", Autori.Select(a => $"{a.Ime} {a.Prezime}"))}|Izdavač ID: {(IzdavacId.HasValue ? IzdavacId.Value.ToString() : "Nema izdavača")}";
    }
}