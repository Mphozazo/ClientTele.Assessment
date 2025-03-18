using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClientTele.Assassment.Console.Factorial
{
    /// <summary>
    /// making sure the interface is implemented with fully async methods 
    /// </summary>
    public interface IFactorialCalculator
    {
        /// <summary>
        /// Calculate the factorial of a number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        Task<BigInteger> CalculateFactorialAsync(int number);

        /// <summary>
        /// Calculate the factorial of a list of numbers
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        Task<ConcurrentDictionary<int, BigInteger>> CalculateFactorials(int[] numbers);
    }
}
