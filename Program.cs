/*
You are climbing a staircase. It takes n steps to reach the top.
Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?

Example 1:
    Input: n = 2
    Output: 2
    Explanation: There are two ways to climb to the top.
        1. 1 step + 1 step
        2. 2 steps

Example 2:
    Input: n = 3
    Output: 3
    Explanation: There are three ways to climb to the top.
        1. 1 step + 1 step + 1 step
        2. 1 step + 2 steps
        3. 2 steps + 1 step 
*/

using System.Numerics;

namespace Assignment_5._3._2
{
    internal class Program
    {
        static Nullable<long>[] sequence = new Nullable<long>[0];
        static void Main(string[] args)
        {
            int n = 2;
            sequence = new Nullable<long>[n];
            DisplayResults(n);


            n = 3;
            sequence = new Nullable<long>[n];
            DisplayResults(n);

            n = 5;
            sequence = new Nullable<long>[n];
            DisplayResults(n);

            n = 7;
            sequence = new Nullable<long>[n];
            DisplayResults(n);
        }
        static int Fibonacci(int n)
        {
            if (n == 1)
            {
                sequence[0] = 1;
                return 1;
            }

            if (n == 2)
            {
                sequence[1] = 2;
                return 2;
            }

            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        static BigInteger BruteForce(int n)
        {
            // This is how I thought about it in my head before realizing it was just a Fibonacci sequence
            // after writing it out on Notepad. Decided to code it out anyway!

            #region Notepad Walkthrough
            /*
            1 step -- 1 combination
            [1]

            2 steps -- 2 combinations
            [2]
            [1,1]

            3 steps -- 3 combinations
            [2,1]
            [1,2]
            [1,1,1]

            4 steps -- 5 combinations
            [2,2]
            [2,1,1]
            [1,2,1]
            [1,1,2]
            [1,1,1,1]

            5 steps -- 8 combinations
            [2,2,1]
            [2,1,2]
            [1,2,2]
            [2,1,1,1]
            [1,2,1,1]
            [1,1,2,1]
            [1,1,1,2]
            [1,1,1,1,1]

            Fibonacci sequence: 1 2 3 5 8 13 21...
            */
            #endregion

            if (n == 0) return 0; // return 0 if no steps
            if (n == 1) return 1; // return 1 if 1 step
            
            // Create a dictionary holding the number of "1 Steps" taken and "2 Steps" taken
            var climbAmount = new Dictionary<int, int> { { 1, n }, { 2, 0 } };

            // Counter to hold number of possible ways to walk up the steps
            // Start at 1 for "all 1s" solution
            BigInteger waysCounter = 1;

            while (climbAmount[1] > 1)
            {
                // Convert two "1" to one "2"
                climbAmount[1] -= 2;
                climbAmount[2] += 1;

                // Calculate number of combinations (unfortunately, I had to Google this formula)
                waysCounter += CalculateWays(climbAmount[1], climbAmount[2]);
            }

            return waysCounter;
        }

        static BigInteger Factorial(int n)
        {
            BigInteger product = 1;

            for (int i = 2; i <= n; i++)
                product *= i;

            return product;
        }

        static BigInteger CalculateWays(int ones, int twos)
        {
            return Factorial(ones + twos) / (Factorial(ones)*Factorial(twos));
        }

        static int BinetFormula(int n)
        {
            // Googled this formula (not the code) after coming up with the other two solutions 
            // Binet's Formula is a way to computer Fibonacci numbers directly without recursion
            // Formula is: F(n) = 1/sqrt(5) * (phi^(n+1) - psi^(n+1))
            //      Where:
            //          phi = (1+sqrt(5))/2       psi = (1-sqrt(5))/2

            double phi = (1 + Math.Sqrt(5)) / 2;
            double psi = (1 - Math.Sqrt(5)) / 2;

            return (int)Math.Round((1 / Math.Sqrt(5)) * (Math.Pow(phi, n + 1) - Math.Pow(psi, n + 1)));
        }

        static void DisplayResults(int n)
        {
            // Method to easily display all different solution variations
            Console.WriteLine($"Input: {n} steps to reach the top.");
            Console.WriteLine($"Output: {Fibonacci(n)} ways to climb to the top.");
            Console.WriteLine("Brute-forced: " + BruteForce(n));
            Console.WriteLine("Binet's Formula: " + BinetFormula(n));
            Console.WriteLine();
        }
    }
}
