using System;
using System.Collections.Generic;
using System.Linq;

namespace Eksamensopgave2017 {
  public class Stregsystem : IStregsystem {
    public IEnumerable<Product> ActiveProducts => Product.All;

    //public event UserBalanceNotification UserBalanceWarning;

    public InsertCashTransaction AddCreditsToAccount(User user, int amount) {
      throw new NotImplementedException();
    }

    public BuyTransaction BuyProduct(User user, Product product) {
      throw new NotImplementedException();
    }

    public Product GetProductByID(int productID) {
      return Product.Find(productID);
    }

    public IEnumerable<Transaction> GetTransactions(User user, int count) {
      return Transaction.All.Where(t => t.User == user).OrderBy(t => t.Date).Take(count);
    }

    public User GetUserByUsername(string username) {
      return User.FindBy("Username", username);
    }
  }
}