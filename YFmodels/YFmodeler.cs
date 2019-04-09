using System;
using System.Collections.Generic;
using System.Text;

namespace YFmodels
{
    public class YFmodeler
    {
        private Model basicModel;
        public List<Atom> stable_model;
        public Dictionary<string, int> ValueTable;//0-false 1-true 2-unknown

        public YFmodeler(Model model) { basicModel = model; ValueTable = new Dictionary<string, int>(); }

        public void run(int expect)
        {
            stable_model = meaning(basicModel);
        }


        private List<Atom> meaning(Model basicModel)
        {
            List<Atom> m_model = new List<Atom>();
            foreach (Atom item in basicModel.facts)
            {
                m_model.Add(item);
            }
            while (true)
            {
                bool flag = true;
                foreach(var rule in basicModel.rules)
                {
                    if (rule.haveNot) continue;
                    bool bodytrue = true;
                    foreach(var body in rule.body)
                    {
                        if (m_model.Exists(a => { return a == body; })) continue;
                        bodytrue = false;
                        break;
                    }
                    if (bodytrue)
                    {
                        foreach(var head in rule.head)
                        {
                            if (m_model.Exists(a => { return a == head; })) continue;
                            m_model.Add(head);
                            flag = false;
                        }
                    }
                }
                if (flag) break;
            }
            return m_model;
        }

        public string PrintModel()
        {
            string result = "stable model:";
            if (stable_model == null) return "have not run model";
            foreach (var item in stable_model)
            {
                result += item.ToString() + " ";
            }
            return result;
        }
    }
}
