namespace TextSearch;

public class FileSearchResult
{
    public string FilePath { get; set; } = string.Empty;
    public List<string> Lines { get; set; } = [];
    public int Occurrences { get; set; }
}

public class FileSearch
{
    private static readonly object _lock = new();

    public static void FindTextInDirectoryAndOutputToFile(string dirPath, string searchText, string outputPath)
    {
        var results = FindTextInDirectory(dirPath, searchText);
        using var writer = new StreamWriter(outputPath);
        writer.WriteLine($"Search Text: {searchText}");
        writer.WriteLine($"Total Files: {results.Count}");
        writer.WriteLine($"Total Occurrences: {results.Sum(r => r.Occurrences)}");
        writer.WriteLine($"Total Lines: {results.Sum(r => r.Lines.Count)}");

        foreach (var result in results)
        {
            writer.WriteLine($"File: {result.FilePath}");
            writer.WriteLine($"Occurrences: {result.Occurrences}");
            foreach (var line in result.Lines)
            {
                writer.WriteLine(line);
            }
            writer.WriteLine();
            writer.WriteLine();
        }
    }

    public static List<FileSearchResult> FindTextInDirectory(string dirPath, string searchText)
    {
        var files = Directory.GetFiles(dirPath, "*.*", SearchOption.TopDirectoryOnly);
        var results = new List<FileSearchResult>();

        Parallel.ForEach(files, (file) =>
        {
            var result = FindText(file, searchText);
            lock (_lock)
            {
                results.Add(result);
            }
        });
        return results;
    }

    private static FileSearchResult FindText(string file, string searchText)
    {
        using var reader = new StreamReader(file);

        string? line;
        int occurrences = 0;
        var lines = new List<string>();

        while ((line = reader.ReadLine()) != null)
        {
            int occurrence = OccurrencesOf(line, searchText);
            if (occurrence > 0)
            {
                lines.Add(line);
                occurrences += occurrence;
            }
        }
        return new FileSearchResult
        {
            FilePath = file,
            Occurrences = occurrences,
            Lines = lines,
        };
    }

    private static int OccurrencesOf(string text, string find)
    {
        if (string.IsNullOrEmpty(find)) return 0;

        int count = 0;
        int i = 0;
        while ((i = text.IndexOf(find, i, StringComparison.Ordinal)) != -1)
        {
            i += find.Length;
            count++;
        }
        return count;
    }
}
