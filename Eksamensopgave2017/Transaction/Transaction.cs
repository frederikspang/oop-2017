using System;
namespace Eksamensopgave2017 {
  public abstract class Transaction : BaseModel<Transaction> {
    public static int NextID = 1;
    public User User { get; set; }
    public DateTime Date { get; set; }
    public int Amount { get; set; }

    public bool Execute() {
      throw new NotImplementedException();
    }

    override public string ToString() {
      return $"[{Date.ToString()}] Transaktion {Id}, User{User.Id}, {Amount} kr.";
    }
  }
}
