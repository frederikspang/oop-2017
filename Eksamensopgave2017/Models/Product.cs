using System;
namespace Eksamensopgave2017 {
  public class Product : BaseModel<Product> {
    public bool CanBeBoughtOnCredit { get; set; }
    public bool Active { get; set; }
    public double Price { get; set; }
    public string Name { get; set; }
    public int ProductID { get; set; }

    public Product(int id, string name, double price, bool active, bool canCredit) {
      if (id < 1)
        throw new ArgumentOutOfRangeException(nameof(id), "Product ID must be higher than 0!");

      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(nameof(name), "Product name cannot be null");

      ProductID = id;
      Name = name;
      Price = price;
      Active = active;
      CanBeBoughtOnCredit = canCredit;

    }

    public string ToPrettyString() {
      return $"{Name} ({Price.ToString()} kr)";
    }
  }
}
