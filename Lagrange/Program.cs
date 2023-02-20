using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;
using SkiaSharp;

NumberFormatInfo nfi = new NumberFormatInfo();
nfi.NumberDecimalSeparator = ".";
CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

Lagrange g = new Lagrange();

while (true)
{

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Lagrange > ");
    Console.ResetColor();
    string result = Console.ReadLine();

    if (result.Trim().ToLower().StartsWith("test"))
    {
        double[] x = { 1, 3, 5, 7, 13 };
        double[] y = { 800, 2310, 3090, 3940, 4755 };

        string polynomial = g.LagrangePolynomial(x, y);

        Console.WriteLine("Resultant polynomial: ");
        Console.WriteLine(polynomial);
        Console.WriteLine();

        for (int i = 1; i <= 13; i++)
        {
            Console.WriteLine($"t = {i} ->" + g.TestFunction(polynomial, i));
        }

        continue;

    }

    if (result.Trim().ToLower() == "quit")
    {
        break;
    }

    Console.WriteLine("Comando desconocido");

}




public class Lagrange
{
    Dictionary<string, Script> functions = new Dictionary<string, Script>();

    //Este método toma dos argumentos:
    //
    //Un arreglo de valores de x
    //Un arreglo de valores de y correspondientes a los valores de x
    //El método devuelve el polinomio interpolador de Lagrange en forma de una cadena de caracteres.
    //
    //Este polinomio puede ser utilizado para encontrar el valor interpolado para cualquier valor de x dentro del rango de valores de x dados.
    public string LagrangePolynomial(double[] x, double[] y)
    {
        StringBuilder polynomial = new StringBuilder();

        int n = x.Length;
        for (int i = 0; i < n; i++)
        {
            StringBuilder term = new StringBuilder();

            double yi = y[i];
            double xi = x[i];
            for (int j = 0; j < n; j++)
            {
                if (i != j)
                {
                    double xj = x[j];
                    term.Append("(x - ");
                    term.Append(xj);
                    term.Append(") / (");
                    term.Append(xi);
                    term.Append(" - ");
                    term.Append(xj);
                    term.Append(") * ");
                }
            }
            term.Append(yi);
            polynomial.Append(term);
            if (i != n - 1)
            {
                polynomial.Append(" + ");
            }
        }
        return polynomial.ToString();
    }


    // Test function
    // Esta funcion es la funcion que se va a evaluar
    // para encontrar sus raices.    
    public double TestFunction(string code, double x)
    {
        string result = Eval("TestFunction", "return (" + code + ");", x);

        if (result.StartsWith("Error:"))
        {
            Console.WriteLine(result);
            return 0;
        }

        return Convert.ToDouble(result);
    }

    public string Eval(string funtion_name, string code, double x)
    {
        try
        {
            Globals g = new Globals();
            g.x = x;
            object o;

            if (this.functions.ContainsKey(funtion_name))
            {
                o = functions[funtion_name].RunAsync(g).GetAwaiter().GetResult().ReturnValue;

                if (o != null)
                {
                    return o.ToString();
                }

                return "";

            }

            ScriptOptions options = ScriptOptions.Default.WithImports(new[] { "System", "System.Net", "System.Collections.Generic", "System.Math" });
            Script script = CSharpScript.Create(code, options, typeof(Globals));
            script.GetCompilation();
            script.Compile();

            this.functions.Add(funtion_name, script);

            o = script.RunAsync(g).GetAwaiter().GetResult().ReturnValue;

            if (o != null)
            {
                return o.ToString();
            }

            return "";

        }
        catch (Exception e)
        {
            return $"Error: {e.ToString()}";
        }

    }

}


public class Globals
{
    public double x = 0;
}
