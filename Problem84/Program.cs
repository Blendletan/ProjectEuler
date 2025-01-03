namespace Problem84
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split();
            Int32 numberOfSidesOnDice = Int32.Parse(inputs[0]);
            Int32 numberOfPlacesToReport = Int32.Parse(inputs[1]);
            const Int32 GO = 0;
            const Int32 JAIL = 10;
            const Int32 GOTOJAIL = 30;
            const Int32 C1 = 11;
            const Int32 E3 = 24;
            const Int32 H2 = 39;
            const Int32 R1 = 05;
            const Int32 R2 = 15;
            const Int32 R3 = 25;
            const Int32 R4 = 35;
            const Int32 U1 = 12;
            const Int32 U2 = 28;
            const Int32 CHEST1 = 2;
            const Int32 CHEST2 = 17;
            const Int32 CHEST3 = 33;
            const Int32 CHANCE1 = 7;
            const Int32 CHANCE2 = 22;
            const Int32 CHANCE3 = 36;
            const double ChestTransition = 1.0 / 16.0;
            double[] dice = new double[numberOfSidesOnDice + 1];
            for (Int32 i = 1; i <= numberOfSidesOnDice; i++)
            {
                dice[i] = 1.0 / (double)numberOfSidesOnDice;
            }
            var twoDice = Markov.Convolution(dice, dice);
            double[] stateVector = new double[40];
            stateVector[0] = 1;
            double[,] transitionMatrix = new double[40, 40];
            for (Int32 startPosition = 0; startPosition < 40; startPosition++)
            {
                for (Int32 diceOutcome = 0; diceOutcome < twoDice.Length; diceOutcome++)
                {
                    double diceProbability = twoDice[diceOutcome];
                    Int32 nextSquare = (diceOutcome + startPosition) % 40;
                    if (nextSquare != GOTOJAIL && nextSquare != CHEST1 && nextSquare != CHEST2 && nextSquare != CHEST3 && nextSquare != CHANCE1 && nextSquare != CHANCE2 && nextSquare != CHANCE3)
                    {
                        transitionMatrix[nextSquare, startPosition] += diceProbability;
                    }
                    if (nextSquare == GOTOJAIL)
                    {
                        transitionMatrix[JAIL, startPosition] += diceProbability;
                    }
                    if (nextSquare == CHEST1 || nextSquare == CHEST2 || nextSquare == CHEST3)
                    {
                        transitionMatrix[JAIL, startPosition] += diceProbability * ChestTransition;
                        transitionMatrix[GO, startPosition] += diceProbability * ChestTransition;
                        transitionMatrix[nextSquare, startPosition] += diceProbability * (1 - 2 * ChestTransition);
                    }
                    if (nextSquare == CHANCE1 || nextSquare == CHANCE2 || nextSquare == CHANCE3)
                    {
                        transitionMatrix[nextSquare, startPosition] += diceProbability * (1 - 10 * ChestTransition);
                        transitionMatrix[JAIL, startPosition] += diceProbability * ChestTransition;
                        transitionMatrix[GO, startPosition] += diceProbability * ChestTransition;
                        transitionMatrix[C1, startPosition] += diceProbability * ChestTransition;
                        transitionMatrix[E3, startPosition] += diceProbability * ChestTransition;
                        transitionMatrix[H2, startPosition] += diceProbability * ChestTransition;
                        transitionMatrix[R1, startPosition] += diceProbability * ChestTransition;
                        if (nextSquare < U2 && nextSquare > U1)
                        {
                            transitionMatrix[U2, startPosition] += diceProbability * ChestTransition;
                        }
                        else
                        {
                            transitionMatrix[U1, startPosition] += diceProbability * ChestTransition;
                        }
                        if (nextSquare > R4)
                        {
                            transitionMatrix[R1, startPosition] += 2 * diceProbability * ChestTransition;
                        }
                        else if (nextSquare > R3)
                        {
                            transitionMatrix[R4, startPosition] += 2 * diceProbability * ChestTransition;
                        }
                        else if (nextSquare > R2)
                        {
                            transitionMatrix[R3, startPosition] += 2 * diceProbability * ChestTransition;
                        }
                        else if (nextSquare > R1)
                        {
                            transitionMatrix[R2, startPosition] += 2 * diceProbability * ChestTransition;
                        }
                        else
                        {
                            transitionMatrix[R1, startPosition] += 2 * diceProbability * ChestTransition;
                        }
                        Int32 backwardsSquare = (nextSquare + 37) % 40;
                        if (backwardsSquare != CHEST3)
                        {
                            transitionMatrix[backwardsSquare, startPosition] += diceProbability * ChestTransition;
                        }
                        else
                        {
                            transitionMatrix[JAIL, startPosition] += diceProbability * ChestTransition * ChestTransition;
                            transitionMatrix[GO, startPosition] += diceProbability * ChestTransition * ChestTransition;
                            transitionMatrix[backwardsSquare, startPosition] += diceProbability * (1 - 2 * ChestTransition) * ChestTransition;
                        }
                    }
                }
            }
            Int32 numberOfTrials = 200000;
            var squaredMatrix = Markov.MatrixMultiplication(transitionMatrix, transitionMatrix);
            for (Int32 i = 0; i < numberOfTrials; i++)
            {
                stateVector = Markov.Multiply(squaredMatrix, stateVector);
            }
            var outcome = new List<(Int32, double)>();
            for (Int32 i = 0; i < 40; i++)
            {
                outcome.Add((i, stateVector[i]));
            }
            outcome.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            outcome.Reverse();
            for (Int32 i = 0; i < numberOfPlacesToReport; i++)
            {
                Console.Write($"{WriteSquareName(outcome[i].Item1)} ");
            }
        }
        static string WriteSquareName(Int32 square)
        {
            switch (square)
            {
                case 0:
                    return "GO";
                case 1:
                    return "A1";
                case 2:
                    return "CC1";
                case 3:
                    return "A2";
                case 4:
                    return "T1";
                case 5:
                    return "R1";
                case 6:
                    return "B1";
                case 7:
                    return "CH1";
                case 8:
                    return "B2";
                case 9:
                    return "B3";
                case 10:
                    return "JAIL";
                case 11:
                    return "C1";
                case 12:
                    return "U1";
                case 13:
                    return "C2";
                case 14:
                    return "C3";
                case 15:
                    return "R2";
                case 16:
                    return "D1";
                case 17:
                    return "CC2";
                case 18:
                    return "D2";
                case 19:
                    return "D3";
                case 20:
                    return "FP";
                case 21:
                    return "E1";
                case 22:
                    return "CH2";
                case 23:
                    return "E2";
                case 24:
                    return "E3";
                case 25:
                    return "R3";
                case 26:
                    return "F1";
                case 27:
                    return "F2";
                case 28:
                    return "U2";
                case 29:
                    return "F3";
                case 30:
                    return "G2J";
                case 31:
                    return "G1";
                case 32:
                    return "G2";
                case 33:
                    return "CC3";
                case 34:
                    return "G3";
                case 35:
                    return "R4";
                case 36:
                    return "CH3";
                case 37:
                    return "H1";
                case 38:
                    return "T2";
                case 39:
                    return "H2";
                default:
                    return "error";
            }
        }
    }
    internal class Markov
    {
        public static double[,] MatrixMultiplication(double[,] a, double[,] b)
        {
            Int32 length = a.GetLength(0);
            double[,] output = new double[length, length];
            for (Int32 i = 0; i < length; i++)
            {
                for (Int32 j = 0; j < length; j++)
                {
                    double sum = 0;
                    for (Int32 k = 0; k < length; k++)
                    {
                        sum += a[i, k] * b[k, j];
                    }
                    output[i, j] = sum;
                }
            }
            return output;
        }
        public static double[] Convolution(double[] a, double[] b)
        {
            var output = new double[a.Length + b.Length];
            for (Int32 i = 0; i < output.Length; i++)
            {
                for (Int32 j = 0; j < output.Length; j++)
                {
                    if (j >= a.Length || i - j < 0 || i - j >= b.Length)
                    {
                        continue;
                    }
                    output[i] += a[j] * b[i - j];
                }
            }
            return output;
        }
        public static double[] Multiply(double[,] matrix, double[] vector)
        {
            Int32 length = vector.Length;
            double[] output = new double[length];
            for (Int32 i = 0; i < length; i++)
            {
                double sum = 0;
                for (Int32 j = 0; j < length; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }
                output[i] = sum;
            }
            return output;
        }
    }
}