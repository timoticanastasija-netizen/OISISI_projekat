
using SajamKnjiga.Core.Serialization;
using SajamKnjiga.Core.Utils;
using System.Net.NetworkInformation;


namespace SajamKnjiga.Core.Model;

public class Izdavac : ISerializable
{
    public const int MinGodinaIskustvaZaSefa = 5;

    public int Id { get; set; }
    public string Sifra { get; set; } = string.Empty;
    public string Naziv { get; set; } = string.Empty;

    public int? SefId { get; set; }

    public Autor? Sef { get; set; }
    public List<Knjiga> Knjige { get; set; } = new List<Knjiga>();

    public List<Autor> Autori
    {
        get
        {
            return Knjige.SelectMany(k => k.Autori).Distinct().ToList();
        }
    }

    public Izdavac() { }

    public static bool MozeBitiSef(Autor sef)
    {
        return sef.GodineIskustva >= MinGodinaIskustvaZaSefa;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Sifra,
            Naziv,
            CsvFormat.OpcioniId(SefId)
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Sifra = values[1];
        Naziv = values[2];
        SefId = CsvFormat.ParsirajOpcioniId(values[3]);
    }

    public override string ToString()
    {
        string sef = Sef is null ? "nema" : $"{Sef.Ime} {Sef.Prezime}";
        return $"ID: {Id}|Šifra: {Sifra}|Naziv: {Naziv}|Šef: {sef}|" +
               $"Broj knjiga: {Knjige.Count}|Broj autora: {Autori.Count}";
    }

}
