using System.Diagnostics;
using System.Text;

namespace Problem68
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int32 size = Int32.Parse(inputs[0]);
            Int32 sum = Int32.Parse(inputs[1]);
            List<Int32> digits = new List<Int32>();
            for (Int32 i = 1; i <= 2 * size; i++)
            {
                digits.Add(i);
            }
            List<Int32[]> possiblePolygons = TestSolve(digits, size, sum);
            List<string> outputs = new List<string>();
            foreach (var polygon in possiblePolygons)
            {
                string nextOutput = FormatOutput(polygon, size);
                outputs.Add(nextOutput);

            }
            outputs.Sort();
            foreach (var v in outputs)
            {
                Console.WriteLine(v);
            }
        }
        static List<Int32[]> TestSolve(List<Int32> digits, Int32 size, Int32 sum)
        {
            List<(List<Int32>, List<Int32>)>[] polygonList = new List<(List<Int32>, List<Int32>)>[size + 1];
            polygonList[1] = InitalizePolygonList(digits, size, sum);
            for (Int32 i = 2; i < size; i++)
            {
                polygonList[i] = new List<(List<Int32>, List<Int32>)>();
                foreach (var previousPolygon in polygonList[i - 1])
                {
                    List<Int32> previousInner = previousPolygon.Item1;
                    List<Int32> previousOuter = previousPolygon.Item2;
                    List<Int32> availableDigits = new List<Int32>(digits);
                    foreach (var digit in previousInner)
                    {
                        availableDigits.Remove(digit);
                    }
                    foreach (var digit in previousOuter)
                    {
                        availableDigits.Remove(digit);
                    }
                    foreach (var digit in availableDigits)
                    {
                        List<Int32> nextInner = new List<Int32>(previousInner);
                        nextInner.Add(digit);
                        Int32 nextInnerSize = nextInner.Count;
                        Int32 nextOuterDigit = sum - nextInner[nextInnerSize - 1] - nextInner[nextInnerSize - 2];
                        List<Int32> availableOuterDigits = new List<Int32>();
                        foreach(var v in availableDigits)
                        {
                            if (v > previousOuter.First())
                            {
                                availableOuterDigits.Add(v);
                            }
                        }
                        availableOuterDigits.Remove(digit);
                        if (availableOuterDigits.Contains(nextOuterDigit))
                        {
                            availableOuterDigits.Remove(nextOuterDigit);
                            List<Int32> nextOuter = new List<Int32>(previousOuter);
                            nextOuter.Add(nextOuterDigit);
                            if (OuterRingIsValid(nextOuter, availableOuterDigits, sum, size))
                            {
                                polygonList[i].Add((nextInner, nextOuter));
                            }
                        }
                    }
                }
            }
            List<Int32[]> outputs = new List<Int32[]>();
            foreach (var polygon in polygonList[size - 1])
            {
                List<Int32> inner = polygon.Item1;
                List<Int32> outer = polygon.Item2;
                Int32 finalOuter = sum - polygon.Item1.First() - polygon.Item1.Last();
                if (digits.Contains(finalOuter))
                {
                    if (!inner.Contains(finalOuter) && !outer.Contains(finalOuter))
                    {
                        outer.Add(finalOuter);
                        Int32[] nextOutput = new Int32[2 * size];
                        for(Int32 i = 0; i < size; i++)
                        {
                            nextOutput[i] = inner[i];
                            nextOutput[size + i] = outer[i];
                        }
                        outputs.Add(nextOutput);
                    }
                }
            }
            return outputs;
        }
        static List<(List<Int32>, List<Int32>)> InitalizePolygonList(List<Int32> digits, Int32 size, Int32 sum)
        {
            List<(List<Int32>, List<Int32>)> output = new List<(List<Int32>, List<Int32>)>();
            foreach(var firstDigit in digits)
            {
                foreach(var secondDigit in digits)
                {
                    if (secondDigit == firstDigit)
                    {
                        continue;
                    }
                    List<Int32> nextInner = new List<Int32>();
                    List<Int32> nextOuter = new List<Int32>();
                    nextInner.Add(firstDigit);
                    nextInner.Add(secondDigit);
                    Int32 outerDigit = sum - secondDigit - firstDigit;
                    if (outerDigit != firstDigit && outerDigit != secondDigit)
                    {
                        if (digits.Contains(outerDigit))
                        {
                            nextOuter.Add(outerDigit);
                            List<Int32> avilableDigits = new List<Int32>();
                            foreach(var v in digits)
                            {
                                if (v!= firstDigit && v!= secondDigit && v > outerDigit)
                                {
                                    avilableDigits.Add(v);
                                }
                            }
                            if (!OuterRingIsValid(nextOuter, avilableDigits, sum, size))
                            {
                                continue;
                            }
                            output.Add((nextInner, nextOuter));
                        }
                    }
                }
            }
            return output;
        }
        static string FormatOutput(Int32[] innerRing, Int32 size)
        {
            StringBuilder output = new StringBuilder();
            for (Int32 i = 0; i < size - 1; i++)
            {
                output.Append(innerRing[size + i].ToString());
                output.Append(innerRing[i].ToString());
                output.Append(innerRing[i + 1].ToString());
            }
            output.Append(innerRing[2 * size - 1].ToString());
            output.Append(innerRing[size - 1].ToString());
            output.Append(innerRing[0].ToString());
            return output.ToString();
        }
        static bool RingIsValid(List<Int32> ring, List<Int32> availableDigits, Int32 sum, Int32 size, Int32 targetSum)
        {
            Int32 initialSum = ring.Sum();
            Int32 currentSum = initialSum;
            if (currentSum > targetSum)
            {
                return false;
            }
            Int32 currentSize = ring.Count;
            Int32 digitsToAdd = size - currentSize;
            if (availableDigits.Count < digitsToAdd)
            {
                return false;
            }
            for (Int32 i = 0; i < digitsToAdd; i++)
            {
                currentSum += availableDigits[availableDigits.Count - 1 - i];
            }
            if (currentSum < targetSum)
            {
                return false;
            }
            currentSum = initialSum;
            for (Int32 i = 0; i < digitsToAdd; i++)
            {
                currentSum += availableDigits[i];
            }
            if (currentSum > targetSum)
            {
                return false;
            }
            return true;
        }
        static bool InnerRingIsValid(List<Int32> innerRing, List<Int32> availableDigits, Int32 sum, Int32 size)
        {
            Int32 targetSum = sum * size - size * (2 * size + 1);
            return RingIsValid(innerRing, availableDigits, sum, size, targetSum);
        }
        static bool OuterRingIsValid(List<Int32> outerRing, List<Int32> availableDigits, Int32 sum, Int32 size)
        {
            Int32 targetSum = 2 * size * (2 * size + 1) - sum * size;
            return RingIsValid(outerRing, availableDigits, sum, size, targetSum);
        }
    }
}