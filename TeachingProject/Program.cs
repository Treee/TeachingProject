namespace TeachingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericsStuff();
        }

        public static void GenericsStuff()
        {
            var g = new GenericsPlayground();
            g.ShowSingleObjectMapping();
            //g.ShowManyObjectMapping();
            //g.ShowDataReaderMapping();
        }
    }
}
