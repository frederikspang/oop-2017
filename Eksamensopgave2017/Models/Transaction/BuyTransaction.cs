using Eksamensopgave2017.Exceptions;
namespace Eksamensopgave2017 {
  public class BuyTransaction : Transaction {
    public Product Product { get; set; }

    public BuyTransaction(Product product, User user) {
      Product = product;
      User = user;
    }

    public new bool Execute() {
      if (this.Product.Active()) {
        if (this.Product.CanBeBoughtOnCredit) {
          this.User.Balance += (this.Product.Price * -1);
          Amount = this.Product.Price;
        } else if (this.User.Balance >= this.Product.Price) {
          this.User.Balance += (this.Product.Price * -1);
          Amount = this.Product.Price;
        } else {
          throw new InsufficientCreditsException(this.User);
        }
      } else {
        throw new ProductInactiveException(this.Product);
      }
      return true; // Ensure the common methods are
    }
  }
}
