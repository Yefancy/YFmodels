using System;
using System.Collections.Generic;
using System.Text;

namespace YFmodels
{
    public class Literal:ICloneable
    {
        public L_Type type;
        public string content;
        
        public Literal(L_Type type = L_Type.Const,string content = "")
        {
            this.type = type;
            this.content = content;
        }

        public object Clone()
        {
            return new Literal(type, content);
        }

        public static bool operator ==(Literal a, Literal b)
        {
            if (a.type != b.type) return false;
            if (a.content != b.content) return false;
            return true;
        }

        public static bool operator !=(Literal a, Literal b)
        {
            return !(a == b);
        }
    }

    public enum L_Type
    {
        Variable,
        Const,
        Func
    }
}
