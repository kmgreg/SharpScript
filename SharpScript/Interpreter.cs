using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpScript
{
    internal class Interpreter
    {
        public static float EvalExpression(List<string> expression, Dictionary<string, float> varList)
        {

            return 0f;
        }

        public static Queue<List<string>> EvaluateParens(List<string> expression)
        {
            var calcOrder = new Queue<List<string>>();
            var valueMap = new Dictionary<string, float>();
            var termMap = new Dictionary<string, List<String>>();
            var parenL = new Stack<int>();
            var parenOrder = new Queue<int>();
            var pairs = new Dictionary<int, int>();

            // Typical paren matching algo, match r with most recent l
            for (int i = 0; i < expression.Count; i++)
            {
                if (expression[i].Equals("("))
                {
                    parenL.Push(i);
                }
                else if (expression[i].Equals(")"))
                {
                    if (parenL.Count == 0) throw new ArgumentException("Parenthesis Mismatch");
                    int corresponding = parenL.Pop();
                    pairs.Add(corresponding, i);
                    parenOrder.Enqueue(corresponding);
                }
            }

            if (parenL.Count > 0 || parenOrder.Count != pairs.Count)
            {
                throw new ArgumentException("Parenthesis mismatch");
            }

            var expressionCopy = new List<string>(expression);

            int parenCount = 0;

            foreach (var leftParen in parenOrder)
            {
                List<string> calc = new List<string>();
                var rightParen = pairs.GetValueOrDefault(leftParen, -1);
                if (rightParen == -1) throw new ArgumentException("Error in Paren calculation");
                for (int i = leftParen + 1; i < rightParen; i++)
                {
                    if (expressionCopy[i].Equals("-X-")) continue;
                    var token = expressionCopy[i];
                    calc.Add(token);
                    expressionCopy[i] = "-X-";
                }
                calcOrder.Enqueue(calc);

                string parenNum = "ParenNum" + parenCount;
                termMap.Add(parenNum, calc);

                expressionCopy[leftParen] = parenNum;
                expressionCopy[rightParen] = "-X-";
                parenCount++;
            }


            return calcOrder;

        }

        private class ExpressionMap
        {
            private Dictionary<string, float> ValueMap { get; set; }
            private Dictionary<string, List<String>> TermMap { get; set; }
            private Queue<string> ExpressionOrder { get; set; }

            public ExpressionMap(Dictionary<string, float> valueMap, Queue<string> expressionOrder, Dictionary<string, List<string>> termMap)
            {
                ValueMap = valueMap;
                ExpressionOrder = expressionOrder;
                TermMap = termMap;
            }
        }
    }
}
