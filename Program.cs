using Mozijegykezelo1.Document;
using Mozijegykezelo1.Model;
using System.Data;

internal class Program
{
    public static FileIO.ReadFromFile reader = new FileIO.ReadFromFile();
    public static List<List<string>> jegyAdatok = new();
    public static List<Jegy> jegyek;

    public static readonly string connectionString = "Server=localhost;Database=mozi;User=root;";
    public static DataTable filmAdatok = new DataTable();
    public static List<Film> filmLista = new List<Film>();
    static void Main(string[] args)
    {
        JegyBeolvasas(ref jegyAdatok);
        JegyFeltoltes(jegyAdatok);
        Kiiras(jegyek);

        //DBCheck(connectionString);
        SelectFromTable("filmek", connectionString);
        FilmFeltoltes(filmAdatok);
        PrintDataTable(filmAdatok);

    }

    private static void PrintDataTable(DataTable filmLista)
    {
        foreach (DataColumn column in filmLista.Columns)
        {
            Console.Write(column.ColumnName + "\t");
        }
        Console.WriteLine();

        // Print rows
        foreach (DataRow row in filmLista.Rows)
        {
            foreach (var item in row.ItemArray)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
        }
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



    private static void FilmFeltoltes(DataTable filmAdatok)
    {
        foreach (DataRow f in filmAdatok.Rows)
        {
            Film film = new Film();
            film.Id = f.Field<int>(0);
            film.Cim = f.Field<string>(1);
            film.Kategoria = f.Field<string>(2);
            film.Rendezo = f.Field<string>(3);
            film.MegjelenesEve = f.Field<int>(4);
            film.Hossz = f.Field<int>(5);

            filmLista.Add(film);
        }
    }

    private static void SelectFromTable(string tableName, string connectionString)
    {
        filmAdatok = DatabaseService.GetAllData(tableName, connectionString);
        Console.WriteLine("Adatok sikeresen szinkronizálva az adatbázisból, átmeneti tárolóba");
    }

    private static void DBCheck(string connectionString)
    {
        DatabaseService.DbConnectionCheck(connectionString);
    }
}