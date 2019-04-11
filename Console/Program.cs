using System;
using YFmodels;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("YFmodels");
            test1();
            Console.ReadLine();
        }

        static void test1()
        {
            YFProgram program = new YFProgram();
            //Atom
            Atom kk_b = new Atom(0);
            Atom foo_a = new Atom(1);
            Atom foo_b = new Atom(2);
            Atom git_b = new Atom(3);
            Atom git_c = new Atom(4);
            Atom git_d = new Atom(5);
            Atom git_e = new Atom(6);

            program.atoms.Add(kk_b);
            program.atoms.Add(foo_a);
            program.atoms.Add(foo_b);
            program.atoms.Add(git_b);
            program.atoms.Add(git_c);
            program.atoms.Add(git_d);
            program.atoms.Add(git_e);

            //fact
            Rule fact1 = new Rule();
            Rule fact2 = new Rule();
            fact1.head = kk_b;
            kk_b.hList.Add(fact1);
            fact2.head = foo_a;
            foo_a.hList.Add(fact2);

            //rule
            Rule rule1 = new Rule();           
            rule1.head = foo_b;
            foo_b.hList.Add(rule1);
            rule1.pBody.Add(foo_a);
            foo_a.pList.Add(rule1);

            Rule rule2 = new Rule();
            rule2.head = git_b;
            git_b.hList.Add(rule2);
            rule2.pBody.Add(foo_b);
            foo_b.pList.Add(rule2);

            Rule rule3 = new Rule();
            rule3.head = git_c;
            git_c.hList.Add(rule3);
            rule3.pBody.Add(kk_b);
            kk_b.pList.Add(rule3);
            rule3.pBody.Add(git_b);
            git_b.pList.Add(rule3);

            Rule rule4 = new Rule();
            rule4.head = git_e;
            git_e.hList.Add(rule4);
            rule4.pBody.Add(git_d);
            git_d.pList.Add(rule4);

            Rule rule5 = new Rule();
            rule5.head = git_c;
            git_c.hList.Add(rule5);
            rule5.pBody.Add(git_d);
            git_d.pList.Add(rule5);

            program.rules.Add(fact1);
            program.rules.Add(fact2);
            program.rules.Add(rule1);
            program.rules.Add(rule2);
            program.rules.Add(rule3);
            program.rules.Add(rule4);
            program.rules.Add(rule5);

            Atom a = new Atom(7);
            Atom b = new Atom(8);

            program.atoms.Add(a);
            program.atoms.Add(b);

            //fact

            //rule
            Rule rule6 = new Rule();
            rule6.head = a;
            a.hList.Add(rule6);
            rule6.nBody.Add(b);
            b.nList.Add(rule6);

            Rule rule7 = new Rule();
            rule7.head = b;
            b.hList.Add(rule7);
            rule7.nBody.Add(a);
            a.nList.Add(rule7);

            program.rules.Add(rule6);
            program.rules.Add(rule7);

            Console.WriteLine(program);
            YFmodeler test1 = new YFmodeler(program);
            test1.RunModels();
            var result = test1.stableModel;
           
        }

        static void test2()
        {
            YFProgram program = new YFProgram();
            //Atom
            Atom a = new Atom(0);
            Atom b = new Atom(1);

            program.atoms.Add(a);
            program.atoms.Add(b);

            //fact

            //rule
            Rule rule1 = new Rule();
            rule1.head = a;
            a.hList.Add(rule1);
            rule1.nBody.Add(b);
            b.nList.Add(rule1);

            Rule rule2 = new Rule();
            rule2.head = b;
            b.hList.Add(rule2);
            rule2.nBody.Add(a);
            a.nList.Add(rule2);

            program.rules.Add(rule1);
            program.rules.Add(rule2);

            Console.WriteLine(program);
            YFmodeler test2 = new YFmodeler(program);
            test2.RunModels();
            var result = test2.stableModel;

        }
    }
}
