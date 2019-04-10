using System;
using System.Collections.Generic;
using System.Text;

namespace YFmodels
{
    public class YFProgram
    {
        public List<Rule> rules;
        public List<Literal> facts;
        public int AtomCount;

        public YFProgram()
        {
            rules = new List<Rule>();
            facts = new List<Literal>();
        }
    }
}
