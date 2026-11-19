using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Model;

public class AutorKnjiga:ISerializable
{
    public int AutorId { get; set; }
    public int KnjigaId { get; set; }

    public AutorKnjiga()
    {
    }

    public AutorKnjiga(int autorId, int knjigaId)
    {
        AutorId = autorId;
        KnjigaId = knjigaId;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            AutorId.ToString(),
            KnjigaId.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        AutorId = int.Parse(values[1]);
        KnjigaId = int.Parse(values[2]);
    }
}