using System;
using System.Linq;

namespace BinarySolver
{
    public static class BinaryParser
    {
        public static InputPart[] Parse(string input)
        {
            var inputParts = input.Split(' ');
            var output = new InputPart[inputParts.Length];

            for (var i = 0; i < inputParts.Length; i++)
                // Parse each part into a InputPart object
                output[i] = new InputPart(inputParts[i]);

            // TODO: Should we check for Invalid objects here
            if (output.Any(x => x.Type == InputTypes.Invalid))
            {
                Console.WriteLine("Invalid input");
                return null;
            }

            return output;
        }
    }
}