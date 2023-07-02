using System.Text;

var lines = File.ReadAllLines("melyseg.txt");
Console.WriteLine($"1. feladat\nA fájl adatainak száma: {lines.Length}\n");

Console.Write("2. feladat\nAdjon meg egy távolságértéket! ");
int chosenDistance = int.Parse(Console.ReadLine());
string chosenLine = lines[chosenDistance - 1];
Console.WriteLine($"Ezen a helyen a felszín {chosenLine} méter mélyen van.\n");

Console.WriteLine($"3. feladat\nAz érintetlen terület aránya {(double)lines.Where(x => x == "0").Count() / lines.Length * 100:0.00}%.\n");

int godorCount = 0;
bool prevGodor = false;
StringBuilder builder = new();
foreach (string line in lines)
{
    bool isGodor = line != "0";
    if (isGodor)
    {
        builder.Append(line + " ");
    }
    else if (prevGodor != isGodor)
    {
        builder.AppendLine();
        godorCount++;
    }
    prevGodor = isGodor;
}
File.WriteAllText("godrok.txt", builder.ToString());

Console.WriteLine($"5. feladat\nA gödrök száma: {godorCount}\n");

Console.WriteLine("6. feladat");
if (chosenLine == "0")
{
    Console.WriteLine("Az adott helyen nincs gödör.\n");
    return;
}

int first = 0;
int i = 0;
for (; i < chosenDistance - 1; i++)
{
    if (lines[i] == "0") first = i + 1;
}

int last = 0;
while (true)
{
    i++;
    if (lines[i] == "0")
    {
        last = i - 1;
        break;
    }
}

Console.WriteLine("a)");
Console.WriteLine($"A gödör kezdete: {first + 1} méter, a gödör vége: {last + 1} méter.");

Console.WriteLine("b)");
i = first;
bool isMonoton = true;
bool isDown = true;
int max = 0;
long sum = 0;
while (i <= last)
{
    int prev = int.Parse(lines[i - 1]);
    int curr = int.Parse(lines[i]);
    if (curr > max) max = curr;
    sum += curr;
    if (isDown)
    {
        if (curr < prev) isDown = false;
    }
    else
    {
        if (prev > curr) isMonoton = false;
    }
    i++;
}
Console.WriteLine(isMonoton
    ? "Folyamatosan mélyül."
    : "Nem mélyül folyamatosan.");

Console.WriteLine($"c)\nA legnagyobb mélysége {max} méter.");

Console.WriteLine($"d)\nA térfogata {sum * 10} m^3.");

Console.WriteLine($"e)\nA vízmennyiség {sum * 10 - (last - first + 1) * 10} m^3.");