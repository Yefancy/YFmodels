using System;
using System.Collections.Generic;
using System.Text;

namespace YFmodels
{
    public class Atom: ICloneable
    {
        public int atom;        
        public bool trueFlag;
        public bool falseFlag;
        public int headof;
        public bool inUpper;

        public List<Rule> hList;
        public List<Rule> pList;
        public List<Rule> nList;

        public Atom(int _atom,bool _trueFlag = false, bool _falseFlag = false, int _headof = -1)
        {
            atom = _atom;
            trueFlag = _trueFlag;
            falseFlag = _falseFlag;
            headof = _headof;
            hList = new List<Rule>();
            pList = new List<Rule>();
            nList = new List<Rule>();
        }

        public object Clone()
        {
            return new Atom(atom, trueFlag, falseFlag, headof);
        }
    }
}
