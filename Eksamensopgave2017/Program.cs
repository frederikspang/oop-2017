//20165250_Frederik_Spang_Thomsen

using System;
using System.Diagnostics;
using System.IO;

namespace Eksamensopgave2017 {
  class Program {
    static void Main(string[] args) {
      try {
        IStregsystem stregsystem = new Stregsystem();
        IStregsystemUI ui = new StregsystemCLI(stregsystem);

        ui.Start();

        foreach (User prod in User.All) {
          Debug.WriteLine(prod);
        }
      } catch(FileLoadException) {
        Console.WriteLine("Kunne ikke hente indhold.");
      }
    }
  }
}
