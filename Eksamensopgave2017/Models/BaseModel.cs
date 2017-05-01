using System.Collections.Generic;
using System;

namespace Eksamensopgave2017 {
  public abstract class BaseModel<T> where T : BaseModel<T> {
    public int Id {
      get;
      protected set;
    }
    static List<T> _all = new List<T>();
    public static List<T> All {
      get {
        return _all;
      }
      set { 
        _all = value; 
      } 
    }

    public static T Find(int id) {
      return All.Find((obj) => obj.Id == id );
    }

    protected BaseModel() {
      All.Add((T)this);
    }
  }
}