using Eksamensopgave2017.Exceptions;
namespace Eksamensopgave2017 {
  public class BuyTransaction : Transaction {
    public Product Product { get; set; }

    public BuyTransaction(Product product, User user) : base() {
      Product = product;
      User = user;
    }

    public new bool Execute() {
      if (Product.Active()) {
        if (Product.CanBeBoughtOnCredit || User.Balance >= Product.Price) {
          User.Balance -= Product.Price;
          Amount = this.Product.Price;
        } else {
          throw new InsufficientCreditsException(User);
        }
      } else {
        throw new ProductInactiveException(Product);
      }
      return base.Execute(); // Ensure the common methods are
    }
  }
}
