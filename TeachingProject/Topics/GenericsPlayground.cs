using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TeachingProject.Helper;

namespace TeachingProject
{
    public class GenericsPlayground
    {
        public GenericsPlayground()
        {
        }

        public void ShowDataReaderMapping()
        {
            var dataSet = CreateDataSet(3);
            var classAList = dataSet
                .CreateDataReader()
                .FromDataTableReader<ClassA>()
                .ToList();
            PrintAllClassA(classAList);

            var classBList = dataSet
                .CreateDataReader()
                .FromDataTableReader<ClassB>()
                .ToList();
            PrintAllClassB(classBList);

            var classCList = dataSet
                .CreateDataReader()
                .FromDataTableReader<ClassC>()
                .ToList();
            PrintAllClassC(classCList);

            Console.WriteLine("Press Any Key to Exit.");
            Console.ReadLine();
        }

        public void ShowSingleObjectMapping()
        {
            var classA = CreateClassA("test a", "test b", "test c");
            Console.WriteLine("Class A");
            Console.WriteLine(classA.ToString());
            var classB = classA.MapTo<ClassA, ClassB>();
            Console.WriteLine("Class A mapped to Class B (newly created)");
            Console.WriteLine(classB.ToString());
            Console.WriteLine("Try mapping class B back to a class A, does it work?");
            Console.WriteLine("Press Any Key To Exit.");
            Console.ReadLine();
        }

        public void ShowManyObjectMapping()
        {
            var classAList = CreateClassAList(3);
            foreach (var v in classAList)
            {
                Console.WriteLine(v.ToString());
            }
            var classBList = classAList.MapTo<ClassA, ClassB>().ToList();
            foreach (var v in classBList)
            {
                Console.WriteLine(v.ToString());
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        public DataSet CreateDataSet(int numItems)
        {
            var ds = new DataSet();
            ds.Tables.Add();
            var dt = ds.Tables[0];
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C");
            dt.Columns.Add("D");
            dt.Columns.Add("E");
            dt.Columns.Add("F");

            for (int i = 0; i < numItems; i++)
            {
                dt.Rows.Add($"A{i}", $"B{i}", $"C{i}", $"D{i}", $"E{i}", $"F{i}");
            }
            return ds;
        }

        private ClassA CreateClassA(string a, string b, string c)
        {//sugar for saying var v = new ClassA(); v.A = a; v.B = b; v.C = c;
            return new ClassA
            {
                A = a,
                B = b,
                C = c
            };
        }

        public void PrintAllClassA(List<ClassA> objects)
        {
            foreach (var v in objects)
            {
                Console.WriteLine(v.ToString());
            }
        }

        public void PrintAllClassB(List<ClassB> objects)
        {
            foreach (var v in objects)
            {
                Console.WriteLine(v.ToString());
            }
        }


        public void PrintAllClassC(List<ClassC> objects)
        {
            foreach (var v in objects)
            {
                Console.WriteLine(v.ToString());
            }
        }


        private List<ClassA> CreateClassAList(int numObjects)
        {
            var list = new List<ClassA>();
            for (int i = 0; i < numObjects; i++)
            {
                list.Add(CreateClassA($"a{i}", $"b{i}", $"c{i}"));
            }
            return list;
        }

        private ClassB CreateClassB(string a, string b, string d, string c = "default C")
        {//sugar for saying var v = new ClassA(); v.A = a; v.B = b; v.C = c;
            return new ClassB()
            {
                A = a,
                B = b,
                C = c,
                D = d
            };
        }

        private List<ClassB> CreateClassBList(int numObjects)
        {
            var list = new List<ClassB>();
            for (int i = 0; i < numObjects; i++)
            {
                list.Add(CreateClassB($"a{i}", $"b{i}", $"c{i}"));
            }
            return list;
        }

        private ClassC CreateClassC(string b, string e, string f)
        {//sugar for saying var v = new ClassA(); v.A = a; v.B = b; v.C = c;
            return new ClassC
            {
                B = b,
                E = e,
                F = f
            };
        }

        private List<ClassC> CreateClassCList(int numObjects)
        {
            var list = new List<ClassC>();
            for (int i = 0; i < numObjects; i++)
            {
                list.Add(CreateClassC($"b{i}", $"e{i}", $"f{i}"));
            }
            return list;
        }

    }

    public class ClassA
    {
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }

        public override string ToString()
        {
            return $"A: {A} B: {B} C: {C}";
        }
    }

    public class ClassB
    {
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; } = "Default D";

        public override string ToString()
        {
            return $"A: {A} B: {B} C: {C} D: {D}";
        }
    }

    public class ClassC
    {
        public string B { get; set; }
        public string E { get; set; }
        public string F { get; set; }

        public override string ToString()
        {
            return $"B: {B} E: {E} F: {F}";
        }
    }
}
