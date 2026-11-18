namespace SajamKnjiga.Core.Utils;

public static class Putanje
{
    private static readonly char Sep = Path.DirectorySeparatorChar;
    private static string? _dataFolder;


    public static string DataFolder => _dataFolder ??= PronadjiDataFolder();

    public static string PutanjaDoFajla(string imeFajla)
    {
        return DataFolder + Sep + imeFajla;
    }

    private static string PronadjiDataFolder()
    {
        DirectoryInfo? folder = new DirectoryInfo(AppContext.BaseDirectory);

        while (folder != null)
        {
            if (folder.GetFiles("*.sln").Length > 0 || folder.GetFiles("*.slnx").Length > 0)
                return folder.FullName + Sep + "Data";

            folder = folder.Parent;
        }

        return AppContext.BaseDirectory.TrimEnd(Sep) + Sep + "Data";
    }
}
