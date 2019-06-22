using System;

namespace BinarySolver
{
    public static class Evaluator
    {
        public static int? Evaluate(string input)
        {
            var parts = BinaryParser.Parse(input);

            if (parts == null) return null;

            EvaluateByHighestPriority(ref parts);

            if (parts.Length != 1)
                // Something terribly wrong
                throw new Exception("Could not evaluate " + nameof(parts));

            // If there is only one part left, the evaluation is complete
            return parts[0].Value;
        }

        private static void EvaluateByHighestPriority(ref InputPart[] parts)
        {
            // Highest evaluation priority
            var currentEnumPriority = (int) InputTypes.FLIP;

            // 1 is the enum value of a VALUE, anything higher is the operators
            while (currentEnumPriority > 1)
            {
                for (var i = 0; i < parts.Length; i++)
                    if (parts[i].IsOperator && ((int) parts[i].Type == currentEnumPriority ||
                                                // RSHIFT and LSHIFT have equal weighting, look for both
                                                (InputTypes) currentEnumPriority == InputTypes.RSHIFT &&
                                                parts[i].Type == (InputTypes) (currentEnumPriority - 1)))
                    {
                        var oldLength = parts.Length;

                        EvaluateOperation(i, ref parts);

                        var newLength = parts.Length;
                        // The index changes with reduced parts length, therefore change current index back
                        i -= oldLength - newLength;
                    }

                if (currentEnumPriority == (int)InputTypes.RSHIFT)
                {
                    // LSHIFT is already evaluated on RSHIFT, so skip it
                    currentEnumPriority = (int)InputTypes.LSHIFT - 1;
                    continue;
                }

                currentEnumPriority--;
            }
        }

        private static void EvaluateOperation(int operatorIndex, ref InputPart[] parts)
        {
            var inputType = parts[operatorIndex].Type;

            if (InputTypeHelper.IsOperator(inputType.ToString(), out var operatorType))
            {
                // Gets the values of the simplified operation
                var first = 0;

                if (operatorType != InputTypes.FLIP)
                    // Type is flip so no need to check left of operator
                    first = parts[operatorIndex - 1].Value;

                var second = parts[operatorIndex + 1].Value;

                var twoInputValues = true;
                var result = 0;

                switch (inputType)
                {
                    case InputTypes.AND:
                        result = first & second;
                        parts[operatorIndex - 1] = new InputPart(result);
                        break;
                    case InputTypes.OR:
                        result = first | second;
                        parts[operatorIndex - 1] = new InputPart(result);
                        break;
                    case InputTypes.XOR:
                        result = first ^ second;
                        parts[operatorIndex - 1] = new InputPart(result);
                        break;
                    case InputTypes.RSHIFT:
                        result = first >> second;
                        parts[operatorIndex - 1] = new InputPart(result);
                        break;
                    case InputTypes.LSHIFT:
                        result = first << second;
                        parts[operatorIndex - 1] = new InputPart(result);
                        break;
                    case InputTypes.FLIP:
                        twoInputValues = false;
                        result = ~second;
                        parts[operatorIndex + 1] = new InputPart(result);
                        break;
                    default:
                        throw new NotImplementedException();
                }


                // Remove the redundant values
                parts = parts.RemoveAt(operatorIndex);

                if (twoInputValues)
                {
                    Output.ShowWorking(inputType, result, first, second);

                    // Index changes, so operatorIndex + 1, becomes operatorIndex again
                    parts = parts.RemoveAt(operatorIndex);
                }
                else
                {
                    Output.ShowWorking(inputType, result, second);
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(inputType));
            }
        }
    }
}