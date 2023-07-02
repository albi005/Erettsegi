string[] lines = File.ReadAllLines("lista.txt");
List<Episode> episodes = new List<Episode>();
for (int i = 0; i < lines.Length / 5; i++)
{
    episodes.Add(new Episode
    {
        Date = lines[i * 5] == "NI" ? null : DateTime.Parse(lines[i * 5]),
        Title = lines[i * 5 + 1],
        Season = int.Parse(lines[i * 5 + 2].Split('x')[0]),
        Number = int.Parse(lines[i * 5 + 2].Split('x')[1]),
        Length = int.Parse(lines[i * 5 + 3]),
        IsSeen = lines[i * 5 + 4] == "1"
    });
}

Console.WriteLine("2. feladat");
Console.WriteLine($"A listában {episodes.Count(x => x.Date != null)} db vetítési dátummal rendelkező epizód van.");

Console.WriteLine("3. feladat");
Console.WriteLine(
    $"A listában lévő epizódok {(float)episodes.Count(x => x.IsSeen) / episodes.Count * 100:0.00}%-át látta.");

Console.WriteLine("4. feladat");
TimeSpan watchTime = episodes
    .Where(e => e.IsSeen)
    .Aggregate(new TimeSpan(), (sum, e) => sum.Add(TimeSpan.FromMinutes(e.Length)));
Console.WriteLine(
    $"Sorozatnézéssel {watchTime.Days} napot {watchTime.Hours} órát és {watchTime.Minutes} percet töltött.");

Console.WriteLine("5. feladat");
Console.Write("Adjon meg egy dátumot! Dátum= ");
DateTime date = DateTime.Parse(Console.ReadLine());
foreach (Episode episode in episodes.Where(e => !e.IsSeen && e.Date <= date))
{
    Console.WriteLine($"{episode.Season}x{episode.Number:00}\t{episode.Title}");
}

Console.WriteLine("7. feladat");
Console.Write("Adja meg a hét egy napját (például cs)! Nap= ");
string nap = Console.ReadLine();
List<string> titles = episodes
    .Where(x => x.Date != null && Hetnapja(x.Date.Value.Year, x.Date.Value.Month, x.Date.Value.Day) == nap)
    .Select(e => e.Title)
    .Distinct()
    .ToList();
if (titles.Count == 0) Console.WriteLine("Az adott napon nem kerül adásba sorozat.");
else
    foreach (string title in titles)
        Console.WriteLine(title);

IEnumerable<string> data = episodes
    .GroupBy(x => x.Title)
    .Select(episodes => $"{episodes.Key} {episodes.Sum(e => e.Length)} {episodes.Count()}");
File.WriteAllLines("summa.txt", data);

string Hetnapja(int ev, int ho, int nap)
{
    string[] napok = { "v", "h", "k", "sze", "cs", "p", "szo" };
    int[] honapok = { 0, 3, 2, 5, 0, 3, 5, 1, 4, 6, 2, 4 };
    if (ho < 3) ev--;
    return napok[(ev + ev / 4 - ev / 100 + ev / 400 + honapok[ho - 1] + nap) % 7];
}

public class Episode
{
    public DateTime? Date { get; set; }
    public string Title { get; set; }
    public int Season { get; set; }
    public int Number { get; set; }
    public int Length { get; set; }
    public bool IsSeen { get; set; }
}