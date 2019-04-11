using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YFmodels
{
    public class Model : ICloneable
    {
        public List<int> trueList;
        public List<int> falseList;

        public Model()
        {
            trueList = new List<int>();
            falseList = new List<int>();
        }

        public override string ToString()
        {
            var result = "\ntrueList:";
            foreach (var i in trueList) result += i + " ";
            result += "\nfalseList:";
            foreach (var i in falseList) result += i + " ";

            return result+"\n";
        }

        public int Count()
        {
            return trueList.Count + falseList.Count;
        }

        public Model Converse()
        {
            Model m = new Model();
            m.trueList = falseList.ToList();
            m.falseList = trueList.ToList();
            return m;
        }

        public bool IsConflict()
        {
            HashSet<int> hash = new HashSet<int>();
            foreach (var i in trueList)
                hash.Add(i);
            foreach (var i in falseList)
                if (hash.Contains(i)) return true;
            return false;
        }

        public object Clone()
        {
            Model m = new Model();
            m.trueList = trueList.ToList();
            m.falseList = falseList.ToList();
            return m;
        }

        public static Model operator +(Model a, Model b)
        {
            foreach (int _ in b.trueList)
            {
                if (a.trueList.Contains(_)) continue;
                    a.trueList.Add(_);
            }
            foreach (int _ in b.falseList)
            {
                if (a.falseList.Contains(_)) continue;
                    a.falseList.Add(_);
            }
            return a;
        }

        public static Model operator -(Model a, Model b)
        {
            foreach(int _ in b.trueList)
            {
                if (a.trueList.Contains(_))
                    a.trueList.Remove(_);
            }
            foreach (int _ in b.falseList)
            {
                if (a.falseList.Contains(_))
                    a.falseList.Remove(_);
            }
            return a;
        }

        public static bool operator==(Model a, Model b)
        {
            if (a.trueList.Count != b.trueList.Count || a.falseList.Count != b.falseList.Count) return false;
            foreach (var i in a.trueList)
                if (!b.trueList.Contains(i)) return false;
            foreach (var i in a.falseList)
                if (!b.falseList.Contains(i)) return false;
            return true;
        }

        public static bool operator !=(Model a, Model b)
        {
            return !(a == b);
        }
    }
}
