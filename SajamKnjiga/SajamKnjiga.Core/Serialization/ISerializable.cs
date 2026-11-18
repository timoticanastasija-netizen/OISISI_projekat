
namespace SajamKnjiga.Core.Serialization;

public interface ISerializable // sto ne class
{
    string[] toCSV();

    void fromCSV(string[] values);
}
