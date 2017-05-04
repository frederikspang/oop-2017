using System;
using System.Linq;

namespace Eksamensopgave2017 {
  public class StregsystemCLI : IStregsystemUI {
    StregsystemController Parser;

    int _shutting_down = -1;


    public StregsystemCLI() {
      Parser = new StregsystemController(this);
    }

    public void DisplayUserNotFound(string username) {
      Console.WriteLine("No user with username [" + username + "] found");
    }

    public void DisplayProductNotFound(string id) {
      Console.WriteLine("No product with id [" + id + "] found");
    }

    public void DisplayProductInactive(Product p) {
      Console.WriteLine($"{p.Name} is inactive");
    }

    public void DisplayUserInfo(User u) {
      PrintUserStats(u);
      if (u.Balance < 50)
        DisplayBalanceBelowFifty();

      int i = 0;
      var t = u.Transactions;
      Console.WriteLine("Last transactions:");
      foreach (var tran in t)
        Console.WriteLine((++i).ToString() + ". " + tran.ToString());

    }

    private void PrintUserStats(User u) {
      Console.WriteLine("/----------------------------------");
      Console.WriteLine("| ID:       " + u.Id);
      Console.WriteLine("| Username: " + u.Username);
      Console.WriteLine("| Name:     " + u.Name);
      Console.WriteLine("| E-mail:   " + u.Email);
      Console.WriteLine("| Balance:  " + u.Balance);
      Console.WriteLine("\\----------------------------------");
    }

    public void DisplayTooManyArgumentsError(string args) {
      Console.WriteLine("[" + args + "] too many arguments for this command");
    }

    public void DisplayAdminCommandNotFoundMessage(string args) {
      Console.WriteLine("[" + args + "] is not a valid admin commando");
    }

    public void DisplayUserBuysProduct(BuyTransaction transaction) {
      Console.WriteLine(transaction);
      if (transaction.User.Balance < 50)
        DisplayBalanceBelowFifty();
    }

    public void DisplayUserBuysProduct(int count, Product product, User user) {
      Console.WriteLine("[" + user.Username + "] Bought " + count.ToString() + "x " + product.Name + " for " + (product.Price * count).ToString() + "kr");
      if (user.Balance < 50)
        DisplayBalanceBelowFifty();
    }

    public void Close(int exit_code = 0) {
      Console.WriteLine("Shutting down");
      _shutting_down = exit_code;
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
      var input = Console.ReadLine();

      if (input.Length > 0) {
        Parser.ParseCommand(input);
      } else {
        DisplayGeneralError("Error");
      }
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
      foreach (Product p in Product.All.Where(p => p.Active())) {
        Console.WriteLine(String.Format("{0, -4}|{1, 7:F} - {2}", p.Id, p.Price, p.Name));
      }
    }

    public void DisplayEnterToCont() {
      Console.WriteLine("\nPress enter to return to menu");
      Console.ReadKey();
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

    public void DisplayInsufficientCash(User user, Product product) {
      throw new NotImplementedException();
    }

    public void Start() {
      while (_shutting_down == -1) {
        DisplayReadyForCommand();
        DisplayEnterToCont();
      }

      Environment.Exit(_shutting_down);
    }
  }
}