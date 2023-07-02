Console.WriteLine("1. feladat");
Console.Write("Adja meg a bemeneti fájl nevét! ");
string fileName = Console.ReadLine();
Console.Write("Adja meg egy sor sorszámát! ");
int rowIndex = int.Parse(Console.ReadLine()) - 1;
Console.Write("Adja meg egy oszlop sorszámát! ");
int colIndex = int.Parse(Console.ReadLine()) - 1;

var table = new List<List<byte>>(9);
var plays = new List<List<byte>>();
int i = 0;
foreach (string line in File.ReadLines(fileName))
{
    var list = i++ < 9 ? table : plays;
    list.Add(line.Split(' ').Select(x => byte.Parse(x)).ToList());
}

Console.WriteLine("3. feladat");
byte val = table[rowIndex][colIndex];
if (val == 0) Console.WriteLine("Az adott helyet még nem töltötték ki.");
else Console.WriteLine($"Az adott helyen szereplő szám: {val}");
int subTable = colIndex / 3 + 1 + 3 * (rowIndex / 3);
Console.WriteLine($"A hely a(z) {subTable} résztáblázathoz tartozik.");

Console.WriteLine("4. feladat");
int filledCount = table.SelectMany(x => x).Count(x => x == 0);
Console.WriteLine($"Az üres helyek aránya: {filledCount / 81f * 100:0.0}%");

Console.WriteLine("5. feladat");
foreach (var play in plays)
{
    int num = play[0];
    int row = play[1];
    int col = play[2];
    Console.WriteLine($"A kiválasztott sor: {row} oszlop: {col} a szám: {num}");
    if (table[row - 1][col - 1] != 0)
    {
        Console.WriteLine("A helyet már kitöltötték.");
    }
    else if (table[row - 1].Any(x => x == num))
    {
        Console.WriteLine("Az adott sorban már szerepel a szám.");
    }
    else if (table.Select(r => r[col - 1]).Any(x => x == num))
    {
        Console.WriteLine("Az adott oszlopban már szerepel a szám.");
    }
    else
    {
        bool found = false;
        int x = (row - 1) / 3;
        int y = (col - 1) / 3;
        for (i = 0; i < 9; i++)
        for (int j = 0; j < 9; j++)
            if (i / 3 == x && j / 3 == y && table[i][j] == num)
                found = true;
        if (found)
            Console.WriteLine("Az adott résztáblázatban már szerepel a szám.");
        else
            Console.WriteLine("A lépés megtehető.");
    }

    Console.WriteLine();
}