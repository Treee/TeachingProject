using System;
using System.Linq;
using TeachingProject.Topics;

namespace TeachingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //GenericsStuff();
            //FunThings();
        }

        public static void GenericsStuff()
        {
            var g = new GenericsPlayground();
            g.ShowSingleObjectMapping();
            //g.ShowManyObjectMapping();
            //g.ShowDataReaderMapping();
        }

        public static void FunThings()
        {
            var v = new FunThings();
            v.Foo = "my first string";
            Console.WriteLine(v.Foo);

            v.Foo1 = 42;
            Console.WriteLine(v.Foo1);

            v.Foo2 = false;
            Console.WriteLine(v.Foo2);

            Console.ReadLine();
        }
    }
}
