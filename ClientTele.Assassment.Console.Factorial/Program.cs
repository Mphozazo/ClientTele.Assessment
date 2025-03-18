using System.Collections.Concurrent;
using System.Numerics;

namespace ClientTele.Assassment.Console.Factorial
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var calculator = new FactorialCalculator();

            // CalculateFactorialAsync
            int number = 5;
            BigInteger factorial = await calculator.CalculateFactorialAsync(number);
            System.Console.WriteLine($"Factorial of {number} is {factorial}");

            System.Console.WriteLine($"========================================");
            // CalculateFactorials
            int[] numbers = { 3, 4, 5 };
            ConcurrentDictionary<int, BigInteger> factorials = await calculator.CalculateFactorials(numbers);
            foreach (var kvp in factorials)
            {
                System.Console.WriteLine($"Factorial of {kvp.Key} is {kvp.Value}");
            }
        }
    }
}