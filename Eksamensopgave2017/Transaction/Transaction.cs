using System;
namespace Eksamensopgave2017 {
  public abstract class Transaction : BaseModel<Transaction> {
    public static int NextID = 1;
    public User User { get; set; }
    public DateTime Date { get; set; }
    public int Amount { get; private set; }

    public bool Execute() {
      Console.WriteLine("KØB: xxxx");
      throw new NotImplementedException();
    }

    override public string ToString() {
      return $"[{Date.ToString()}] Transaktion {Id}, User{User.Id}, {Amount} kr.";
    }
  }
}
