using System;
using System.Collections.Generic;

namespace YFmodels
{
    public class Rule:ICloneable
    {
        public Literal head;
        public List<Literal> pBody;
        public List<Literal> nBody;
        public int literal;
        public int inactive;

        public Rule(Literal head = new Literal())
        {
            this.head = head;
            pBody = new List<Literal>();
            nBody = new List<Literal>();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
