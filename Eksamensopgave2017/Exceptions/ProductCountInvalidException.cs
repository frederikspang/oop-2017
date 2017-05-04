using System;
namespace Eksamensopgave2017.Exceptions {
  public class ProductCountInvalidException : Exception {
    public int Count;

    public ProductCountInvalidException() {
    }

    public ProductCountInvalidException(int count) {
      Count = count;
    }

    public ProductCountInvalidException(string message) : base(message) {
    }

    public ProductCountInvalidException(string message, Exception inner) : base(message, inner) {
    }
  }
}
