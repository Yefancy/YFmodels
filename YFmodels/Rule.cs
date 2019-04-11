using System;
using System.Collections.Generic;

namespace YFmodels
{
    public class Rule : ICloneable
    {
        public Atom head;
        public List<Atom> pBody;
        public List<Atom> nBody;
        public int literal;
        public int inactive;
        public int upper;
        //public int inactive2;

        public Rule()
        {
            pBody = new List<Atom>();
            nBody = new List<Atom>();
        }

        public override string ToString()
        {
            string result = "";
            result += head.atom + ".";
            result += " :- ";
            foreach (var b in pBody)
                result += b.atom + ". ";
            foreach (var b in nBody)
                result += "not " + b.atom + ". ";
            return result;
        }

        public void fire(Queue<Atom> posq, Queue<Atom> negq)
        {
            literal--;
            if (literal == 0)
                posq.Enqueue(head);
            else if (head.falseFlag)
            {
                BCF(posq, negq);
            }
        }

        public void inactivate(Queue<Atom> posq, Queue<Atom> negq)
        {
            inactive++;
            if (inactive == 1)
            {
                head.headof--;
                if (head.headof == 0)
                    negq.Enqueue(head);
                else if (head.trueFlag && head.headof == 1)
                    BCT(posq, negq);

            }
        }

        public void BCT(Queue<Atom> posq, Queue<Atom> negq)
        {
            for (int i = 0; i < pBody.Count; i++)
                posq.Enqueue(pBody[i]);
            for (int i = 0; i < nBody.Count; i++)
                negq.Enqueue(nBody[i]);
        }

        public void BCF(Queue<Atom> posq, Queue<Atom> negq)
        {
            if (literal == 1 && inactive == 0)
            {
                for (int j = 0; j < pBody.Count; j++)
                {
                    Atom patom = pBody[j];
                    if (!patom.trueFlag)
                    {
                        negq.Enqueue(patom);
                        return;
                    }
                }
                for (int j = 0; j < nBody.Count; j++)
                {
                    Atom patom = nBody[j];
                    if (!patom.falseFlag)
                    {
                        posq.Enqueue(patom);
                        return;
                    }
                }
            }
        }

        public bool isUpperActive()
        {
            if (upper == 0 && inactive == 0)
                return true;
            else
                return false;
        }

        public void propagateFalse(Queue<Atom> queue)
        {
            upper++;
            if (upper == 1 && inactive == 0)
                queue.Enqueue(head);
        }

        public void propagateTrue(Queue<Atom> queue)
        {
            upper--;
            if (upper == 0 && inactive == 0)
                queue.Enqueue(head);
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
