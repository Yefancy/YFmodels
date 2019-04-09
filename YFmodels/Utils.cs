using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YFmodels
{
    public static class Utils
    {
        public static List<T> ListClone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

    }
}
