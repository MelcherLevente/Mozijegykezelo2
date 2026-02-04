using Mozijegykezelo1.Document;
using Mozijegykezelo1.Model;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;


internal class Program
{
    public static FileIO.ReadFromFile reader = new FileIO.ReadFromFile();
    public static FileIO.WriteToFile writer = new FileIO.WriteToFile();
    public static List<List<string>> jegyAdatok = new();
    public static List<Jegy> jegyek;

    public static readonly string connectionString = "Server=localhost;Database=mozi;User=root;";
    public static DataTable filmAdatok = new DataTable();
    public static List<Film> filmLista = new List<Film>();
    public static List<string> kategoriak = new List<string>();
    
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.Clear();
        Console.WriteLine("   xxxxxxx         xx xxx                                        \r\n  xxxxxxxxx        xxxxxxxx                                      \r\n    xxx  xx        xxx xxx                                       \r\n    xxxx xxxxxx   xx    xxx                                      \r\n  xxxxxxx   xxx  xxx    xxxx                                  xxx\r\n  xxxxxx     xxxxxxx    xxxxx      xxx                        xxx\r\n xxxxxx       xxx       xxx    xxxxx  xxxxx  xxxxxxxx            \r\n  xxx                   xxx   xxx        xx  xxxxxxxxxxx      xx \r\n xxxx                   xx   xxx xx  x  xx         xxxx      xxxx\r\n  xxxx                  xx   xx         xx        xxx        xxxx\r\n  xxx                   xxx  xx  x  x   xx      xxxx         xxxx\r\n xxx                    xx   x   xxx  xxx     xxx            xxxx\r\nxxxx                    xx   xx      xx      xxxx            xxxx\r\n xxx                    xxx  xxxxxxxxxx     xxxxxxxxxxxxx     xx ");
        Console.WriteLine("\n");

        JegyBeolvasas(ref jegyAdatok);
        JegyFeltoltes(jegyAdatok);
        //Kiiras(jegyek);

        //DBCheck(connectionString);
        SelectFromTable("filmek", connectionString);
        FilmFeltoltes(filmAdatok);
        //PrintDataTable(filmAdatok);

        KategoriaLista(kategoriak);
        Jegyvasarlas(jegyek);


        Top5();
        LegTobbFilm();




        MilyenHosszuFilm();
        Elkoszones();
        Console.ReadKey();

       



}

    private static void Elkoszones()
    {
        string text = "----Köszönjük a figyelmet!----";
        int windowWidth = Console.WindowWidth;
        int left = (windowWidth - text.Length) / 2;

        Console.SetCursorPosition(left, Console.CursorTop);
        Console.WriteLine(text);
    }

    private static void LegTobbFilm()
    {
        Console.WriteLine("\n--- Rendező, aki a legtöbb filmet rendezte ---");

        var top = filmLista
            .Where(f => !string.IsNullOrWhiteSpace(f.Rendezo))
            .GroupBy(f => f.Rendezo)
            .Select(g => new { Rendezo = g.Key, FilmDb = g.Count() })
            .OrderByDescending(x => x.FilmDb)
            .FirstOrDefault();

        if (top == null)
        {
            Console.WriteLine("Nincs erről a rendezőről adat az adatbázisban.");
            return;
        }

        Console.WriteLine($"{top.Rendezo} - {top.FilmDb} db film");

        
        Console.WriteLine("Filmek tőle:");
        foreach (var f in filmLista.Where(f => f.Rendezo == top.Rendezo))
            Console.WriteLine($"\t- {f.Cim} ({f.MegjelenesEve})");
    }

    private static void Top5()
    {
        Console.WriteLine("\n--- TOP 5 film (legtöbb eladott jegy) ---");

        var top5 = jegyek
            .GroupBy(j => j.FilmCim)
            .Select(g => new { FilmCim = g.Key, JegyekSzama = g.Count() })
            .OrderByDescending(x => x.JegyekSzama)
            .Take(5)
            .ToList();

        int helyezes = 1;
        foreach (var item in top5)
        {
            Console.WriteLine($"{helyezes}.\t {item.FilmCim} - {item.JegyekSzama} db jegy");
            helyezes++;
        }
    }

    private static void MilyenHosszuFilm()
    {
        Console.WriteLine("Hány órás filmet szeretnél nézni? (1-3)");
        int ora = Convert.ToInt32(Console.ReadLine());
        while (ora<1 || ora>3)
        {
            Console.WriteLine("Hány órás filmet szeretnél nézni? (1-3)");
            ora = Convert.ToInt32(Console.ReadLine());
        }

        for (int i = 0; i < filmLista.Count; i++)
        {
            switch (ora)
            {
                case 1:
                    if (30<=filmLista[i].Hossz && filmLista[i].Hossz <= 90)
                    {
                        Console.WriteLine($"\t{filmLista[i].Cim} - {filmLista[i].Hossz} perc");
                    }
                    break;
                case 2:
                    if (91 <= filmLista[i].Hossz && filmLista[i].Hossz <= 150)
                    {
                        Console.WriteLine($"\t{filmLista[i].Cim} - {filmLista[i].Hossz} perc");
                    }
                    break;
                case 3:
                    if (151 <= filmLista[i].Hossz && filmLista[i].Hossz <= 250)
                    {
                        Console.WriteLine($"\t{filmLista[i].Cim} - {filmLista[i].Hossz} perc");
                    }
                    break;
            }
        }
    }

    private static string KategoriaValasztas()
    {
        Console.WriteLine("Milyen stílusú filmet keresel?");
        foreach (var k in kategoriak)
        {
            Console.WriteLine($"\t{k}");
        }

        Console.WriteLine($"Választott kategória: ");
        string valasztottKategoria = Console.ReadLine();

        while (!kategoriak.Contains(valasztottKategoria))
        {
            Console.WriteLine("Ilyen kategória nem létezik!");
            Console.WriteLine("Milyen stílusú filmet keresel?");
            valasztottKategoria = Console.ReadLine();
        }
        return valasztottKategoria;
    }

    private static void Jegyvasarlas(List<Jegy> jegyek)
    {
        string kivalasztottFilm;
        while (true)
        {
            string valasztottKategoria = KategoriaValasztas();

            List<int> sorszamokFilm = new List<int>();
            List<string> valaszthatoFilmek = new List<string>();
            int sorszamFilm = 1;

            Console.WriteLine("Ezek a filmek elérhetőek a választott kategóriában:");

            foreach (var f in filmLista)
            {
                if (f.Kategoria == valasztottKategoria)
                {
                    Console.WriteLine($"{sorszamFilm}.\t {f.Cim} ({f.MegjelenesEve}) - {f.Rendezo}");
                    valaszthatoFilmek.Add(f.Cim);
                    sorszamokFilm.Add(sorszamFilm);
                    sorszamFilm++;
                }
            }

            Console.WriteLine("Add meg a film sorszámát, amit nézni szeretnél, ha másik kategóriát választanál üss 0-ást:");
            int valasztottSorszamFilm = int.Parse(Console.ReadLine());

            if (valasztottSorszamFilm == 0)
            {
                continue;
            }

            while (!sorszamokFilm.Contains(valasztottSorszamFilm))
            {
                Console.WriteLine("Nincs ilyen sorszámú film a választott kategóriában!");
                Console.WriteLine("Add meg újra (0 = vissza):");
                valasztottSorszamFilm = int.Parse(Console.ReadLine());

                if (valasztottSorszamFilm == 0)
                {
                    Console.Clear();
                    break;
                }
            }

            if (valasztottSorszamFilm == 0)
            {
                continue;
            }
            kivalasztottFilm = valaszthatoFilmek[valasztottSorszamFilm - 1];
            Console.WriteLine($"A választott film: {kivalasztottFilm}");
            break;
        }

        Console.WriteLine("A jegyvásárláshoz add meg a neved:");
        string vevoNev = Console.ReadLine();

        List<string> vetitesek = new List<string>();
        foreach(var j in jegyek)
        {
            if (j.FilmCim == kivalasztottFilm)
            {
                if (!vetitesek.Contains(j.VetitesIdopont))
                {
                    vetitesek.Add(j.VetitesIdopont);
                }
            }
        }

        List<int> sorszamokVetites = new List<int>();
        List<string> valaszthatoVetitesek = new List<string>();
        int sorszamVetites = 1;

        Console.WriteLine($"Ezek a vetítési időpontok elérhetőek:");

        foreach (var f in jegyek)
        {
            if (f.FilmCim == kivalasztottFilm && !valaszthatoVetitesek.Contains(f.VetitesIdopont))
            {
                Console.WriteLine($"{sorszamVetites}.\t {f.VetitesIdopont}");
                valaszthatoVetitesek.Add(f.VetitesIdopont);
                sorszamokVetites.Add(sorszamVetites);
                sorszamVetites++;
            }
        }

        int valasztottSorszamVetites;
        Console.WriteLine("Melyik sorszámú vetítés felel meg?");
        while (true)
        {
            valasztottSorszamVetites = int.Parse(Console.ReadLine());
            if (sorszamokVetites.Contains(valasztottSorszamVetites))
            {
                break;
            }
            Console.WriteLine("Nincs ilyen sorszámú vetítés! Add meg újra:");
        }

        string kivalasztottVetites = valaszthatoVetitesek[valasztottSorszamVetites - 1];
        Console.WriteLine($"A választott vetítés időpontja: {kivalasztottVetites}");


        string[] valaszthatoSorok = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J"];
        int[] szekek = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20];
        List<int> valaszthatoSzekek = new List<int>();
        string valasztottSor = "";
        int valasztottSzek = 0;
        Console.WriteLine("Válaszd ki a szé" +
            "ked:");
        foreach (var j in jegyek)
        {
            if (j.FilmCim == kivalasztottFilm && j.VetitesIdopont == kivalasztottVetites)
            {
                while(!valaszthatoSorok.Contains(valasztottSor))
                {
                    Console.WriteLine("Választható sorok:");
                    foreach (var sor in valaszthatoSorok)
                    {
                        Console.WriteLine($"\t{sor}");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Választott sor:");
                    valasztottSor = Console.ReadLine();
                    break;
                }
                break;
            }
        }

        

        foreach (var szek in szekek)
        {
            bool foglalt = false;

            foreach (var j in jegyek)
            {
                if (j.FilmCim == kivalasztottFilm &&
                    j.VetitesIdopont == kivalasztottVetites &&
                    j.SzekSor == valasztottSor &&
                    j.SzekSzam == szek)
                {
                    foglalt = true;
                    break;
                }
            }

            if (!foglalt)
            {
                valaszthatoSzekek.Add(szek);
            }
        }


        do
        {
            Console.WriteLine("Választható székek:");
            foreach (var szek in valaszthatoSzekek)
                Console.WriteLine($"\t{szek}");

            Console.Write("Választott szék: ");
        }
        while (!int.TryParse(Console.ReadLine(), out valasztottSzek)
               || !valaszthatoSzekek.Contains(valasztottSzek));

        // CSV-be írás
        string fajlNev = "jegyek.csv";
        bool letezik = File.Exists(fajlNev);

        bool endsWithNewline = true;

        if (letezik && new FileInfo(fajlNev).Length > 0)
        {
            using (FileStream fs = new FileStream(fajlNev, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.Seek(-1, SeekOrigin.End);
                int lastByte = fs.ReadByte();
                endsWithNewline = lastByte == '\n' || lastByte == '\r';
            }
        }

        using (StreamWriter sw = new StreamWriter(fajlNev, append: true))
        {
            if (!endsWithNewline)
                sw.WriteLine();

            sw.WriteLine($"{vevoNev};{kivalasztottFilm};{kivalasztottVetites};{valasztottSor};{valasztottSzek}");

        }




        Console.WriteLine("Jegyed sikeresen megvásárolva! Jó szórakozást!");
    }


    

    

    private static void KategoriaLista(List<string> kategoriak)
    {
        foreach (var f in filmLista)
        {
            if (!kategoriak.Contains(f.Kategoria))
            {
                kategoriak.Add(f.Kategoria);
            }
        }
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
        jegyAdatok = reader.FileRead("Jegyek.csv", 5, ';', true);
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
        //Console.WriteLine("Adatok sikeresen szinkronizálva az adatbázisból, átmeneti tárolóba");
    }

    private static void DBCheck(string connectionString)
    {
        DatabaseService.DbConnectionCheck(connectionString);
    }
}