using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Threading;

namespace SampleApp
{
    internal class Test<TKey, TValue>
    {
        public void GenericMethod(TKey item)
        {
            Console.WriteLine($"Test.GenericMethod with types {typeof(TKey)} {typeof(TValue)}");
        }

        public void GenericMethodT<T>(T item)
        {
            Console.WriteLine($"Test.GenericMethodT with types {typeof(TKey)} {typeof(TValue)} {typeof(T)}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            new Test<int, double>().GenericMethod(1);
            new Test<int, double>().GenericMethodT(1.0);
            new Test<Program, List<int>>().GenericMethod(new Program());
            new Test<Program, List<int>>().GenericMethodT(new List<int>());
            new Test<int, int>().GenericMethod(1);
            new Test<int, int>().GenericMethodT(1);
            new Test<List<int>, List<int>>().GenericMethod(new List<int>());
            new Test<List<int>, List<int>>().GenericMethodT(new List<int>());
        }
    }
}