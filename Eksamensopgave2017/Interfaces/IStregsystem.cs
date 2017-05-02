using System;
using System.Collections.Generic;

namespace Eksamensopgave2017 {
  public interface IStregsystem {
    IEnumerable<Product> ActiveProducts { get; }
    InsertCashTransaction AddCreditsToAccount(User user, int amount);
    PurchaseTransaction BuyProduct(User user, Product product);
    Product GetProductByID(int productID);
    IEnumerable<Transaction> GetTransactions(User user, int count);
    User GetUserByUsername(string username);
    //event UserBalanceNotification UserBalanceWarning;
  }
}