using System;
using System.Text.RegularExpressions;
namespace Eksamensopgave2017 {
  public class Product : BaseModel<Product> {
    public bool CanBeBoughtOnCredit { get; set; }
    public bool _active { get; set; }
    public double Price { get; set; }
    public string Name { get; set; }

    public Product(int id, string name, double price, bool active, bool canCredit) {
      if (id < 1)
        throw new ArgumentOutOfRangeException(nameof(id), "Product ID must be higher than 0!");

      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(nameof(name), "Product name cannot be null");

      Id = id;

      var namecheck = new Regex(@"</?[\d\w]{1,15}>");
      Name = namecheck.Replace(name, "").Replace("\"", "");
      Price = price;
      _active = active;
      CanBeBoughtOnCredit = canCredit;
    }

    public bool Active(){
      return _active;
    }

    public void Activate(){
      _active = true;
    }
    public void Deactivate(){
      _active = false;
    }

    public override string ToString() => $"{Id} {Name} ({Price.ToString()} kr)";
  }
}
