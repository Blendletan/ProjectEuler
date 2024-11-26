using System.Numerics;
namespace Problem80
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 n = Int32.Parse(Console.ReadLine());
            Int32 numberOfDigits = Int32.Parse(Console.ReadLine());
            List<Int32> primeList = GeneratePrimes(n);
            Dictionary<Int32, BigInteger> sqrtCache = GeneratePrimeRoots(primeList, numberOfDigits);
            List<List<Int32>>[] squareFreeNumbers = GenerateSquareFreeNumbers(primeList, n);
            GenerateSquareFreeRoots(squareFreeNumbers, primeList, n, sqrtCache);
            GenerateNonSquareRoots(sqrtCache, n);
            BigInteger sum = 0;
            Int32 numberOfDigitsInDigitCache = 6;
            Dictionary<Int32, Int64> digitCache = GenerateDigitCache(numberOfDigitsInDigitCache);
            foreach (var v in sqrtCache)
            {
                BigInteger sqrt = v.Value;
                sqrt = TrimExtraDigits(sqrt, numberOfDigits);
                sum += DigitSum(sqrt, digitCache, numberOfDigitsInDigitCache);
            }
            Console.WriteLine(sum);
        }
        static void GenerateNonSquareRoots(Dictionary<Int32, BigInteger> sqrtCache, Int32 maxSize)
        {
            List<(Int32, BigInteger)> toAdd = new List<(Int32, BigInteger)>();
            foreach (var v in sqrtCache)
            {
                Int32 currentNumber = v.Key;
                BigInteger currentRoot = v.Value;
                Int32 maxHeight = (Int32)Math.Sqrt(maxSize);
                for (Int32 i = 2; i <= maxHeight; i++)
                {
                    Int32 nextNumber = i * i * currentNumber;
                    if (nextNumber <= maxSize)
                    {
                        BigInteger nextRoot = i * currentRoot;
                        toAdd.Add((nextNumber, nextRoot));
                    }
                }
            }
            foreach (var v in toAdd)
            {
                sqrtCache.Add(v.Item1, v.Item2);
            }
        }
        static void GenerateSquareFreeRoots(List<List<Int32>>[] squareFreeNumbers, List<Int32> primeList, Int32 maxSize, Dictionary<Int32, BigInteger> sqrtCache)
        {
            Int32 desiredLength = sqrtCache[2].ToString().Length;
            Int32 maxDistinctPrimeFactors = squareFreeNumbers.Length - 1;
            for (Int32 numberOfPrimeFactors = 2; numberOfPrimeFactors <= maxDistinctPrimeFactors; numberOfPrimeFactors++)
            {
                foreach (var factorList in squareFreeNumbers[numberOfPrimeFactors])
                {
                    Int32 nextPrime = factorList.Last();
                    Int32 currentNumber = ListProduct(factorList);
                    Int32 previousNumber = currentNumber / nextPrime;
                    BigInteger previousSqrt = sqrtCache[previousNumber];
                    BigInteger nextSqrt = TrimExtraDigits(previousSqrt * sqrtCache[nextPrime], desiredLength);
                    sqrtCache.Add(currentNumber, nextSqrt);
                }
            }
        }
        static BigInteger TrimExtraDigits(BigInteger n, Int32 desiredNumberOfDigits)
        {
            Int32 currentNumberOfDigits = (Int32)BigInteger.Log10(n) + 1;
            if (currentNumberOfDigits <= desiredNumberOfDigits)
            {
                return n;
            }
            Int32 numberOfDigitsToRemove = currentNumberOfDigits - desiredNumberOfDigits;
            return n / BigInteger.Pow(10, numberOfDigitsToRemove);
        }
        static List<List<Int32>>[] GenerateSquareFreeNumbers(List<Int32> primeList, Int32 maxSize)
        {
            Int32 productOfAllPrimes = 1;
            Int32 numberOfPrimes = primeList.Count;
            Int32 maxNumberOfPrimeFactors = numberOfPrimes;
            for (Int32 i = 0; i < numberOfPrimes; i++)
            {
                productOfAllPrimes *= primeList[i];
                if (productOfAllPrimes > maxSize)
                {
                    maxNumberOfPrimeFactors = i;
                    break;
                }
            }
            List<List<Int32>>[] output = new List<List<Int32>>[maxNumberOfPrimeFactors + 1];
            output[1] = new List<List<Int32>>();
            foreach (var p in primeList)
            {
                List<Int32> listToAdd = new List<Int32>();
                listToAdd.Add(p);
                output[1].Add(listToAdd);
            }
            for (Int32 numberOfDistinctPrimes = 2; numberOfDistinctPrimes <= maxNumberOfPrimeFactors; numberOfDistinctPrimes++)
            {
                output[numberOfDistinctPrimes] = new List<List<Int32>>();
                foreach (var previousList in output[numberOfDistinctPrimes - 1])
                {
                    foreach (var primeToAdd in primeList)
                    {
                        if (previousList.Contains(primeToAdd))
                        {
                            continue;
                        }
                        if (primeToAdd < previousList.Last())
                        {
                            continue;
                        }
                        List<Int32> newList = new List<Int32>(previousList);
                        newList.Add(primeToAdd);
                        if (ListProduct(newList) <= maxSize)
                        {
                            output[numberOfDistinctPrimes].Add(newList);
                        }
                    }
                }
            }
            return output;
        }
        static Int32 ListProduct(List<Int32> list)
        {
            Int32 output = 1;
            foreach (var v in list)
            {
                output *= v;
            }
            return output;
        }
        static Dictionary<Int32, BigInteger> GeneratePrimeRoots(List<Int32> primeList, Int32 numberOfDigits)
        {
            Dictionary<Int32, BigInteger> output = new Dictionary<Int32, BigInteger>();
            foreach (var p in primeList)
            {
                BigInteger n = p * BigInteger.Pow(10, 2 * numberOfDigits + 10);
                BigInteger initialGuess = (BigInteger)Math.Sqrt(p * 1000000);
                initialGuess *= BigInteger.Pow(10, numberOfDigits + 2);
                output.Add(p, Sqrt(n, initialGuess));
            }
            return output;
        }
        static List<Int32> GeneratePrimes(Int32 maxPrimeSize)
        {
            bool[] isPrime = new bool[maxPrimeSize + 1];
            for (Int32 i = 2; i <= maxPrimeSize; i++)
            {
                isPrime[i] = true;
            }
            List<Int32> output = new List<Int32>();
            for (Int32 p = 2; p <= maxPrimeSize; p++)
            {
                if (isPrime[p])
                {
                    output.Add(p);
                }
                for (Int32 c = 2 * p; c <= maxPrimeSize; c += p)
                {
                    isPrime[c] = false;
                }
            }
            return output;
        }
        static BigInteger Sqrt(BigInteger n, BigInteger initialGuess)
        {
            if (n == 0)
            {
                return 0;
            }
            Int32 numberOfSteps = 0;
            BigInteger previousGuess = initialGuess;
            while (true)
            {
                BigInteger nextGuess = previousGuess * previousGuess;
                nextGuess += n;
                nextGuess /= previousGuess;
                nextGuess /= 2;
                if (previousGuess == nextGuess)
                {
                    return nextGuess;
                }
                previousGuess = nextGuess;
                numberOfSteps++;
            }
        }
        static Dictionary<Int32, Int64> GenerateDigitCache(Int32 maxNumberOfDigits)
        {
            Dictionary<Int32, Int64> output = new Dictionary<Int32, Int64>();
            output.Add(0, 0);
            for (Int32 currentPlace = 0; currentPlace < maxNumberOfDigits; currentPlace++)
            {
                List<(Int32, Int64)> toAddToCache = new List<(Int32, Int64)>();
                for (Int32 currentDigit = 1; currentDigit < 10; currentDigit++)
                {
                    foreach (var alreadyFound in output)
                    {
                        Int32 currentNumber = (Int32)Math.Pow(10, currentPlace) * currentDigit + alreadyFound.Key;
                        toAddToCache.Add((currentNumber, currentDigit + alreadyFound.Value));
                    }
                }
                foreach (var v in toAddToCache)
                {
                    output.Add(v.Item1, v.Item2);
                }
            }
            return output;
        }
        static Int64 DigitSum(BigInteger n, Dictionary<Int32, Int64> digitCache, Int32 maxNumberOfDigitsInCache)
        {
            Int64 sum = 0;
            Int32 modulusSize = (Int32)Math.Pow(10, maxNumberOfDigitsInCache);
            while (n != 0)
            {
                BigInteger remainder;
                n = BigInteger.DivRem(n, modulusSize, out remainder);
                Int64 toAdd = digitCache[(Int32)remainder];
                sum += toAdd;
            }
            return sum;
        }
    }
}