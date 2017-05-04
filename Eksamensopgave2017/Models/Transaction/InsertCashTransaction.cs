namespace Eksamensopgave2017 {
  public class InsertCashTransaction : Transaction {
    public InsertCashTransaction(User u, decimal amount) : base() {
      Amount = amount;
      User = u;
    }

    public new bool Execute() {
      User.AddCredit(Amount);
      return base.Execute();
    }
  }
}