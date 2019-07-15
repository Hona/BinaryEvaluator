using System;

namespace BinarySolver
{
    internal static class Output
    {
        private static string GetBinaryString(int value) => Convert.ToString(value, 2).PadLeft(32, '0');

        internal static void ShowWorking(InputTypes operation, int result, params int[] input)
        {
            // If it is a test, there is no valid console
            if (!ValidConsoleBuffer()) return;

            if (!(input.Length == 1 || input.Length == 2))
                // Should be 1 (flip) or two arguments (everything else)
                throw new ArgumentOutOfRangeException(nameof(input));

            // Space each output entry by a line
            Console.WriteLine();

            // Show each input value
            foreach (var value in input) Console.WriteLine(GetBinaryString(value) + " = " + value);

            // Show divider
            var operationString = operation.ToString();
            Console.WriteLine(operationString + " " +
                              new string('-', Console.WindowWidth - (operationString.Length + 1)));

            // Show output
            Console.WriteLine(GetBinaryString(result) + " = " + result);
        }

        private static bool ValidConsoleBuffer()
        {
            try
            {
                // Throws exception if there is no valid buffer
                var _ = Console.WindowWidth;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}