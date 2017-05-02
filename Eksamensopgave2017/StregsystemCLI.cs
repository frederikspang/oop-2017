using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017 {
  public class StregsystemCLI : IStregsystemUI {
    IStregsystem _sys;
    CommandParser Parser;

    Dictionary<string, Action<dynamic, dynamic>> _commands = new Dictionary<string, Action<dynamic, dynamic>>();

    public IStregsystem Sys {
      get { return _sys; }
    }

    public StregsystemCLI(IStregsystem sy) {
      _sys = sy;
      Parser = new CommandParser(this, (Stregsystem)sy);
    }

    public void DisplayUserNotFound(string username) {
      Console.WriteLine("No user with username [" + username + "] found");
    }

    public void DisplayProductNotFound(string id) {
      Console.WriteLine("No product with id [" + id + "] found");
    }

    public void DisplayProductInactive() {
      Console.WriteLine("Attempted to purchase inactive product");
    }

    public void DisplayUserInfo(User u) {

      PrintUserStats(u);
      if (u.Balance < 50)
        DisplayBalanceBelowFifty();

      int i = 0;
      var t = Sys.GetTransactions(u, 20);
      Console.WriteLine("Last transactions:");
      foreach (var tran in t)
        Console.WriteLine((++i).ToString() + ". " + tran.ToString());

    }

    private void PrintUserStats(User u) {
      Console.WriteLine("*----------------------------------");
      Console.WriteLine("* ID:       " + u.Id);
      Console.WriteLine("* Username: " + u.Username);
      Console.WriteLine("* Name:     " + u.Name());
      Console.WriteLine("* E-mail:   " + u.Email);
      Console.WriteLine("* Balance:  " + u.Balance);
      Console.WriteLine("*----------------------------------");
    }

    public void DisplayTooManyArgumentsError(string args) {
      Console.WriteLine("[" + args + "] too many arguments for this command");
    }

    public void DisplayAdminCommandNotFoundMessage(string args) {
      Console.WriteLine("[" + args + "] is not a valid admin commando");
      Console.ReadKey();
    }

    public void DisplayUserBuysProduct(PurchaseTransaction transaction) {
      Console.WriteLine(transaction.ToString());
      if (transaction.User.Balance < 50)
        DisplayBalanceBelowFifty();
    }

    public void DisplayUserBuysProduct(int count, Product p, User u) {
      Console.WriteLine("[" + u.Username + "] Bought " + count.ToString() + "x " + p.Name + " for " + (p.Price * (double)count).ToString() + "kr");
      if (u.Balance < 50)
        DisplayBalanceBelowFifty();
    }

    public void Close() {
      Console.WriteLine("Shutting down");
      Environment.Exit(0);
    }

    public void DisplayInsufficientCash(User u) {
      Console.WriteLine("[" + u.Username + "] Not enough credit to purchase product");
    }

    public void DisplayInsufficientCash(User u, int count) {
      Console.WriteLine("[" + u.Username + "] Not enough credit to purchase " + count.ToString() + "x products");
    }

    public void DisplayGeneralError(string msg) {
      Console.WriteLine(msg);
    }

    public void DisplayReadyForCommand() {
      Console.Clear();
      DisplayActiveProducts();
      Console.Write("\n>");
    }

    public void DisplayAddedCreditsToUser(User u, double amount) {
      Console.WriteLine("Added " + amount + "kr to user [" + u.Username + "]");
    }

    private void DisplayBalanceBelowFifty() {
      Console.WriteLine("Please note that your balance is below 50kr!");
    }

    private void DisplayActiveProducts() {
      Console.Write(string.Format("{0, -4}|{1, 7} - {2}", "ID", "Price", "Product"));
      DisplayHelpOptions();
      foreach (Product p in Product.All.Where(p => p.Active() )) {
        Console.WriteLine(p);
      }
    }

    public void DisplayEnterToCont() {
      Console.WriteLine("\nPress enter to return to menu");
    }

    public void DisplayTransactionNotFound(string username) {
      Console.WriteLine("No transactions found for user [" + username + "]");
    }

    private void DisplayHelpOptions() {
      Console.WriteLine(" (? for help)");
    }

    public void DisplayHelp() {
      Console.WriteLine("Commands are as follows: (without <>)");
      Console.WriteLine("> ?" + "\n\tDisplays this\n");
      Console.WriteLine("> <Username>" + "\n\tDisplays User statistics\n");
      Console.WriteLine("> <Username> <Product id>" + "\n\tPurchase product with stated id\n");
      Console.WriteLine("> <Username> <Amount> <Product id>" + "\n\tPurchase product wiht stated id <Amount> times\n");
      Console.WriteLine("For administrative commands, please contact your systems admin");
    }

    public void DisplayActivation(int id, bool val) {
      Console.WriteLine("Product with id [" + id + "] was " + (val ? "activated" : "deactivated"));
    }

    public void DisplayCreditChange(int id, bool val) {
      Console.WriteLine("Product with id [" + id + "] " + (val ? "may" : "cannot") + " be bought on credit now");
    }

    public void Start() {
      DisplayReadyForCommand();
      var input = Console.ReadLine();

      if (input.Length > 0) {
        if (input[0] == ':' || input[0] == '?') {
          Parser.ParseCommand(input);
        } else {
          var lookup = User.FindBy("Username", input);
          if (lookup != null) {
            DisplayUserInfo(lookup);
          } else {
            DisplayUserNotFound(input);
          }
          Console.ReadKey();
        }
      }
      Start();
    }
  }
}