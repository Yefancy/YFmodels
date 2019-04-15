using System;
using System.Collections.Generic;
using System.Text;

namespace YFmodels
{
    public class YFProgram
    {
        public List<Rule> rules;
        public List<Atom> atoms;
        public int expect;
        public Dictionary<int, string> dic;

        public YFProgram(int expect = -1)
        {
            rules = new List<Rule>();
            atoms = new List<Atom>();
            dic = new Dictionary<int, string>();
            this.expect = expect;
        }

        public override string ToString()
        {
            string result = "";
            foreach (var rule in rules)
                result += rule.ToString()+"\n";
            return result;
        }

        public void reset()
        {
            for(int i = 0; i < rules.Count; i++)
            {
                rules[i].literal = rules[i].nBody.Count + rules[i].pBody.Count;
                rules[i].inactive = 0;
                rules[i].upper = 0;
                rules[i].avtiveLs.Clear();
            }
            for(int i = 0; i < atoms.Count; i++)
            {
                atoms[i].trueFlag = false;
                atoms[i].falseFlag = false;
                atoms[i].headof = atoms[i].hList.Count;
                atoms[i].inUpper = true;
            }
        }
    }
}
