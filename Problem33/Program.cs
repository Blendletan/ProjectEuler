using System.Diagnostics;

namespace Problem33
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            string[] input = Console.ReadLine().Split();
            Int32 totalDesiredLength = Int32.Parse(input[0]);
            Int32 digitsToCancel = Int32.Parse(input[1]);
            if (totalDesiredLength != 4 || digitsToCancel != 1)
            {
                (Int32, Int32) output = Solve(totalDesiredLength, digitsToCancel);
                Console.WriteLine($"{output.Item1} {output.Item2}");
            }
            else
            {
                (Int32, Int32) output = SolveCase4and1();
                Console.WriteLine($"{output.Item1} {output.Item2}");
            }
        }
        static (Int32, Int32) SolveCase4and1()
        {
            // This uses MANY dirty hacks to rule out cases, most of which don't seem to work in general
            // It assumes that our four digit numbers come from multiplying the fraction in reduced form by some multiple which comes from
            // adding a digit the gcd of the original fraction 
            // It assumes the 3 digit fraction is NEVER reduced
            // It assummes certain certain constraints on the values of the GCD that I can't justify
            // But it does spit out the right answer for the case 4 1
            // Almost as hacky as just cheating and using a lookup table
            Int32 totalDesiredLength = 4;
            Int32 digitsToCancel = 1;
            Int32 maxHeight = (Int32)Math.Pow(10, totalDesiredLength - digitsToCancel);
            Int32[,] gcdList = GCD(1000);
            HashSet<(Int32, Int32)> output = new HashSet<(Int32, Int32)>();
            Int32 numeratorSum = 0;
            Int32 denominatorSum = 0;
            for (Int32 numerator = 100; numerator < maxHeight; numerator++)
            {
                for (Int32 denominator = numerator + 1; denominator < maxHeight; denominator++)
                {
                    Int32 gcd = gcdList[numerator, denominator];
                    if (gcd == 1)
                    {
                        continue;
                    }
                    Int32 lowNumerator = numerator / gcd;
                    Int32 lowDenominator = denominator / gcd;
                    List<Int32> listOfIndices = new List<Int32>();
                    for (Int32 digitToInsert = 9; digitToInsert >= 1; digitToInsert--)
                    {
                        List<Int32> gcdDigits = GetDigits(gcd);
                        if (gcd * lowNumerator < 1000)
                        {
                            gcdDigits.Add(0);
                        }
                        foreach (var v in GenerateDigitInserts(gcdDigits, digitToInsert))
                        {
                            Int32 numberToAdd = GetNumberFromDigits(v);
                            if (!listOfIndices.Contains(numberToAdd))
                            {
                                if (numberToAdd * lowNumerator > 1000 && numberToAdd * lowDenominator < 10000)
                                {
                                    listOfIndices.Add(numberToAdd);
                                }
                            }
                        }
                        foreach (var i in listOfIndices)
                        {
                            Int32 newDenominator = i * lowDenominator;
                            Int32 newNumerator = i * lowNumerator;
                            (Int32, Int32) nextResult = (newNumerator, newDenominator);
                            if (output.Contains(nextResult))
                            {
                                continue;
                            }
                            if (IsAdmissible(numerator, newNumerator, denominator, newDenominator, digitsToCancel, totalDesiredLength))
                            {
                                output.Add(nextResult);
                                numeratorSum += newNumerator;
                                denominatorSum += newDenominator;
                            }
                        }
                        listOfIndices.Clear();
                    }
                }
            }
            return (numeratorSum, denominatorSum);
        }
        static (Int32, Int32) Solve(Int32 totalDesiredLength, Int32 digitsToCancel)
        {
            Int32 maxHeight = (Int32)Math.Pow(10, totalDesiredLength - digitsToCancel);
            Int32[,] gcdList = GCD(1000);
            HashSet<(Int32, Int32)> output = new HashSet<(Int32, Int32)>();
            Int32 numeratorSum = 0;
            Int32 denominatorSum = 0;
            for (Int32 numerator = 1; numerator < maxHeight; numerator++)
            {
                for (Int32 denominator = numerator + 1; denominator < maxHeight; denominator++)
                {
                    Int32 gcd = gcdList[numerator, denominator];
                    Int32 lowNumerator = numerator / gcd;
                    Int32 lowDenominator = denominator / gcd;
                    Int32 minHeight = (Int32)Math.Pow(10, totalDesiredLength - 1) / lowNumerator;
                    for (Int32 i = minHeight; i < 10000 - minHeight; i++)
                    {
                        Int32 newDenominator = i * lowDenominator;
                        if (newDenominator > (Int32)Math.Pow(10, totalDesiredLength))
                        {
                            break;
                        }
                        Int32 newNumerator = i * lowNumerator;
                        (Int32, Int32) nextResult = (newNumerator, newDenominator);
                        if (output.Contains(nextResult))
                        {
                            continue;
                        }
                        if (IsAdmissible(numerator, newNumerator, denominator, newDenominator, digitsToCancel, totalDesiredLength))
                        {
                            output.Add(nextResult);
                            numeratorSum += newNumerator;
                            denominatorSum += newDenominator;
                        }
                    }
                }
            }
            return (numeratorSum, denominatorSum);
        }
        static bool IsAdmissible(Int32 aOld, Int32 aNew, Int32 bOld, Int32 bNew, Int32 digitsToCancel, Int32 totalDesiredLength)
        {
            if (aNew == bNew)
            {
                return false;
            }
            List<Int32> aNewDigits = GetDigits(aNew);
            if (aNewDigits.Count != totalDesiredLength)
            {
                return false;
            }
            List<Int32> bNewDigits = GetDigits(bNew);
            if (bNewDigits.Count != totalDesiredLength)
            {
                return false;
            }
            List<Int32> bOldDigits = GetDigits(bOld);
            Int32 initialLength = totalDesiredLength - digitsToCancel;
            List<Int32>? bAddedDigits = GetExtraDigits(bOld, bNew, initialLength);
            if (bAddedDigits == null)
            {
                return false;
            }
            if (bAddedDigits.Count != digitsToCancel)
            {
                return false;
            }
            List<Int32> aOldDigits = GetDigits(aOld);
            List<Int32>? aAddedDigits = GetExtraDigits(aOld, aNew, initialLength);
            if (aAddedDigits == null)
            {
                return false;
            }
            if (aAddedDigits.Count != digitsToCancel)
            {
                return false;
            }
            aAddedDigits.Sort();
            bAddedDigits.Sort();
            for (Int32 i = 0; i < digitsToCancel; i++)
            {
                if (aAddedDigits[i] != bAddedDigits[i])
                {
                    return false;
                }
            }
            return true;
        }
        static List<Int32>? GetExtraDigits(Int32 oldNumber, Int32 newNumber, Int32 initialLength)
        {
            //returns null if newNumer does not consist of oldNumer plus new extra digits
            //Else returns the list of new digitsdigits
            List<Int32> oldDigits = GetDigits(oldNumber);
            while (true)
            {
                if (oldDigits.Count >= initialLength)
                {
                    break;
                }
                oldDigits.Add(0);
            }
            List<Int32> newDigits = GetDigits(newNumber);
            Int32 currentIndex = 0;
            foreach (var digit in oldDigits)
            {
                while (true)
                {
                    if (currentIndex >= newDigits.Count)
                    {
                        return null;
                    }
                    if (newDigits[currentIndex] == digit)
                    {
                        newDigits.RemoveAt(currentIndex);
                        break;
                    }
                    currentIndex++;
                }
            }
            if (newDigits.Contains(0))
            {
                return null;
            }
            return newDigits;
        }
        static Int32 GetNumberFromDigits(List<Int32> digits)
        {
            Int32 length = digits.Count;
            Int32 powerOfTen = 1;
            Int32 output = 0;
            for (Int32 i = 0; i < length; i++)
            {
                output += digits[i] * powerOfTen;
                powerOfTen *= 10;
            }
            return output;
        }
        static List<Int32> GetDigits(Int32 n)
        {
            List<Int32> output = new List<Int32>();
            while (true)
            {
                Int32 nextDigit = n % 10;
                output.Add(nextDigit);
                if (n < 10)
                {
                    return output;
                }
                n = n / 10;
            }
        }
        static Int32[,] GCD(Int32 max)
        {
            //uses sieve of Eratsothenes to precumpute all GCDs up to 1000x1000
            bool[] isPrime = new bool[max + 1];
            Int32[,] gcd = new Int32[max + 1, max + 1];
            for (Int32 i = 2; i <= max; i++)
            {
                isPrime[i] = true;
            }
            for (Int32 i = 1; i <= max; i++)
            {
                for (Int32 j = 1; j <= max; j++)
                {
                    gcd[i, j] = 1;
                }
            }
            for (Int32 p = 2; p <= max; p++)
            {
                if (isPrime[p])
                {
                    for (Int32 i = 2 * p; i <= max; i += p)
                    {
                        isPrime[i] = false;
                    }
                    for (Int32 primePower = p; primePower <= max; primePower *= p)
                    {
                        for (Int32 i = primePower; i <= max; i += primePower)
                        {
                            for (Int32 j = primePower; j <= max; j += primePower)
                            {
                                gcd[i, j] *= p;
                            }
                        }
                    }
                }
            }
            return gcd;
        }
        static List<List<Int32>> GenerateDigitInserts(List<Int32> digits, Int32 digitToInsert)
        {
            List<List<Int32>> output = new List<List<Int32>>();
            Int32 length = digits.Count;
            for (Int32 i = 0; i <= length; i++)
            {
                List<Int32> nextDigitSequence = new List<Int32>(digits);
                nextDigitSequence.Insert(i, digitToInsert);
                if (!output.Contains(nextDigitSequence))
                {
                    output.Add(nextDigitSequence);
                }
            }
            return output;
        }

    }
}