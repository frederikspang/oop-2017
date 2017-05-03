using System;
using System.Collections.Generic;
namespace Eksamensopgave2017 {
  public class StregsystemController {

    Stregsystem _sys;

    public StregsystemCLI UI {
      get;
      set;
    }

    public Stregsystem Sys {
      get { return _sys; }
      set { _sys = value; }
    }
    public Dictionary<string, Action<dynamic, dynamic>> _adminCommands;

    public StregsystemController(IStregsystemUI ui, Stregsystem sys) {
      UI = (StregsystemCLI)ui;
      _sys = sys;
      _adminCommands = new Dictionary<string, Action<dynamic, dynamic>>();

      _adminCommands.Add("?", (x, y) => UI.DisplayHelp());

      _adminCommands.Add(":q", (x, y) => UI.Close());
      _adminCommands.Add(":quit", (x, y) => UI.Close());
      _adminCommands.Add(":activate", (x, y) => {
        Product.Find(int.Parse(x)).Activate();
        UI.DisplayActivation(int.Parse(x), true);
      });
      _adminCommands.Add(":deactivate", (x, y) => {
        Product.Find(int.Parse(x)).Deactivate();
        UI.DisplayActivation(int.Parse(x), false);
      });
      _adminCommands.Add(":crediton", (x, y) => {
        Product.Find(x).CanBeBoughtOnCredit = true;
        UI.DisplayCreditChange(x, false);
      });
      _adminCommands.Add(":creditoff", (x, y) => {
        Product.Find(x).CanBeBoughtOnCredit = false;
        UI.DisplayCreditChange(x, false);
      });
      _adminCommands.Add(":addcredits", (x, y) => {
        Sys.AddCreditsToAccount(User.FindBy("Username", x), y);
        UI.DisplayAddedCreditsToUser(User.FindBy("Username", x), y);
      });
    }

    public void ParseCommand(string command) {
      var split = command.Split(' ');
      if (split[0] == ":") {
        try {
          _adminCommands[split[0]].Invoke(split.Length > 1 ? split[1] : null, split.Length > 2 ? split?[2] : null);
        } catch (KeyNotFoundException) {
          UI.DisplayAdminCommandNotFoundMessage(split[0]);
        }
      } else {
        ParsePurchase(command);
      }
    }

    public void ParsePurchase(string command) {
      string[] split = command.Split(' ');
      if (split.Length == 2) {
        Product p = Product.Find(int.Parse(split[1]));
        BuyTransaction purchase = new BuyTransaction();
        purchase.Execute();
      } else {
        Product p = Product.Find(int.Parse(split[2]));
        int count = int.Parse(split[1]);

        for (int i = 0; i < int.Parse(split[1]); i++) {
          BuyTransaction purchase = new BuyTransaction();
          purchase.Execute();
        }
        UI.DisplayUserBuysProduct(count, purchase);
      }
    }

    public void Start() {
      while(true)
        UI.DisplayReadyForCommand();
    }
  }
}
