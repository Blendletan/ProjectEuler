using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
namespace Problem79
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfLines = Int32.Parse(Console.ReadLine());
            List<(char, char, char)> inputList = new List<(char, char, char)>();
            for (Int32 i = 0; i < numberOfLines; i++)
            {
                string nextLine = Console.ReadLine();
                inputList.Add(ParseInput(nextLine));
            }
            string failMessage = "SMTH WRONG";
            StringSorter sorter = new StringSorter(inputList);
            if (sorter.failed)
            {
                Console.WriteLine(failMessage);
                return;
            }
            Int32 length = sorter.initialNumberOfChars;
            StringBuilder output = new StringBuilder();
            for (Int32 i = 0; i < length; i++)
            {
                char? nextChar = sorter.GetNextLetter();
                if (nextChar == null)
                {
                    Console.WriteLine(failMessage);
                    break;
                }
                output.Append(nextChar);
            }
            Console.WriteLine(output.ToString());
        }
        static (char, char, char) ParseInput(string input)
        {
            return (input[0], input[1], input[2]);
        }
    }
    class StringSorter
    {
        public readonly bool failed;
        public readonly Int32 initialNumberOfChars;
        List<char> availableChars;
        Dictionary<char, List<char>> greaterThan;
        Dictionary<char, List<char>> lessThan;
        public StringSorter(List<(char, char, char)> inputs)
        {
            availableChars = new List<char>();
            greaterThan = new Dictionary<char, List<char>>();
            lessThan = new Dictionary<char, List<char>>();
            foreach (var nextInput in inputs)
            {
                char a = nextInput.Item1;
                char b = nextInput.Item2;
                char c = nextInput.Item3;
                if (!availableChars.Contains(a)) { availableChars.Add(a); }
                if (!availableChars.Contains(b)) { availableChars.Add(b); }
                if (!availableChars.Contains(c)) { availableChars.Add(c); }
                if (!AddRelationship(b, a))
                {
                    failed = true;
                    break;
                }
                if (!AddRelationship(c, b))
                {
                    failed = true;
                    break;
                }
            }
            availableChars.Sort();
            initialNumberOfChars = availableChars.Count;
        }
        bool AddRelationship(char larger, char smaller)
        {
            if (greaterThan.ContainsKey(smaller))
            {
                if (greaterThan[smaller].Contains(larger))
                {
                    return false;
                }
            }
            if (!greaterThan.ContainsKey(larger))
            {
                greaterThan.Add(larger, new List<char>());
            }
            if (!greaterThan[larger].Contains(smaller))
            {
                greaterThan[larger].Add(smaller);
            }
            else
            {
                return true;
            }
            if (!lessThan.ContainsKey(smaller))
            {
                lessThan.Add(smaller, new List<char>());
            }
            if (!lessThan[smaller].Contains(larger))
            {
                lessThan[smaller].Add(larger);
            }
            if (greaterThan.ContainsKey(smaller))
            {
                foreach (var evenSmaller in greaterThan[smaller])
                {
                    AddRelationship(larger, evenSmaller);
                }
            }
            if (lessThan.ContainsKey(larger))
            {
                foreach(var evenLarger in lessThan[larger])
                {
                    AddRelationship(evenLarger, smaller);
                }
            }
            return true;
        }
        public char? GetNextLetter()
        {
            char? possibleOutput = null;
            foreach (var c in availableChars)
            {
                if (greaterThan.ContainsKey(c))
                {
                    continue;
                }
                possibleOutput = c;
                break;
            }
            if (possibleOutput == null)
            {
                return null;
            }
            char output = possibleOutput.Value;
            availableChars.Remove(output);
            List<char> toRemove = new List<char>();
            foreach (var c in greaterThan)
            {
                if (c.Value.Contains(output))
                {
                    c.Value.Remove(output);
                }
                if (c.Value.Count == 0)
                {
                    toRemove.Add(c.Key);
                }
            }
            foreach (var c in toRemove)
            {
                greaterThan.Remove(c);
            }
            return output;
        }
    }
}