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
            Model testmodel = new Model();

            //fact
            Atom kk_b = new Atom("kk");
            kk_b.literals.Add(new Literal(L_Type.Const, "b"));
            Atom foo_a = new Atom("foo");
            foo_a.literals.Add(new Literal(L_Type.Const, "a"));

            testmodel.AddFact(foo_a);
            testmodel.AddFact(kk_b);

            //rule
            Rule rule1 = new Rule();
            Atom head1 = new Atom("foo");
            head1.literals.Add(new Literal(L_Type.Const, "b"));
            Atom body1 = new Atom("foo");
            body1.literals.Add(new Literal(L_Type.Const, "a"));
            rule1.AddBody(body1);
            rule1.AddHead(head1);

            Rule rule2 = new Rule();
            Atom head2 = new Atom("git");
            head2.literals.Add(new Literal(L_Type.Const, "b"));
            Atom body2 = new Atom("foo");
            body2.literals.Add(new Literal(L_Type.Const, "b"));
            rule2.AddBody(body2);
            rule2.AddHead(head2);

            Rule rule3 = new Rule();
            Atom head3 = new Atom("git");
            head3.literals.Add(new Literal(L_Type.Const, "c"));
            Atom body3 = new Atom("git");
            body3.literals.Add(new Literal(L_Type.Const, "b"));
            Atom body33 = new Atom("kk");
            body33.literals.Add(new Literal(L_Type.Const, "b"));
            rule3.AddBody(body3);
            rule3.AddBody(body33);
            rule3.AddHead(head3);

            Rule rule4 = new Rule();
            Atom head4 = new Atom("git");
            head4.literals.Add(new Literal(L_Type.Const, "c"));
            Atom head44 = new Atom("git");
            head44.literals.Add(new Literal(L_Type.Const, "e"));
            Atom body4 = new Atom("git");
            body4.literals.Add(new Literal(L_Type.Const, "d"));
            rule4.AddHead(head44);
            rule4.AddBody(body4);
            rule4.AddHead(head4);

            testmodel.AddRule(rule1);
            testmodel.AddRule(rule2);
            testmodel.AddRule(rule3);
            testmodel.AddRule(rule4);

            Console.WriteLine(testmodel);

            YFmodeler YFM = new YFmodeler(testmodel);
            YFM.run(1);
            Console.WriteLine(YFM.PrintModel());
            Console.WriteLine();
        }
    }
}
