using System;

double a = 0;
double b = Math.PI / 2;
int n = 100;

double result = TrapezoidalRule(MyFunction, a, b, n);
Console.WriteLine($"Trapezoidal Rule");
Console.WriteLine($"The approximate value of the integral is: {result}");


result = Simpson(MyFunction, a, b, n);
Console.WriteLine($"Simpson Rule");
Console.WriteLine($"The approximate value of the integral is: {result}");

int maxIterations = 10;
double targetError = 1e-6;

result = RombergIntegration(MyFunction, a, b, maxIterations, targetError);
Console.WriteLine($"Romberg Integration");
Console.WriteLine($"The approximate value of the integral is: {result}");


Console.ReadKey();

static double TrapezoidalRule(Func<double, double> f, double a, double b, int n)
{
    double h = (b - a) / n;
    double sum = (f(a) + f(b)) / 2.0;

    for (int i = 1; i < n; i++)
    {
        double x = a + i * h;
        sum += f(x);
    }

    return h * sum;
}


static double Simpson(Func<double, double> f, double a, double b, int n)
{
    if (n % 2 != 0)
    {
        throw new ArgumentException("El número de subintervalos debe ser par.");
    }

    double h = (b - a) / n;
    double sum = f(a) + f(b);
    for (int i = 1; i < n; i++)
    {
        double x = a + i * h;
        sum += 2 * f(x) * (1 + i % 2);
    }
    return sum * h / 3;
}


static double RombergIntegration(Func<double, double> f, double a, double b, int maxIterations, double targetError)
{
    double[,] R = new double[maxIterations + 1, maxIterations + 1];
    R[1, 1] = (b - a) / 2.0 * (f(a) + f(b));
    double h = b - a;

    for (int m = 2; m <= maxIterations; m++)
    {
        h /= 2.0;
        double sum = 0.0;
        for (int i = 1; i <= Math.Pow(2, m - 2); i++)
        {
            double x = a + (2 * i - 1) * h;
            sum += f(x);
        }

        R[m, 1] = 0.5 * R[m - 1, 1] + h * sum;

        for (int n = 2; n <= m; n++)
        {
            R[m, n] = (Math.Pow(4, n - 1) * R[m, n - 1] - R[m - 1, n - 1]) / (Math.Pow(4, n - 1) - 1);
        }

        if (Math.Abs(R[m, m] - R[m - 1, m - 1]) < targetError)
        {
            return R[m, m];
        }
    }

    return R[maxIterations, maxIterations];
}



static double MyFunction(double x)
{
    return Math.Exp(-x) * Math.Sin(x);
}
