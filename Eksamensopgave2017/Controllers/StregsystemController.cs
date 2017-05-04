using System;
using System.Collections.Generic;
using Eksamensopgave2017.Exceptions;

namespace Eksamensopgave2017 {
  public class StregsystemController {
    public StregsystemCLI UI {
      get;
      set;
    }

    public Dictionary<string, Action<dynamic, dynamic>> _adminCommands;

    public StregsystemController(IStregsystemUI ui) {
      UI = (StregsystemCLI)ui;
      _adminCommands = new Dictionary<string, Action<dynamic, dynamic>> {
        { "?", (x, y) => UI.DisplayHelp() },

        { ":q", (x, y) => UI.Close() },
        { ":quit", (x, y) => UI.Close() },
        {
          ":activate",
          (x, y) => {
            Product.Find(int.Parse(x)).Activate();
            UI.DisplayActivation(int.Parse(x), true);
          }
        },
        {
          ":deactivate",
          (x, y) => {
            Product.Find(int.Parse(x)).Deactivate();
            UI.DisplayActivation(int.Parse(x), false);
          }
        },
        {
          ":crediton",
          (x, y) => {
            Product.Find(x).CanBeBoughtOnCredit = true;
            UI.DisplayCreditChange(x, false);
          }
        },
        {
          ":creditoff",
          (x, y) => {
            Product.Find(x).CanBeBoughtOnCredit = false;
            UI.DisplayCreditChange(x, false);
          }
        },
        {
          ":addcredits",
          (x, y) => {
            User u = User.FindBy("Username", x);
            var transaction = new InsertCashTransaction(u, decimal.Parse(y));
            transaction.Execute();
            UI.DisplayAddedCreditsToUser(User.FindBy("Username", x), decimal.Parse(y));
          }
        }
      };
    }

    public void ParseCommand(string command) {
      if (command[0] == ':') {
        var split = command.Split(' ');
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
      string[] split = command.Split(new[] { ' ' });
      User u = User.FindBy("Username", split[0]);

      try {
        int pid;
        Product p = null;

        if (u == null)
          throw new UserNotFoundException(split[0]);
        
        if (split.Length == 1) {
          UI.DisplayUserInfo(u);
        } else if (split.Length == 2) {
          if (int.TryParse(split[2], out pid)) {
            p = Product.Find(pid);
          }
          BuyTransaction purchase = new BuyTransaction(user: u, product: p);
          purchase.Execute();
        } else {
          if (int.TryParse(split[2], out pid)) {
            p = Product.Find(pid);
          }

          int count = int.Parse(split[1]);
          if (count < 1) {
            throw new ProductCountInvalidException(count);
          }

          for (int i = 0; i < count; i++) {
            BuyTransaction purchase = new BuyTransaction(user: u, product: p);
            purchase.Execute();
          }
          UI.DisplayUserBuysProduct(product: p, user: u, count: count);
        }
      } catch (InsufficientCreditsException e) {
        UI.DisplayInsufficientCash(e.User);
      } catch (UserNotFoundException e) {
        UI.DisplayUserNotFound(e.Username);
      } catch (ProductInactiveException e) {
        UI.DisplayProductInactive(e.Product);
      } catch (ProductCountInvalidException e) {
        UI.DisplayGeneralError($"You cannot buy {e.Count} of those.");
      } catch (NullReferenceException) {
        UI.DisplayProductNotFound(split[split.Length == 2 ? 1 : 2]); // Ternary for the if above..
      }
    }
  }
}
