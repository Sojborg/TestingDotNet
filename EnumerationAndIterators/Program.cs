using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EnumerationAndIterators
{
    class Program
    {
        static void Main(string[] args)
        {
            var runners = new List<IRunBasic> {new BasicEnumerator(), new BasicIterator(), new ComposableIterator()};
            foreach (var runner in runners)
            {
                runner.Run();
            }
            Console.ReadLine();
        }
    }

    class BasicEnumerator : IRunBasic
    {
        public void Run()
        {
            Console.WriteLine("Basic enumerator");
            using (var enumerator = "beer".GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var element = enumerator.Current;
                    Console.WriteLine(element);
                }
            }
        }
    }

    class BasicIterator : IRunBasic
    {
        public void Run()
        {
            Console.WriteLine("Basic iterator");
            foreach (var s in FooList())
            {
                Console.WriteLine(s);
            }
        }

        public static IEnumerable<string> FooList()
        {
            yield return "one";
            yield return "two";
            yield return "three";
        }
    }

    class ComposableIterator : IRunBasic {
        public void Run()
        {
            Console.WriteLine("Composable iterator");
            var list = Fibs(6).EvenNumberOnly();
            foreach (var fib in list)//EvenNumberOnly(Fibs(6)))
            {
                Console.WriteLine(fib);
            }
        }

        private IEnumerable<int> Fibs(int fibCount)
        {
            for (int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++)
            {
                yield return prevFib;
                int newFib = prevFib + curFib;
                prevFib = curFib;
                curFib = newFib;
            }
        }

        private static IEnumerable<int> EvenNumberOnly( IEnumerable<int> sequence)
        {
            foreach (int x in sequence)
            {
                if ((x%2) == 0)
                    yield return x;
            }
        }
    }

    public static class Extensions
    {
        public static IEnumerable<int> EvenNumberOnly(this IEnumerable<int> sequence)
        {
            foreach (int x in sequence)
            {
                if ((x % 2) == 0)
                    yield return x;
            }
        }
    }

    public interface IRunBasic
    {
        void Run();
    }
}
