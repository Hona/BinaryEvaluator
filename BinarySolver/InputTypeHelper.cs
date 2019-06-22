using System;
using System.Collections.Generic;

namespace BinarySolver
{
    public static class InputTypeHelper
    {
        private static InputTypes[] GetBinaryOperators()
        {
            var output = new List<InputTypes>();

            // Loop through each InputType item
            foreach (InputTypes inputItem in Enum.GetValues(typeof(InputTypes)))
            {
                var enumName = inputItem.ToString();

                // Check if the enum value is uppercase, meaning it is an operator
                if (enumName == enumName.ToUpper()) output.Add(inputItem);
            }

            return output.ToArray();
        }

        public static bool IsOperator(string input, out InputTypes output)
        {
            // Loop through each InputType item
            foreach (var inputItem in GetBinaryOperators())
                // Check if the input is equal to the enum name
                if (input.ToUpper() == inputItem.ToString())
                {
                    output = inputItem;
                    return true;
                }

            // The input is not a valid operator
            output = InputTypes.Invalid;
            return false;
        }
    }
}