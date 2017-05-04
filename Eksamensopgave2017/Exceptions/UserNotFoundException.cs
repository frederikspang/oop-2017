namespace Eksamensopgave2017.Exceptions {
  public class UserNotFoundException : System.Exception {
    public string Username;
    public UserNotFoundException() {
    }

    public UserNotFoundException(string username) {
      Username = username;
    }
  }
}