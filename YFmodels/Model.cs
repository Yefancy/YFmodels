using System;
using System.Collections.Generic;
using System.Text;

namespace YFmodels
{
    public class Model:ICloneable
    {
        public List<Atom> facts;
        public List<Rule> rules;

        public Model()
        {
            facts = new List<Atom>();
            rules = new List<Rule>();
        }

        public override string ToString()
        {
            string result = "\n";
            foreach(var f in facts)
            {
                result += f.ToString() + "\n";
            }
            foreach (var r in rules)
            {
                result += r.ToString() + "\n";
            }
            return result;
        }

        public void AddRule(Rule rule)
        {
            rules.Add(rule);
        }

        public void AddFact(Atom fact)
        {
            facts.Add(fact);
        }

        public object Clone()
        {
            Model clone = new Model();
            clone.facts = Utils.ListClone(facts);
            clone.rules = Utils.ListClone(rules);
            return clone;
        }
    }
}
