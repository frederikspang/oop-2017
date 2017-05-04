using System;
namespace Eksamensopgave2017 {
  public class SeasonalProduct : Product {
    public DateTime DeactivatesAt {
      get;
      set;
    }
    public SeasonalProduct(int id, string name, decimal price, bool active, bool canCredit, DateTime deactivatesAt) 
      : base(id, name, price, active, canCredit) {
      DeactivatesAt = deactivatesAt;
    }

    public new bool Active() {
      return base.Active() && DateTime.Now < DeactivatesAt;
    }
  }
}
