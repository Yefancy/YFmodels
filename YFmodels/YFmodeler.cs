using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YFmodels
{
    public class YFmodeler
    {
        private YFProgram program;
        public List<Model> stableModel;

        public YFmodeler(YFProgram p)
        {
            stableModel = new List<Model>();
            program = p;
        }

        public bool RunModels()
        {
            Model basicModel = new Model();
            foreach (var r in program.rules)
                if (r.nBody.Count == 0 && r.pBody.Count == 0)
                    basicModel.trueList.Add(r.head.atom);
            if(!program.dic.ContainsKey(1))
                basicModel.falseList.Add(1);
            return DFSModels(program, basicModel);
        }

        private bool DFSModels(YFProgram program, Model partialModel)
        {

            partialModel = Expand(program, partialModel);


            if (partialModel.IsConflict()) return false;
            else if (partialModel.Count() == program.atoms.Count)
            {
                stableModel.Add((Model)partialModel.Clone());
                program.expect--;
                return true;
            }
            else
            {
                Model chosen = heuristic(program, partialModel);
                Model TM = (Model)partialModel.Clone();
                Model FM = (Model)partialModel.Clone();
                TM = TM + chosen;
                if (DFSModels(program, TM))
                    if (program.expect != 0)
                    {
                        return DFSModels(program, FM + (chosen.Converse()));
                    }
                    else return true;
                return DFSModels(program, FM + (chosen.Converse()));
            }
        }

        private Model Expand(YFProgram program, Model partialModel)
        {
            Model copy;
            do
            {
                copy = partialModel;
                program.reset();
                partialModel = Atleast(program, partialModel);
                //Console.WriteLine("\ncopy:" + copy);
                //Console.WriteLine("\nexpand:" + partialModel + "\n");
                var most = Atmost(program, partialModel);
                List<int> notList = new List<int>();
                foreach (var a in program.atoms)
                    if (!most.Contains(a))
                        if (!partialModel.falseList.Contains(a.atom))
                            partialModel.falseList.Add(a.atom);
                //Console.WriteLine("\nexpand:" + partialModel + "\n");
            } while (copy != partialModel);
            return partialModel;
        }

        private Model Atleast(YFProgram program, Model partialModel)
        {
            Queue<Atom> posq = new Queue<Atom>();
            Queue<Atom> negq = new Queue<Atom>();
            foreach (var num in partialModel.trueList)
                posq.Enqueue(program.atoms.Find((a) => { return a.atom == num; }));
            foreach (var num in partialModel.falseList)
                negq.Enqueue(program.atoms.Find((a) => { return a.atom == num; }));
            while (posq.Count != 0 || negq.Count != 0)
            {
                if (posq.Count != 0)
                {
                    Atom atom = posq.Dequeue();
                    atom.trueFlag = true;
                    for (int i = 0; i < atom.pList.Count; i++)
                        atom.pList[i].fire(atom,posq, negq);
                    for (int i = 0; i < atom.nList.Count; i++)
                        atom.nList[i].inactivate(posq, negq);
                    if (atom.headof == 1 && atom.hList.Count == 1)
                        atom.hList[0].BCT(posq, negq);
                }
                if (negq.Count != 0)
                {
                    Atom atom = negq.Dequeue();
                    atom.falseFlag = true;
                    for (int i = 0; i < atom.nList.Count; i++)
                        atom.nList[i].fire(atom,posq, negq);
                    for (int i = 0; i < atom.pList.Count; i++)
                        atom.pList[i].inactivate(posq, negq);
                    if (atom.headof > 0)
                        for (int i = 0; i < atom.hList.Count; i++)
                            atom.hList[i].BCF(posq, negq);
                }
            }
            Model back = new Model();
            foreach (var atom in program.atoms)
            {
                if (atom.trueFlag)
                    back.trueList.Add(atom.atom);
                if (atom.falseFlag)
                    back.falseList.Add(atom.atom);
            }
            return back;
        }

        private List<Atom> Atmost(YFProgram program, Model partialModel)
        {
            List<Atom> F = new List<Atom>();
            Queue<Atom> queue = new Queue<Atom>();
            foreach (var r in program.rules)
                queue.Enqueue(r.head);
            while (queue.Count != 0)
            {
                Atom atom = queue.Dequeue();
                atom.inUpper = atom.falseFlag ? false : true;
                if (atom.inUpper)
                {
                    for (int i = 0; i < atom.pList.Count; i++)
                        atom.pList[i].propagateFalse(queue);
                    atom.inUpper = false;
                    if (!F.Contains(atom))
                        F.Add(atom);
                }
            }
            for (int i = 0; i < F.Count; i++)
                for (int j = 0; j < F[i].hList.Count; j++)
                    if ((F[i].hList)[j].isUpperActive())
                        queue.Enqueue(F[i]);
            while (queue.Count != 0)
            {
                Atom atom = queue.Dequeue();
                if (!atom.inUpper && !atom.falseFlag)
                {
                    for (int i = 0; i < atom.pList.Count; i++)
                        atom.pList[i].propagateTrue(queue);
                    atom.inUpper = true;
                }
            }
            return F;
        }

        private Model heuristic(YFProgram program, Model partialModel)
        {
            Queue<int> leftq = new Queue<int>();
            foreach (var atom in program.atoms)
                if (!partialModel.trueList.Contains(atom.atom) && !partialModel.falseList.Contains(atom.atom))
                    leftq.Enqueue(atom.atom);
            int min = 0, max = 0;
            Model x = null;
            while (leftq.Count != 0)
            {
                int a = leftq.Dequeue();
                Model tmp = (Model)partialModel.Clone();
                tmp.trueList.Add(a);
                int p = (Expand(program, tmp) - partialModel).Count();
                if (p >= min)
                {
                    tmp = (Model)partialModel.Clone();
                    tmp.falseList.Add(a);
                    int n = (Expand(program, tmp) - partialModel).Count();
                    var _min = Math.Min(n, p);
                    var _max = Math.Max(n, p);
                    if (_min > min || (_min == min && _max > max))
                    {
                        min = _min;
                        max = _max;
                        x = new Model();
                        if (p == _max)
                            x.trueList.Add(a);
                        else
                            x.falseList.Add(a);
                    }
                }
            }
            return x;
        }
    }
}
