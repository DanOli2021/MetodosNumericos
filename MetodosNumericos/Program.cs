// Author: Daniel Oliver Rojas Escobar
// Date: 2023-01-01
// Description: Programa principal

string formula = "(-4 * Math.Pow(x, 3)) + (6 * Math.Pow(x, 2)) + (2 * x)";
string DerivatedFormula = "(-12 * Math.Pow(x, 2)) + (12 * x) + 2";

NumericMethodsClass mn = new NumericMethodsClass();

Console.WriteLine(formula);
Console.WriteLine();
string result;
Console.WriteLine("Método de bisección");
result = mn.Bisection(formula, 1.50, 2.00, 100, 0.0001);
Console.WriteLine(result);

Console.WriteLine("Método de falsa posición");
result = mn.FalsePosition(formula, 1.50, 2.00, 100, 0.0001);
Console.WriteLine(result);

Console.WriteLine("Método de Newton-Raphson");
result = mn.NewtonRaphson(formula, DerivatedFormula,  1.50, 100, 0.0001);
Console.WriteLine(result);

Console.WriteLine("Método de la secante");
result = mn.Secant(formula, 1.50, 2.00, 100, 0.0001);
Console.WriteLine(result);

Console.ReadLine();