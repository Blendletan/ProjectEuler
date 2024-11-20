namespace Problem59
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] inputs = GetInputs();
            for (Int32 a = 97; a < 173; a++)
            {
                for (Int32 b = 97; b < 173; b++)
                {
                    for (Int32 c = 97; c < 173; c++)
                    {
                        byte[] key = new byte[3];
                        key[0] = (byte)a;
                        key[1] = (byte)b;
                        key[2] = (byte)c;
                        byte[]? output = Encrypt(inputs, key);
                        if (output != null)
                        {
                            Console.WriteLine(((char)a).ToString() + ((char)b).ToString() + ((char)c).ToString() + " ");
                        }
                    }
                }
            }
        }
        static byte[] GetInputs()
        {
            Int32 length = Int32.Parse(Console.ReadLine());
            string[] inputArray = Console.ReadLine().Split();
            byte[] inputMessage = new byte[length];
            for (Int32 i = 0; i < length; i++)
            {
                inputMessage[i] = (byte)(Int32.Parse(inputArray[i]));
            }
            return inputMessage;
        }
        static byte[]? Encrypt(byte[] input, byte[] key)
        {
            Int32 length = input.Length;
            byte[] output = new byte[length];
            Int32 keyLength = key.Length;
            for (Int32 i = 0; i < length; i++)
            {
                Int32 j = i % key.Length;
                byte m = input[i];
                byte k = key[j];
                output[i] = (byte)(m ^ k);
                char c = (char)output[i];
                bool isValid = false;
                if (c >= 'a' && c <= 'z') { isValid = true; }
                if (c >= 'A' && c <= 'Z') { isValid = true; }
                if (c >= '0' && c <= '9') { isValid = true; }
                if (c == '(' || c == ')' || c == ';' || c == ':') { isValid = true; }
                if (c == ',' || c == '.' || c == '?' || c == '!') { isValid = true; }
                if (c == '-' || c == Convert.ToChar("'") || c == ' ') { isValid = true; }
                if (!isValid)
                {
                    return null;
                }
            }
            return output;
        }
    }
}