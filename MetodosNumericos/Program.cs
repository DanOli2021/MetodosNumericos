string result;

Console.WriteLine("Método de bisección");
result = NumericMethodsClass.Bisection(1.50, 2.00, 100, 0.0001);
Console.WriteLine(result);

Console.WriteLine("Método de falsa posición");
result = NumericMethodsClass.FalsePosition(1.50, 2.00, 100, 0.0001);
Console.WriteLine(result);

Console.WriteLine("Método de Newton-Raphson");
result = NumericMethodsClass.NewtonRaphson(1.50, 100, 0.0001);
Console.WriteLine(result);

Console.WriteLine("Método de la secante");
result = NumericMethodsClass.Secant(1.50, 2.00, 100, 0.0001);
Console.WriteLine(result);
