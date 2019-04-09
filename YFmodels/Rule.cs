using System;
using System.Collections.Generic;

namespace YFmodels
{
    public class Rule:ICloneable
    {
        public RuleType ruleType;
        public List<Atom> head;
        public List<Atom> body;
        public bool haveNot;

        public Rule()
        {
            haveNot = false;
            head = new List<Atom>();
            body = new List<Atom>();
        }

        public void AddHead(Atom a)
        {
            head.Add(a);
        }
        public void AddBody(Atom a)
        {
            if (a.isNot) haveNot = true;
            body.Add(a);
        }

        public override string ToString()
        {
            string result = "";
            foreach(var h in head)
            {
                result += h.ToString() + " ";
            }
            result += ":- ";
            foreach(var b in body)
            {
                result += b.ToString() + " ";
            }
            return result;
        }

        public object Clone()
        {
            Rule clone = new Rule();
            clone.ruleType = ruleType;
            clone.head = Utils.ListClone(head);
            clone.head = Utils.ListClone(body);
            clone.haveNot = haveNot;
            return clone;
        }
    }

    public enum RuleType
    {
        BasicRule,
        WeightRule
    }
}
