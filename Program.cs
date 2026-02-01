using Mozijegykezelo1.Model;

internal class Program
{
    public static FileIO.ReadFromFile reader = new FileIO.ReadFromFile();
    public static List<List<string>> jegyAdatok = new();
    public static List<Jegy> jegyek;
    static void Main(string[] args)
    {
        JegyBeolvasas(ref jegyAdatok);
        JegyFeltoltes(jegyAdatok);
        Kiiras(jegyek);
    }

    private static void JegyBeolvasas(ref List<List<string>> jegyAdatok)
    {
        jegyAdatok = reader.FileRead("Jegyek.csv", 5, ',', true);
    }
    private static void JegyFeltoltes(List<List<string>> jegyAdatok)
    {
        List<List<string>> jegyLista = jegyAdatok;
        jegyek = new List<Jegy>();

        foreach (var item in jegyLista)
        {
            string vevoNev = item[0];
            string filmCim = item[1];
            string vetitesIdopont = item[2];
            string szekSor = item[3];
            int szekSzam = int.Parse(item[4]);

            Jegy j = new Jegy(vevoNev, filmCim, vetitesIdopont, szekSor, szekSzam);
            jegyek.Add(j);
        }
    }
    private static void Kiiras(List<Jegy> jegyek)
    {
        foreach (var jegy in jegyek)
        {
            Console.WriteLine(jegy.ToString());
        }
    }
}