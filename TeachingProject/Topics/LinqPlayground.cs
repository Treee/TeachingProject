using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TeachingProject.Helper;

namespace TeachingProject.Topics
{
    public class LinqPlayground
    {

        public MyItem MapYourItemToMine(YourItem yours)
        {
            var temp = new MyItem
            {
                MyId = yours.MyId,
                MyBool = yours.MyBool,
                MyName = yours.MyName
            };
            return temp;
        }

        //public void MyThing(Expression<Func<string, bool>> myFunc)
        //{
        //    myFunc.Parameters.Select(p => p.)

        //    myFunc.Compile()("foo");
        //}

        public LinqPlayground()
        {
            var thridItem = List.Where(x => x.MyId == 3).ToList();

            var firstItem = List.Where(x => x.MyName.Equals("MyItem1")).ToList();
            
            var yours = new YourItem
            {
                MyId = 100,
                MyBool = false,
                MyName = "Testies",
                SecretNumber = 42
            };

            var nowMine = MapYourItemToMine(yours);

            var nowMine1 = yours.MapYourItemToMine1();

            var s = "stuff";

            s.MutateString();


            MyThing(s3 => string.IsNullOrEmpty(s3));

        }

        public List<MyItem> List = new List<MyItem>
            {
                new MyItem
                {
                    MyId = 1,
                    MyBool = true,
                    MyName = "MyItem1"
                },
                new MyItem
                {
                    MyId = 2,
                    MyBool = true,
                    MyName = "MyItem2"
                },
                new MyItem
                {
                    MyId = 3,
                    MyBool = true,
                    MyName = "MyItem3"
                },
                new MyItem
                {
                    MyId = 4,
                    MyBool = true,
                    MyName = "MyItem4"
                }
            };
    }

    public class MyItem
    {
        public int MyId { get; set; }
        public string MyName { get; set; }
        public bool MyBool { get; set; }
    }

    public class YourItem
    {
        public int MyId { get; set; }
        public string MyName { get; set; }
        public bool MyBool { get; set; }
        public int SecretNumber { get; set; }
    }


}
