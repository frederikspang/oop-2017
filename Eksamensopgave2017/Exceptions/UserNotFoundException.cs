namespace Eksamensopgave2017.Exceptions {
  public class UserNotFoundException : System.Exception {
    public string Username;
    public UserNotFoundException() {
    }

    public UserNotFoundException(string username) : base(username) {
      Username = username;
    }

    public UserNotFoundException(string username, System.Exception inner) : base(username, inner) {
      Username = username;
    }
  }
}