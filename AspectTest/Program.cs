namespace AspectTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var target = new Target();
            var sb = target.ListElements();
            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
    }
}