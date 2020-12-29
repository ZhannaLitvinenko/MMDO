using System;

namespace MMDOLab4
{
	class Program
	{
		static string function;
		static double a, b, eps, h, x0;
		static void Main(string[] args)
		{
			Input();

			CalculateDichotomyMethod(a, b, eps);
			CalculateLocalisationMinimumInterval(x0, h, eps);
			CalculateGoldenRatioMethod(a, b, eps);
			int n = CalculateNFibonacci(a, b, eps);
			CalculateFibonacciMethod(a, b, n);

			Console.ReadKey();
		}

		static void Input()
		{
			Console.WriteLine("Введiть функцiю:");
			function = Console.ReadLine();
			Console.WriteLine("Введiть iнтервал у форматi [a, b]:");
			string[] interval = Console.ReadLine().Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
			a = Convert.ToDouble(interval[0]);
			b = Convert.ToDouble(interval[1]);
			Console.WriteLine("Введiть точнiсть (Epsilon):");
			eps = Convert.ToDouble(Console.ReadLine());

			Console.WriteLine("\nВведiть x0 для методу пошуку вiдрiзка локалiзацiї точки мiнiмуму:");
			x0 = Convert.ToDouble(Console.ReadLine());
			Console.WriteLine("Введiть h для методу пошуку вiдрiзка локалiзацiї точки мiнiмуму:");
			h = Convert.ToDouble(Console.ReadLine());

			Console.WriteLine();
		}

		static void CalculateDichotomyMethod(double a, double b, double eps)
		{
			double delta = eps / 3;
			int Nk = 0, Nf = 0;

			do
			{
				double x1 = (a + b - delta) / 2;
				double x2 = (a + b + delta) / 2;

				double f1 = MathHelper.EvaluateFunction(function, x1);
				Nf++;
				double f2 = MathHelper.EvaluateFunction(function, x2);
				Nf++;

				if (f1 <= f2)
					b = x2;
				else
					a = x1;

				Nk++;
			}
			while (b - a > eps);

			double xResult = (a + b) / 2;
			double fResult = MathHelper.EvaluateFunction(function, xResult);
			Nf++;

			Console.WriteLine($"\nРозв'язок (метод дихотомiї):\n{new string('-', 50)}");
			Console.WriteLine($"x* = {xResult}\nf* = {fResult}\nNk = {Nk}\nNf = {Nf}");
		}
		static void CalculateLocalisationMinimumInterval(double x0, double h, double eps)
		{
			double f1 = MathHelper.EvaluateFunction(function, x0), f2 = 0;
			double x1 = 0, x2 = 0;
			int Nk = 0, Nf = 1;

			do
			{
				h /= 2;
				x2 = x0 + h;
				f2 = MathHelper.EvaluateFunction(function, x2);
				Nf++;

				if (f1 <= f2)
				{
					h = -1 * h;
					x2 = x0 + h;
					f2 = MathHelper.EvaluateFunction(function, x2);
					Nf++;
				}

				Nk++;
			}
			while (f1 < f2 && Math.Abs(h) > eps);

			Console.WriteLine($"\nРозв'язок (метод пошуку вiдрiзка локалiзацiї точки мiнiмуму):\n{new string('-', 50)}");
			if (Math.Abs(h) > eps)
			{
				do
				{
					x1 = x2;
					f1 = f2;
					x2 = x1 + h;
					f2 = MathHelper.EvaluateFunction(function, x2);
					Nf++;
					Nk++;
				}
				while (f1 > f2);

				double a, b;
				if (h > 0)
				{
					a = x1 - h;
					b = x2;
				}
				else
				{
					a = x2;
					b = x1 - h;
				}

				Console.WriteLine($"[a, b] = [{a}, {b}]");
			}
			else
			{
				double resultX = x0;
				double resultF = MathHelper.EvaluateFunction(function, resultX);
				Nf++;

				Console.WriteLine($"x* = {resultX}\nf* = {resultF}");
			}

			Console.WriteLine($"\nNk = {Nk}\nNf = {Nf}");
		}
		static void CalculateGoldenRatioMethod(double a, double b, double eps)
		{
			double u = a + (3 - Math.Sqrt(5)) / 2 * (b - a);
			double v = a + b - u;
			double fu = MathHelper.EvaluateFunction(function, u);
			double fv = MathHelper.EvaluateFunction(function, v);
			int Nk = 0, Nf = 2;
			do
			{
				if (fu <= fv)
				{
					b = v;
					v = u;
					fv = fu;
					u = a + b - v;
					fu = MathHelper.EvaluateFunction(function, u);
					Nf++;
				}
				else
				{
					a = u;
					u = v;
					fu = fv;
					v = a + b - u;
					fv = MathHelper.EvaluateFunction(function, v);
					Nf++;
				}

				if (u > v)
				{
					u = a + (3 - Math.Sqrt(5)) / 2 * (b - a);
					v = a + b - u;
					fu = MathHelper.EvaluateFunction(function, u);
					fv = MathHelper.EvaluateFunction(function, v);
					Nf += 2;
				}

				Nk++;
			} while (b - a > eps);

			double resultX = (a + b) / 2;
			double resultF = MathHelper.EvaluateFunction(function, resultX);
			Nf++;

			Console.WriteLine($"\nРозв'язок (метод золотого перерiзу):\n{new string('-', 50)}");
			Console.WriteLine($"x* = {resultX}\nf* = {resultF}\nNk = {Nk}\nNf = {Nf}");
		}
		static void CalculateFibonacciMethod(double a, double b, int n)
		{
			double u = a + GetFibonacciNumber(n) / GetFibonacciNumber(n + 2) * (b - a);
			double v = a + b - u;
			double fu = MathHelper.EvaluateFunction(function, u);
			double fv = MathHelper.EvaluateFunction(function, v);
			int Nk = 0, Nf = 2;

			for (int i = 0; i <= n; i++)
			{
				if (fu <= fv)
				{
					b = v;
					v = u;
					fv = fu;
					u = a + b - v;
					fu = MathHelper.EvaluateFunction(function, u);
					Nf++;
				}
				else
				{
					a = u;
					u = v;
					fu = fv;
					v = a + b - u;
					fv = MathHelper.EvaluateFunction(function, v);
					Nf++;
				}

				if (u > v)
				{
					u = a + GetFibonacciNumber(n - i + 1) / GetFibonacciNumber(n - i + 3) * (b - a);
					v = a + b - u;
					fu = MathHelper.EvaluateFunction(function, u);
					Nf++;
					fv = MathHelper.EvaluateFunction(function, v);
					Nf++;
				}

				Nk++;
			}

			double resultX = (a + b) / 2;
			double resultF = MathHelper.EvaluateFunction(function, resultX);
			Nf++;

			Console.WriteLine($"\nРозв'язок (метод Фiбоначчi):\n{new string('-', 50)}");
			Console.WriteLine($"x* = {resultX}\nf* = {resultF}\nNk = {Nk}\nNf = {Nf}");
		}

		static double GetFibonacciNumber(int n)
		{
			return (Math.Pow((1 + Math.Sqrt(5)) / 2, n) - Math.Pow((1 - Math.Sqrt(5)) / 2, n)) / Math.Sqrt(5);
		}

		static int CalculateNFibonacci(double a, double b, double epsilon)
		{
			int n = 0;
			double currentFibonacci = GetFibonacciNumber(n + 2);
			while ((b - a) / currentFibonacci > epsilon)
			{
				n++;
				currentFibonacci = GetFibonacciNumber(n + 2);
			}

			return n + 2;
		}
	}
}
