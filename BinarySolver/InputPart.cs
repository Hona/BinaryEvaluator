using System;

namespace BinarySolver
{
    public class InputPart
    {
        private readonly int _intValue;

        public InputPart(string input)
        {
            // Check if it is a number
            if (int.TryParse(input, out var intOutput))
            {
                Type = InputTypes.Value;
                _intValue = intOutput;
                return;
            }

            // Check if it is a binary operator
            if (InputTypeHelper.IsOperator(input, out var typeOutput)) Type = typeOutput;
        }

        public InputPart(int value)
        {
            Type = InputTypes.Value;
            _intValue = value;
        }

        public int Value => Type == InputTypes.Value ? _intValue : throw new InvalidOperationException(Type.ToString());
        public InputTypes Type { get; }
        public bool IsOperator => InputTypeHelper.IsOperator(Type.ToString(), out _);

        public override string ToString()
        {
            if (IsOperator) return Type.ToString();
            if (Type == InputTypes.Invalid) return "Invalid";
            return Value.ToString();
        }
    }
}