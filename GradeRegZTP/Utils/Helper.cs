using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradeRegZTP
{
    public static class Helper
    { 
        public static IEnumerable<T> ToIEnumerable<T>(IEnumerator enumerator)
        {
            var listajakas = new List<T>();
            while (enumerator.MoveNext())
            {
                listajakas.Add((T)enumerator.Current);
            }
            return listajakas;
        }
    }
}