
using SajamKnjiga.Core.Serialization;
using SajamKnjiga.Core.Utils;

namespace SajamKnjiga.Core.Model;

public class Kupovina : ISerializable
{
    public int Id { get; set; }
    public int PosetilacId { get; set; }
    public int KnjigaId { get; set; }
    public DateTime DatumKupovine { get; set; }
    public int Ocena { get; set; }
    public string Komentar { get; set; } = string.Empty;

    public Posetilac? Posetilac { get; set; }
    public Knjiga? Knjiga { get; set; }

    public Kupovina() { }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            PosetilacId.ToString(),
            KnjigaId.ToString(),
            CsvFormat.Datum(DatumKupovine),
            Ocena.ToString(),
            Komentar
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        PosetilacId = int.Parse(values[1]);
        KnjigaId = int.Parse(values[2]);
        DatumKupovine = CsvFormat.ParsirajDatum(values[3]);
        Ocena = int.Parse(values[4]);
        Komentar = values[5];
    }

    public override string ToString()
    {
        return $"Kupovina [Id={Id}, PosetilacId={PosetilacId}, KnjigaId={KnjigaId}, DatumKupovine={DatumKupovine}, Ocena={Ocena}, Komentar={Komentar}]";
    }

}
