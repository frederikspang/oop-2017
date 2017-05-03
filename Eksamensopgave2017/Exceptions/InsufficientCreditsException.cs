using System;
namespace Eksamensopgave2017.Exceptions {
  public class InsufficientCreditsException : Exception {
    public User User;

    public InsufficientCreditsException() {
    }

    public InsufficientCreditsException(User u) {
      User = u;
    }

    public InsufficientCreditsException(string message) : base(message) {
    }

    public InsufficientCreditsException(string message, Exception inner) : base(message, inner) {
    }
  }
}
