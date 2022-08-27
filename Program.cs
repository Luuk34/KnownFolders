using System.Runtime.InteropServices;

/*
2022-08-27: all knwnFolders added:
            reg query "HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders" | findstr "{"
2022-08-27: Environment.SpecialFolder added to the output
*/

System.Console.WriteLine("KnownFolder:");
foreach (KnownFolder knownFolder in Enum.GetValues<KnownFolder>())
{
    try
    {
        Console.Write($"{knownFolder}=");
        Console.WriteLine(KnownFolders.GetPath(knownFolder));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"<Exception> {ex.Message}");
    }
    // Console.WriteLine();
}
System.Console.WriteLine("SpecialFolder:");
foreach (Environment.SpecialFolder folder in Enum.GetValues<Environment.SpecialFolder>())
{
    try
    {
        Console.Write($"{folder.ToString()}=");
        System.Console.WriteLine(Environment.GetFolderPath(folder));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"<Exception> {ex.Message}");
    }
}

enum KnownFolder
{
    Documents,
    Downloads,
    Music,
    Pictures,
    SavedGames,
    // ...
    Libraries,
    Contacts,
    RoamingTiles,
    Searches,
    LocalLow,
    Links

}

static class KnownFolders
{
    private static readonly Dictionary<KnownFolder, Guid> _knownFolderGuids = new()
    {
        [KnownFolder.Documents] = new("FDD39AD0-238F-46AF-ADB4-6C85480369C7"),
        [KnownFolder.Downloads] = new("374DE290-123F-4565-9164-39C4925E467B"),
        [KnownFolder.Music] = new("4BD8D571-6D19-48D3-BE97-422220080E43"),
        [KnownFolder.Pictures] = new("33E28130-4E1E-4676-835A-98395C3BC3BB"),
        [KnownFolder.SavedGames] = new("4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4"),
        [KnownFolder.Libraries] = new("1B3EA5DC-B587-4786-B4EF-BD1DC332AEAE"),
        [KnownFolder.Contacts] = new("56784854-C6CB-462B-8169-88E350ACB882"),
        [KnownFolder.RoamingTiles] = new("00BCFC5A-ED94-4E48-96A1-3F6217F21990"),
        [KnownFolder.Searches] = new("7D1D3A04-DEBB-4115-95CF-2F29DA2920DA"),
        [KnownFolder.LocalLow] = new("A520A1A4-1780-4FF6-BD18-167343C5AF16"),
        [KnownFolder.Links] = new("BFB9D5E0-C6A9-404C-B2B2-AE6DB6AF4968")
    };

    public static string GetPath(KnownFolder folder)
    {
        return Enum.IsDefined(typeof(KnownFolder), folder) ? SHGetKnownFolderPath(_knownFolderGuids[folder], 0) : "???";
    }

    [DllImport("shell32", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
    private static extern string SHGetKnownFolderPath(
        [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, nint hToken = default);
}
