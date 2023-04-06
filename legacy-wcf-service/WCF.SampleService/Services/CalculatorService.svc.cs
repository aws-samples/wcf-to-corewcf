using System;
using System.ServiceModel;
using WCF.SampleService.Contracts;

namespace WCF.SampleService.Services
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(int n1, int n2)
        {
            return n1 + n2;
        }

        public int Subtract(int n1, int n2)
        {
            return n1 - n2;
        }

        public int Multiply(int n1, int n2)
        {
            return n1 * n2;
        }

        public int Divide(int n1, int n2)
        {
            try
            {
                return n1 / n2;
            }
            catch (DivideByZeroException)
            {
                throw new FaultException("Invalid Argument: The second argument must not be zero.");
            }
        }

        public int Factorial(int n)
        {
            if (n < 1)
                throw new FaultException("Invalid Argument: The argument must be greater than zero.");

            int factorial = 1;
            for (int i = 1; i <= n; i++)
            {
                factorial = factorial * i;
            }
            return factorial;
        }
    }
}
