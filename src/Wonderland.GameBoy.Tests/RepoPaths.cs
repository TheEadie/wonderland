namespace Wonderland.GameBoy.Tests;

internal static class RepoPaths
{
    public static string? GameBoyRomPath(params string[] segments)
    {
        var repoRoot = FindRepoRoot();
        if (repoRoot is null)
        {
            return null;
        }

        var parts = new string[segments.Length + 3];
        parts[0] = repoRoot;
        parts[1] = "roms";
        parts[2] = "gameboy";
        Array.Copy(segments, 0, parts, 3, segments.Length);
        return Path.Combine(parts);
    }

    private static string? FindRepoRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "wonderland.sln")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        return null;
    }
}
