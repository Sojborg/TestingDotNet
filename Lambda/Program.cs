using System;

namespace Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> multipler = i => i * i;
            var result = multipler(2);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
