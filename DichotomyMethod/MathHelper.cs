using MathNet.Symbolics;
using System.Collections.Generic;

namespace MMDOLab4
{
	class MathHelper
	{
		public static double EvaluateFunction(string function, double x)
		{
			SymbolicExpression expression = Infix.ParseOrThrow(function);
			Dictionary<string, FloatingPoint> variables = new Dictionary<string, FloatingPoint>();
			variables.Add("x", x);

			return expression.Evaluate(variables).RealValue;
		}
	}
}
