using System;
using System.Collections.Generic;
using System.Text;

namespace YFmodels
{
    public class Atom : ICloneable
    {
        public string name = "";
        public List<Literal> literals;
        //public Value trueValue;
        public bool isNot;

        public Atom(string name = "")
        {
            this.name = name;
            isNot = false;
            literals = new List<Literal>();
        }

        public object Clone()
        {
            Atom clone = new Atom(name);
            clone.isNot = isNot;
            //clone.trueValue = trueValue;
            clone.literals = Utils.ListClone(literals);
            return clone;
        }

        public override string ToString()
        {
            if (literals.Count == 0) return name + ".";
            string result = name + "(";
            for (int i = 0; i < literals.Count - 1; i++)
            {
                result += literals[i].content + ",";
            }
            result += literals[literals.Count - 1].content;
            return result + ").";
        }

        public static bool operator ==(Atom a, Atom b)
        {
            if (a.name != b.name) return false;
            if (a.literals.Count != b.literals.Count) return false;
            for (int i = 0; i < a.literals.Count; i++)
                if (a.literals[i] != b.literals[i]) return false;
            return true;
        }

        public static bool operator !=(Atom a, Atom b)
        {
            return !(a == b);
        }
    }
}
