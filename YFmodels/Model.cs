using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YFmodels
{
    public class Model : ICloneable
    {
        public List<Atom> atoms;

        public Model()
        {
            atoms = new List<Atom>();
        }

        public bool IsConflict()
        {
            foreach (var a in atoms)
                if (a.trueFlag && a.falseFlag) return true;
            return false;
        }

        public object Clone()
        {
            Model m = new Model();
            foreach (Atom a in atoms)
                m.atoms.Add(a);
            return m;
        }

        public static bool operator==(Model a, Model b)
        {
            if (a.atoms.Count != b.atoms.Count) return false;
            foreach(var atoma in a.atoms)
            {
                bool find = false;
                foreach(var atomb in b.atoms)
                    if(atomb.atom == atoma.atom)
                    {
                        find = true;
                        break;
                    }
                if (!find)
                    return false;
            }
            return true;
        }

        public static bool operator !=(Model a, Model b)
        {
            return !(a == b);
        }
    }
}
