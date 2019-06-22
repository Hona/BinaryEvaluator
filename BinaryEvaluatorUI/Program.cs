using System;
using BinarySolver;

namespace BinaryEvaluatorUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                var input = Console.ReadLine();
                var evaluated = Evaluator.Evaluate(input);
                Console.WriteLine(evaluated);
            }
        }
    }
}