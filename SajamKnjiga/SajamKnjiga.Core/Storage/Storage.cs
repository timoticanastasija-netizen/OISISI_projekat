using System.Text;
using SajamKnjiga.Core.Serialization;
using SajamKnjiga.Core.Utils;

namespace SajamKnjiga.Core.Storage;

public class Storage<T> where T : ISerializable, new()
{
    private readonly string _fileName;
    private readonly Serializer<T> _serializer = new();

    public Storage(string fileName)
    {
        _fileName = Putanje.PutanjaDoFajla(fileName);
    }

    public List<T> Load()
    {
        // ukoliko Data folder ili fajl ne postoje, napravi ih
        Directory.CreateDirectory(Putanje.DataFolder);
        if (!File.Exists(_fileName))
        {
            FileStream fs = File.Create(_fileName);
            fs.Close();
        }

        IEnumerable<string> lines = File.ReadLines(_fileName, Encoding.UTF8);
        return _serializer.FromCSV(lines);
    }

    public void Save(List<T> objects)
    {
        string serializedObjects = _serializer.ToCSV(objects);
        using StreamWriter streamWriter = new StreamWriter(_fileName, false, new UTF8Encoding(false));
        streamWriter.Write(serializedObjects);
    }
}
