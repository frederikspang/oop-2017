//20165250_Frederik_Spang_Thomsen

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System;

namespace Eksamensopgave2017 {
  class Program {
    static void Main(string[] args) {
      try {
        LoadProducts();
        LoadUsers();
        //IStregsystem stregsystem = new Stregsystem();
        //IStregsystemUI ui = new StregsystemCLI(stregsystem);
        //StregsystemController sc = new StregsystemController(ui, stregsystem);

        //ui.Start();
        foreach (User prod in User.All) {
          Console.WriteLine(prod);
        }
      } catch(FileLoadException) {
        Console.WriteLine("Kunne ikke hente indhold.");
      }
    }

    static bool LoadProducts() {
      Parallel.ForEach(File.ReadAllLines((Directory.GetCurrentDirectory() + "/Data/products.csv")), (line, _, lineNumber) => {
        string[] split = line.Split(';');
        if (split == null) {
          return;
        }
        if (split[0] == "id")
          return;

        if (int.Parse(split[0]) > 0) {
          // Loaded into Product.All by BaseModel Constructor
          // Added to global suppresion file.
          new Product(int.Parse(split[0]), split[1], double.Parse(split[2]) / 100, int.Parse(split[3]) != 0, false);
        }
      });
      return true;
    }

    static bool LoadUsers() {
      Parallel.ForEach(File.ReadAllLines((Directory.GetCurrentDirectory() + "/Data/users.csv")), (line, _, lineNumber) => {
        string[] split = line.Split(';');

        if (split[0] == "id")
          return; // No continue in the threaded foreach

        if (int.Parse(split[0]) > 0) {
          // Loaded into Product.All by BaseModel Constructor
          // Added to global suppresion file.
          new User(int.Parse(split[0]), split[1], split[2], split[4], split[3],N decimal.Parse(split[5]));
        }
      });
      return true;
    }
  }
}
