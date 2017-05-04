using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Eksamensopgave2017 {
  public class User : BaseModel<User>, IComparable {

    #region Properties
    public string Email { get; set; }
    public string Username { get; set; }

    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public string Name => $"{Firstname} {Lastname}";

    public decimal Balance { get; set; }

    public List<Transaction> Transactions => Transaction.All.Where(t => t.User == this).ToList();

    #endregion Properties

    #region Constructors
    // Used for user creation. No balance or Username, and auto generated ID
    public User(string firstname, string lastname, string email) {
      if (ValidEmail(email))
        Email = email;
      else
        throw new ArgumentException("Email is not valid");

      Username = GenerateUsername(email);

      if (firstname == null || lastname == null)
        throw new ArgumentNullException("User firstname and/or lastname cannot be null");

      Id = All.Count();
      Firstname = firstname;
      Lastname = lastname;
      Balance = 0;
    }

    // Used for loading.
    public User(int ID, string firstname, string lastname, string email, string username, decimal balance) {
      if (ValidEmail(email.Replace("\"", "")))
        Email = email.Replace("\"", "");
      else
        throw new ArgumentException("Email is not valid");

      Username = username.Replace("\"", "");

      if (firstname == null || lastname == null)
        throw new ArgumentNullException("User firstname and/or lastname cannot be null");

      Id = ID;
      Firstname = firstname.Replace("\"", "");
      Lastname = lastname.Replace("\"", "");
      Balance = balance;
    }
    #endregion Constructors

    #region Generators and Validators

    string GenerateUsername(string email) {
      return email.Split('@')[0];
    }

    bool ValidEmail(string mail) {
      Regex check = new Regex("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

      return check.IsMatch(mail);
    }
    #endregion Generators and Validators

    public void AddCredit(int credit) {
      Balance += credit / 100;
    }

    #region Interfaces
    public override string ToString() {
      return $"{Firstname} {Lastname} <{Email}>";
    }

    public int CompareTo(Object obj) {
      if (obj == null)
        return 1;

      User otherUser = obj as User;

      if (otherUser != null)
        return Id.CompareTo(otherUser.Id);

      throw new ArgumentException("Object is not of type 'User'");
    }

    public override bool Equals(object obj) {
      if (obj == null || !(obj is User))
        return false;
      return Username == ((User)obj).Username;
    }
    #endregion

    public override int GetHashCode() {
      return Id.GetHashCode(); // Use the hasfor the unique ID. Could maybe use ID since it's an Int32?
    }
  }
}