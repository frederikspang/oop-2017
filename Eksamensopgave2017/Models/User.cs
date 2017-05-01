using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Eksamensopgave2017 {
  public class User : BaseModel<User>, IComparable {
    public string Email { get; set; }
    public string Username { get; set; }

    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public double Balance { get; set; }
   
    public User(string firstname, string lastname, string email) {
      if (ValidEmail(email))
        Email = email;
      else
        throw new ArgumentException("Email is not valid");

      while (!ValidUsername(Username))
        Username = GenerateUsername(lastname, Id);

      if (firstname == null || lastname == null)
        throw new ArgumentNullException("User firstname and/or lastname cannot be null");

      Id = All.Count();
      Firstname = firstname;
      Lastname = lastname;
      Balance = 0;

      All.Add(this);
    }

    public User(string firstname, string lastname, string email, int balance) {
      if (ValidEmail(email))
        Email = email;
      else
        throw new ArgumentException("Email is not valid");

      if (ValidUsername(GenerateUsername(lastname, Id)))
        Username = GenerateUsername(lastname, Id);
      else
        throw new ArgumentException("Invalid username Generated");

      if (firstname == null || lastname == null)
        throw new ArgumentNullException("User firstname and/or lastname cannot be null");

      Id = All.Count();
      Firstname = firstname;
      Lastname = lastname;
      Balance = balance;

      All.Add(this); // Add to static list of users.
    }

    public override string ToString() {
      return Firstname + " " + Lastname + " " + Email;
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

    public override int GetHashCode() {
      return Id.GetHashCode(); // Use the hasfor the unique ID. Could maybe use ID since it's an Int32?
    }

    string GenerateUsername(string name, int num) {
      return name.ToLower() + num.ToString();
    }

    bool ValidEmail(string mail) {
      bool local = false, domain = false;
      string[] split = mail.Split('@');
      if (split.Length != 2)
        return false;

      //Local check
      Regex check = new Regex("[a-zA-Z0-9-_.]");
      local = check.IsMatch(split[0]);

      //Domain check
      check = new Regex("[a-zA-Z0-9-._]");
      domain = check.IsMatch(split[1]) && split[1].Contains('.')
          && !CharEquals(split[1][0], '.', '-', '_') && !CharEquals(split[1][split[1].Length - 1], '.', '-', '_');

      return (local && domain);
    }

    bool CharEquals(char compareTo, params char[] chars) {
      foreach (char ch in chars) {
        if (ch == compareTo)
          return true;
      }
      return false;
    }

    bool ValidUsername(string username) {
      Regex check = new Regex("[a-z0-9_]");

      return !string.IsNullOrWhiteSpace(username) && check.IsMatch(username);
    }

  }
}