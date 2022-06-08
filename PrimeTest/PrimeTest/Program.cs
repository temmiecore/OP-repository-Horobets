using System;

namespace PrimeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(NewPrime(100));
            Console.WriteLine(NewPrime2(100));
        }
        static int NewPrime(int old)
        {
            for (int i = old; i >= 2; --i)
                if (IsPrime(i))
                    return i;
            return -1;
        }
        static int NewPrime2(int old)
        {
            for (int i = old; i >= 2; i--)
                if (IsPrime(i))
                    return i;
            return -1;
        }
        static bool IsPrime(int value)
        {
            if (value <= 1)
                return false;
            else if (value % 2 == 0)
                return value == 2;

            int n = (int)(Math.Sqrt(value) + 0.5);

            for (int i = 3; i <= n; i += 2)
                if (value % i == 0)
                    return false;

            return true;
        }
    }
}
