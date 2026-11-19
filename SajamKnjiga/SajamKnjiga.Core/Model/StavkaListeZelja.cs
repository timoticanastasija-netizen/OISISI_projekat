
using SajamKnjiga.Core.Serialization;


namespace SajamKnjiga.Core.Model;
public class StavkaListeZelja : ISerializable
{
    public int PosetilacId { get; set; }
    public int KnjigaId { get; set; }
    public StavkaListeZelja() { }

    public StavkaListeZelja(int posetilacId, int knjigaId)
    {
        PosetilacId = posetilacId;
        KnjigaId = knjigaId;
    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            PosetilacId.ToString(),
            KnjigaId.ToString()
        };
        return csvValues;
    }
    public void FromCSV(string[] values)
    { 
        PosetilacId = int.Parse(values[1]);
        KnjigaId = int.Parse(values[2]);
    }

}
