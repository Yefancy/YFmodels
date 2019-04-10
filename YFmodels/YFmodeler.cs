using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YFmodels
{
    public class YFmodeler
    {
        private YFProgram program;

        public YFmodeler(YFProgram p)
        {
            program = p;
        }

        public void RunModels()
        {

        }

        private bool DFSModels(YFProgram program, Model partialModel)
        {
            partialModel = Expand(program, partialModel);
            if (partialModel.IsConflict()) return false;
            else if (partialModel.atoms.Count == program.AtomCount)
            {
                if (CheckReduct(program, partialModel))
                    return true;
                return false;
            }
            else
            {
                int literal = heuristic(program, partialModel);
                Atom atom = new Atom();
                atom.atom = literal; atom.falseFlag = false; atom.trueFlag = true;
                Model TM = (Model)partialModel.Clone();
                TM.atoms.Add(atom);
                if (DFSModels(program, TM)) return true;
                else
                {
                    atom.atom = literal; atom.falseFlag = true; atom.trueFlag = false;
                    Model FM = (Model)partialModel.Clone();
                    FM.atoms.Add(atom);
                    return DFSModels(program, FM);
                }
            }
        }

        private Model Expand(YFProgram program, Model partialModel)
        {
            do
            {
                Model copy = (Model)partialModel.Clone();
                partialModel = Atleast(program, partialModel);
            } while (true);
            throw new Exception();
        }

        private bool Conflict(Model partialModel)
        {

            return false;
        }

        private bool CheckReduct(YFProgram program, Model partialModel)
        {
            return true;
        }

        private int heuristic(YFProgram program, Model partialModel)
        {

            return -1;
        }
    }
}
