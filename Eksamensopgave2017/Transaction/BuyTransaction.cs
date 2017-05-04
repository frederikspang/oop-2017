namespace Eksamensopgave2017 {
  public class BuyTransaction : Transaction {
    public Product Product { get; set; }

    public BuyTransaction(Product product, User user) {
      Product = product;
      User = user;
    }
  }
}
