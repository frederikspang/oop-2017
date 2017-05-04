using System;
using System.Text.RegularExpressions;


namespace Eksamensopgave2017 {
  /// <summary>
  /// Product.
  /// </summary>
  public class Product : BaseModel<Product> {
    public bool CanBeBoughtOnCredit { get; set; }
    public bool _active { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; }

    public Product(int id, string name, decimal price, bool active, bool canCredit) {
      if (id < 1)
        throw new ArgumentOutOfRangeException(nameof(id), "Product ID must be higher than 0!");

      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(nameof(name), "Product name cannot be null");

      Id = id;

      // Match HTML med < > og måske en / som tegn nr 2. ? er {0,1} match
      // Virker kun på html tags på 1-15 tegn eller lavere
      // - Kun fordi den længste jeg kan komme i tanke om er <blockquote> på 10.. Og så en safetybuffer oveni
      // Matcher ikke HTML med Classes, eller ID'er eller andre attributter (data-x, disabled m.fl)
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
