using BinarySolver;
using Xunit;

namespace BinaryEvaluation.Tests
{
    public class EvaluationTests
    {
        [Fact]
        public void Eval_5Rshift2_Returns1()
        {
            var tested = Evaluator.Evaluate("5 RSHIFT 2");

            var actual = 5 >> 2;

            Assert.True(actual == tested);
        }

        [Fact]
        public void Eval_Example1_ReturnsSystemEval()
        {
            var tested = Evaluator.Evaluate("32 xor 17 or 15 and 25");

            var actual = (32 ^ 17) | (15 & 25);

            Assert.True(actual == tested);
        }

        [Fact]
        public void Eval_Example2_ReturnsSystemEval()
        {
            var tested =
                Evaluator.Evaluate(
                    "flip 126 xor 26 or flip 26 and 76 and 255 or 26262 and flip 26 or 6 lshift 2 or 8 rshift 1");

            var actual = (~126 ^ 26) | (~26 & 76 & 255) | (26262 & ~26) | (6 << 2) | (8 >> 1);

            Assert.True(actual == tested);
        }

        [Fact]
        public void Eval_Flip555_ReturnsNegative556()
        {
            var tested = Evaluator.Evaluate("FLIP 555");

            var actual = ~555;

            Assert.True(actual == tested);
        }
    }
}