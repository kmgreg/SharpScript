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
                List<int> commaList = new List<int>();
                var rightParen = pairs.GetValueOrDefault(leftParen, -1);
                if (rightParen == -1) throw new ArgumentException("Error in Paren calculation");
                // TODO Add comma separating
                for (int i = leftParen + 1; i < rightParen; i++)
                {
                    if (expressionCopy[i].Equals("-X-")) continue;
                    var token = expressionCopy[i];
                    calc.Add(token);
                    expressionCopy[i] = "-X-";
                }

                var subdividedWithCommas = new List<List<string>>();

                var currentTerm = new List<string>();

                // Subdivide by comma
                // There's definitely a way to do this in line with initial term creation...

                int count = leftParen + 1;

                foreach (var token in calc)
                {
                    if (!token.Equals(","))
                    {
                        currentTerm.Add(token);
                    }
                    else
                    {
                        calcOrder.Enqueue(currentTerm);
                        string parenNum = "ParenNum" + parenCount;
                        expressionCopy[count] = parenNum;
                        currentTerm = new List<string>();
                        parenCount++;
                    }
                    count++;
                }

                calcOrder.Enqueue(currentTerm);
                string parenNumFinal = "ParenNum" + parenCount;
                expressionCopy[count] = parenNumFinal;

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
