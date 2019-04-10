using System;
using System.Collections.Generic;
using System.Text;

namespace YFmodels
{
    public struct Atom
    {
        public int atom;
        public int headof;
        public bool trueFlag;
        public bool falseFlag;     
    }

    public class AtomUtils
    {
        public static List<Rule> PList(List<Rule> rules, Atom atom)
        {
            List<Rule> result = new List<Rule>();
            foreach(var rule in rules)
                foreach(var l in rule.pBody)
                    if(l.literal == atom.atom) { result.Add(rule); break; }
            return result;
        }
        public static List<Rule> NList(List<Rule> rules, Atom atom)
        {
            List<Rule> result = new List<Rule>();
            foreach (var rule in rules)
                foreach (var l in rule.nBody)
                    if (l.literal == atom.atom) { result.Add(rule); break; }
            return result;
        }
    }
}
