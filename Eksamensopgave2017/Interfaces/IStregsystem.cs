using System.Collections.Generic;

namespace Eksamensopgave2017 {
  public interface IStregsystem {
    IEnumerable<Product> ActiveProducts { get; }
    InsertCashTransaction AddCreditsToAccount(User user, int amount);
    BuyTransaction BuyProduct(User user, Product product);
    IEnumerable<Transaction> GetTransactions(User user, int count);
    //event UserBalanceNotification UserBalanceWarning;
  }
}