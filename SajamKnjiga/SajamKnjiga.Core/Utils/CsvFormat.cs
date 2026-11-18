using System.Globalization;

namespace SajamKnjiga.Core.Utils;

public static class CsvFormat
{
    private const string FormatDatuma = "yyyy-MM-dd";

    public static string Datum(DateTime datum)
    {
        return datum.ToString(FormatDatuma, CultureInfo.InvariantCulture);
    }

    public static DateTime ParsirajDatum(string tekst)
    {
        return DateTime.ParseExact(tekst, FormatDatuma, CultureInfo.InvariantCulture);
    }

    public static string Decimalni(decimal broj)
    {
        return broj.ToString(CultureInfo.InvariantCulture);
    }

    public static decimal ParsirajDecimalni(string tekst)
    {
        return decimal.Parse(tekst, CultureInfo.InvariantCulture);
    }

    // null (npr. knjiga bez izdavača) čuvamo kao prazan string
    public static string OpcioniId(int? id)
    {
        return id?.ToString() ?? string.Empty;
    }

    public static int? ParsirajOpcioniId(string tekst)
    {
        return string.IsNullOrWhiteSpace(tekst) ? null : int.Parse(tekst);
    }
}
