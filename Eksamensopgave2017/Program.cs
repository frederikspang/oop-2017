//20165250_Frederik_Spang_Thomsen

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017 {
  class Program {
    static void Main(string[] args) {
      try {
        IStregsystem stregsystem = new Stregsystem();
        IStregsystemUI ui = new StregsystemCLI(stregsystem);

        ((StregsystemCLI)ui).Start();

        foreach (User prod in User.All) {
          Debug.WriteLine(prod);
        }
      } catch(FileLoadException) {
        Console.WriteLine("Kunne ikke hente indhold.");
      }
    }
  }
}
