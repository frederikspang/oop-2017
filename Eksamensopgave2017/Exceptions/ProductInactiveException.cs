using System;
namespace Eksamensopgave2017.Exceptions {
  public class ProductInactiveException : Exception {
    public Product Product;

    public ProductInactiveException() {
    }

    public ProductInactiveException(Product p) {
      Product = p;
    }
  }
}
