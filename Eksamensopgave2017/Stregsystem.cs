using System;
using System.Collections.Generic;

namespace Eksamensopgave2017 {
  class Stregsystem : IStregsystem {
    public IEnumerable<Product> ActiveProducts => Product.All;

    //public event UserBalanceNotification UserBalanceWarning;

    public InsertCashTransaction AddCreditsToAccount(User user, int amount) {
      throw new NotImplementedException();
    }

    public PurchaseTransaction BuyProduct(User user, Product product) {
      throw new NotImplementedException();
    }

    public Product GetProductByID(int productID) {
      throw new NotImplementedException();
    }

    public IEnumerable<Transaction> GetTransactions(User user, int count) {
      throw new NotImplementedException();
    }

    public User GetUser(Func<User, bool> predicate) {
      throw new NotImplementedException();
    }

    public User GetUserByUsername(string username) {
      throw new NotImplementedException();
    }

    IEnumerable<Transaction> IStregsystem.GetTransactions(User user, int count) {
      throw new NotImplementedException();
    }
  }
}