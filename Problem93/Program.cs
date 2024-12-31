using System.Numerics;
namespace Problem93
{
    internal class Program
    {
        static double epsilon = 0.00001;
        private static void Eval(List<double> numbers, bool[] used)
        {
            if (numbers.Count() == 1)
            {
                double result = numbers[0] + epsilon;
                if (Math.Abs(result % 1) > 10 * epsilon)
                {
                    return;
                }
                Int32 index = (Int32)(result + epsilon);
                if (index >= 0 && index < used.Length)
                {
                    used[index] = true;
                }
                return;
            }
            List<Double> next;
            for (Int32 i = 0; i < numbers.Count(); i++)
            {
                for (Int32 j = i + 1; j < numbers.Count(); j++)
                {
                    double a = numbers[i];
                    double b = numbers[j];
                    next = new List<double>(numbers);
                    next.RemoveAt(j);
                    next.RemoveAt(i);
                    next.Add(a + b);
                    Eval(next, used);
                    next[next.Count() - 1] = a - b;
                    Eval(next, used);
                    next[next.Count() - 1] = b - a;
                    Eval(next, used);
                    next[next.Count() - 1] = a * b;
                    Eval(next, used);
                    if (b != 0)
                    {
                        next[next.Count() - 1] = a / b;
                        Eval(next, used);
                    }
                    if (a != 0)
                    {
                        next[next.Count() - 1] = b / a;
                        Eval(next, used);
                    }
                }
            }
        }
        private static Int32 GetSequenceLength(List<Double> numbers)
        {
            bool[] used = new bool[1000];
            Eval(numbers, used);
            Int32 result = 0;
            while (used[result + 1] == true)
            {
                result++;
            }
            return result;
        }
        public static void Main(String[] args)
        {
            Int32 numDigits = Int32.Parse(Console.ReadLine());
            List<Double> numbers = new List<Double>();
            string[] inputs = Console.ReadLine().Split();
            for (Int32 i = 0; i < numDigits; i++)
            {
                numbers.Add(double.Parse(inputs[i]));
            }
            Console.WriteLine(GetSequenceLength(numbers));
        }
    }
}