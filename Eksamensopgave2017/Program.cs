//20165250_Frederik_Spang_Thomsen

using System;
using System.IO;

namespace Eksamensopgave2017 {
  class Program {
    static void Main(string[] args) {
      try {
        if (StregsystemLoader.Load()) {
          IStregsystemUI ui = new StregsystemCLI();
          ui.Start();  
        } else {
          throw new FileLoadException();
        }
      } catch(FileLoadException) {
        Console.WriteLine("Kunne ikke hente indhold.");
      }
    }
  }
}
