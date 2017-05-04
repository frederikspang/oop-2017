using System;
using System.IO;
using System.Diagnostics;
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

    public bool Execute() {
      Date = DateTime.Now;
      _booked = true;

      return true;
    }

    override public string ToString() {
      return $"[{Date.ToString()}] Transaktion {Id}, User{User.Id}, {Amount} kr.";
    }
  }
}
