using System.Reflection;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using System.Linq;

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

    public static IEnumerable<T> Where(Func<T, bool> match) {
      return All.Where(match);
    }

    public static T Find(int id) {
      return _all.Find(obj => (obj.Id == id) );
    }

    public static T FindBy(string field, string val) {
      return _all.Find(obj => obj.GetType().GetProperty(field).GetValue(obj, null).Equals(val));
    }

    protected BaseModel() {
      All.Add((T)this);
    }
  }
}