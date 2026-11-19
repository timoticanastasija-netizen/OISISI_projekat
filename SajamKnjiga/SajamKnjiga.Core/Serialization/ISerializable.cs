
namespace SajamKnjiga.Core.Serialization;

//preuzeto sa vezbi 3

public interface ISerializable 
{
    string[] ToCSV();

    void FromCSV(string[] values);
}
