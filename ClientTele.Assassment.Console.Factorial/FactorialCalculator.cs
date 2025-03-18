using System.Collections.Concurrent;
using System.Numerics;
using Serilog;

namespace ClientTele.Assassment.Console.Factorial
{
    /// <summary>
    /// 
    /// </summary>
    public class FactorialCalculator : IFactorialCalculator
    {
        /// <summary>
        /// ensures the thread safety of the calculation
        /// </summary>
        readonly object _lock = new object();

        static readonly ILogger _logger = new LoggerConfiguration()
            .WriteTo.File("logs/factorialCalculator.log", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .CreateLogger();

        /// <summary>
        /// Calculate the factorial of a number asynchronously.
        /// </summary>
        /// <param name="number">The number to calculate the factorial for. Must be non-negative.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the factorial of the number.</returns>
        /// <exception cref="ArgumentException">If not complied with non-negrative parameter.</exception>
        public Task<BigInteger> CalculateFactorialAsync(int number)
        {
            try
            {
                // check input parameter
                if (number < 0) throw new ArgumentException("Number must be non-negative");

                _logger.Information("Calculating factorial for: {Number}", number);

                // do calculation make sure it thread-safe with lock
                return Task.Run(() =>
                {
                    lock (_lock)
                    {
                        BigInteger result = 1;
                        for (int i = 2; i <= number; i++)
                        {
                            result *= i;
                        }
                        _logger.Information("Factorial calculated: {Number}! = {Result}", number, result);
                        return result;
                    }
                });

            }
            catch (Exception ex )
            {
                _logger.Error(ex, "Error calculating factorial for: {Number}", number);
                throw;
            }
        }


        /// <summary>
        /// Calculate the factorials of a list of numbers asynchronously.
        /// </summary>
        /// <param name="numbers">The array of numbers to calculate the factorials for. Each number must be non-negative.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a dictionary with the numbers and their corresponding factorials.</returns>
        public async Task<ConcurrentDictionary<int, BigInteger>> CalculateFactorials(int[] numbers)
        {
            try
            {
                _logger.Information("Calculating factorials for multiple numbers: {Numbers}", string.Join(", ", numbers));

                // create a dictionary of tasks for each number
                var tasks = numbers.ToDictionary(
                 number => number,
                 number => CalculateFactorialAsync(number)
                          );

                // saving the results in a concurrent dictionary(thread safe dictionary)
                var results = new ConcurrentDictionary<int, BigInteger>();
                foreach (var kvp in tasks)
                {
                    results[kvp.Key] = await kvp.Value;
                    _logger.Information("Factorial stored: {Number}! = {Result}", kvp.Key, results[kvp.Key]);
                }

                return results;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error calculating factorials for multiple numbers");
                throw;
            }
            
        }
    }
}
