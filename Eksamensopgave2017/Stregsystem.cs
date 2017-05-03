using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eksamensopgave2017 {
  public class Stregsystem : IStregsystem {

    public Stregsystem(){
      LoadProducts();
      LoadUsers();
    }
    
    public IEnumerable<Product> ActiveProducts => Product.All.Where(p => p.Active());

    //public event UserBalanceNotification UserBalanceWarning;

    public InsertCashTransaction AddCreditsToAccount(User user, int amount) {
      throw new NotImplementedException();
    }

    public BuyTransaction BuyProduct(User user, Product product) {
      throw new NotImplementedException();
    }

    public IEnumerable<Transaction> GetTransactions(User user, int count) {
      return Transaction.All.Where(t => t.User == user).OrderBy(t => t.Date).Take(count);
    }
    private bool LoadProducts() {
      foreach (string line in File.ReadAllLines((Directory.GetCurrentDirectory() + "/Data/products.csv"))) {
        string[] split = line.Split(';');
        if (split == null || split[0] == "id") {
          continue;
        }

        if (int.Parse(split[0]) > 0) {
          // Loaded into Product.All by BaseModel Constructor
          // Added to global suppresion file.
          new Product(int.Parse(split[0]), split[1], double.Parse(split[2]) / 100, int.Parse(split[3]) != 0, false);
        }
      };
      return true;
    }

    private bool LoadUsers() {
      foreach (string line in File.ReadAllLines((Directory.GetCurrentDirectory() + "/Data/users.csv"))) {
        string[] split = line.Split(';');

        if (split[0] == "id")
          continue; // No continue in the threaded foreach

        if (int.Parse(split[0]) > 0) {
          // Loaded into User.All by BaseModel Constructor
          // Added to global suppresion file.
          new User(int.Parse(split[0]), split[1], split[2], split[4], split[3], decimal.Parse(split[5]));
        }
      };
      return true;
    }
  }
}