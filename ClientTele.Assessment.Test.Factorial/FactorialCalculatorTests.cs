using ClientTele.Assassment.Console.Factorial;
using System.Collections.Concurrent;
using System.Numerics;

namespace ClientTele.Assessment.Test.Factorial
{
    public class FactorialCalculatorTests
    {
        private readonly FactorialCalculator _calculator = new();

        /// <summary>
        /// Testing Multiple Factorials
        /// </summary>
        /// <param name="number"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 6)]
        [InlineData(4, 24)]
        [InlineData(5, 120)]
        public async Task CalculateFactorialAsync_ValidInput_CorrectResult(int number, BigInteger expected)
        {
            // Act
            BigInteger result = await _calculator.CalculateFactorialAsync(number);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Testing Negative Factorial
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CalculateFactorialAsync_NegativeInput_ThrowsArgumentException()
        {
            // Arrange
            int number = -1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _calculator.CalculateFactorialAsync(number));
        }

        /// <summary>
        /// Testing Multiple Factorials
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CalculateFactorials_ValidInput_ReturnsCorrectResults()
        {
            // Arrange
            int[] numbers = { 3, 4, 5 };
            var expected = new Dictionary<int, BigInteger>
            {
                { 3, 6 },
                { 4, 24 },
                { 5, 120 }
            };

            // Act
            ConcurrentDictionary<int, BigInteger> results = await _calculator.CalculateFactorials(numbers);

            // Assert
            foreach (var (key, value) in expected)
            {
                Assert.Equal(value, results[key]);
            }
        }

        /// <summary>
        /// Testing Concurrent
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CalculateFactorials_ConcurrentExecution_ReturnsCorrectResults()
        {
            // Arrange
            int[] numbers = { 3, 4, 5, 6, 7, 8, 9, 10 };
            var expected = new Dictionary<int, BigInteger>
            {
                { 3, 6 },
                { 4, 24 },
                { 5, 120 },
                { 6, 720 },
                { 7, 5040 },
                { 8, 40320 },
                { 9, 362880 },
                { 10, 3628800 }
            };

            // Act
            ConcurrentDictionary<int, BigInteger> results = await _calculator.CalculateFactorials(numbers);

            // Assert - All results are correct
            foreach (var (key, value) in expected)
            {
                Assert.Equal(value, results[key]);
            }
        }
    }
}
