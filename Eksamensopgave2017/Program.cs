﻿//20165250_Frederik_Spang_Thomsen

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
        Console.WriteLine("Yes");
        Console.ReadLine();
      } catch(FileLoadException) {
        Console.WriteLine("Kunne ikke hente indhold.");
      }
    }

    static bool LoadProducts() {
      
      return true;
    }

    static bool LoadUsers() {
      return true;
    }
  }
}
