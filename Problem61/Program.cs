using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Problem61
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            string[] inputs = Console.ReadLine().Split();
            List<Int32> conditions = new List<Int32>();
            foreach (var v in inputs)
            {
                conditions.Add(Int32.Parse(v));
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            conditions = conditions.Distinct().ToList();
            PolygonalNumberHelper pnh = new PolygonalNumberHelper();
            HashSet<string> solutions = new HashSet<string>();
            Int64 totalPermutations = Factorial(conditions.Count);
            for(Int32 i = 0; i < totalPermutations; i++)
            {
                HashSet<string> nextOutput = pnh.GetSolutions(conditions);
                foreach (var output in nextOutput)
                {
                    if (!solutions.Contains(output))
                    {
                        solutions.Add(output);
                    }
                }
                NextPermutation(conditions);
            }
            sw.Stop();
            List<Int32> outputs = new List<Int32>();
            foreach (var v in solutions)
            {
                outputs.Add(SumOutput(v));
            }
            outputs.Sort();
            foreach (var v in outputs)
            {
                Console.WriteLine(v);
            }
            //Console.WriteLine(sw.ElapsedMilliseconds);
        }
        static void NextPermutation(List<Int32> input)
        {
            Int32 length = input.Count;
            Int32 pivotIndex = -1;
            for(Int32 i = length - 1; i > 0; i--)
            {
                if (input[i-1] < input[i])
                {
                    pivotIndex = i-1;
                    break;
                }
            }
            if (pivotIndex == -1)
            {
                input.Reverse();
                return;
            }
            for(Int32 i = length - 1; i > pivotIndex; i--)
            {
                if (input[i] > input[pivotIndex])
                {
                    Int32 temp = input[i];
                    input[i] = input[pivotIndex];
                    input[pivotIndex] = temp;
                    break;
                }
            }
            input.Reverse(pivotIndex + 1, length - pivotIndex - 1);
        }
        static Int64 Factorial(Int32 n)
        {
            Int64 output = 1;
            for(Int32 i = 1; i <= n; i++)
            {
                output *= i;
            }
            return output;
        }
        static IEnumerable<IEnumerable<Int32>> GetPermutations(List<Int32> list,Int32 length)
        {
            if (length == 1) return list.Select(t => new Int32[] { t });
            return GetPermutations(list,length-1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new Int32[] { t2 }));
        }
        static Int32 SumOutput(string output)
        {
            Int32 sum = 0;
            string[] summands = output.Split();
            foreach (var v in summands)
            {
                if (v == "")
                {
                    continue;
                }
                sum += Int32.Parse(v);
            }
            return sum;
        }
    }
    internal class PolygonalNumberHelper
    {
        readonly HashSet<Int32>[] polygonalNumberList;
        readonly Int32 numberOfConditions;
        public PolygonalNumberHelper()
        {
            polygonalNumberList = new HashSet<Int32>[9];
            for(Int32 i = 3; i <= 8; i++)
            {
                polygonalNumberList[i] = new HashSet<Int32>();
                for (Int32 j = 1; j <= 1000; j++)
                {
                    Int32 nextNumber = PolygonalNumer(j, i);
                    if (nextNumber >= 1000 && nextNumber < 10000)
                    {
                        polygonalNumberList[i].Add(nextNumber);
                    }
                }
            }
        }
        public HashSet<string> GetSolutions(List<Int32> conditions)
        {
            Int32 numberOfConditions = conditions.Count;
            List<List<Int32>>[] solutions = new List<List<Int32>>[numberOfConditions];
            solutions[0] = new List<List<Int32>>();
            Int32 condition = conditions[0];
            foreach(var nextElement in polygonalNumberList[condition])
            {
                List<Int32> nextSubset = new List<Int32>();
                nextSubset.Add(nextElement);
                solutions[0].Add(nextSubset);
            }
            for(Int32 conditionIndex = 1; conditionIndex < numberOfConditions; conditionIndex++)
            {
                solutions[conditionIndex] = new List<List<Int32>>();
                condition = conditions[conditionIndex];
                foreach (var previousSubset in solutions[conditionIndex - 1])
                {
                    foreach (var nextElement in polygonalNumberList[condition])
                    {
                        if (!previousSubset.Contains(nextElement) && AdmissablePair(previousSubset[conditionIndex-1], nextElement))
                        {
                            List<Int32> nextSubset = new List<Int32>(previousSubset);
                            nextSubset.Add(nextElement);
                            solutions[conditionIndex].Add(nextSubset);
                        }
                    }
                }
            }
            HashSet<string> output = new HashSet<string>();
            foreach (var set in solutions[numberOfConditions - 1])
            {
                if (!AdmissablePair(set[numberOfConditions - 1], set[0]))
                {
                    continue;
                }
                set.Sort();
                StringBuilder nextOutputBuilder = new StringBuilder();
                foreach (var number in set)
                {
                    nextOutputBuilder.Append(number);
                    nextOutputBuilder.Append(" ");
                }
                string nextOutput = nextOutputBuilder.ToString();
                if (!output.Contains(nextOutput))
                {
                    output.Add(nextOutput);
                }
            }
            return output;
        }
        static bool AdmissablePair(Int32 a,Int32 b)
        {
            if (a % 100 == b / 100)
            {
                return true;
            }
            return false;
        }
        static Int32 PolygonalNumer(Int32 n, Int32 polygonalType)
        {
            if (polygonalType == 3)
            {
                return n * (n + 1) / 2;
            }
            if (polygonalType == 4)
            {
                return n * n;
            }
            if (polygonalType == 5)
            {
                return n * (3 * n - 1) / 2;
            }
            if (polygonalType == 6)
            {
                return n * (2 * n - 1);
            }
            if (polygonalType == 7)
            {
                return n * (5 * n - 3) / 2;
            }
            return n * (3 * n - 2);
        }
    }
}