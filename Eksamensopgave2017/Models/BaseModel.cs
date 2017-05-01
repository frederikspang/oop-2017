using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Eksamensopgave2017 {
  public abstract class BaseModel<T> where T : BaseModel<T> {
    public int Id {
      get;
      protected set;
    }
    public static List<T> All { get; set; }

    public static T Find(int id) {
      return All.Find((obj) => obj.Id == id );
    }
  }
}