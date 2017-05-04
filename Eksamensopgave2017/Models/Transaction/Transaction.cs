using System;
using System.IO;

namespace Eksamensopgave2017 {
  public abstract class Transaction : BaseModel<Transaction> {
    public static int NextID = 1;
    User _user;
    bool _booked;

    public User User {
      get { return _user; }
      set { if (!_booked) _user = value; }
    }

    public DateTime Date { get; set; }
    public decimal Amount { get; protected set; }

    public bool Booked => _booked;

    public Transaction() {
      Id = Id == default(int) ? NextID++ : Id;
    }

    public bool Execute() {
      if (_booked) return false;

      Date = DateTime.Now;
      _booked = true;
      char sep = Path.DirectorySeparatorChar; // Unix Compatibity
      var dir = Directory.GetCurrentDirectory() + $"{sep}Data{sep}Log{sep}";
      var filename = "Transactions.log";
      if (!Directory.Exists(dir)) {
        Directory.CreateDirectory(dir);
      }
      using (StreamWriter w = File.AppendText(dir+filename)) {
        w.WriteLine($"[{GetType()} - {Date.ToString()}] {User.Name}[{User.Username}] {Amount} kr");
      }

      return true;
    }

    public override string ToString() {
      return $"[{Date.ToString()}] Transaktion {Id}, User{User.Id}, {Amount} kr.";
    }
  }
}
