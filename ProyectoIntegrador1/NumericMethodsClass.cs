using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;

// Clase que contiene los métodos numéricos
public class NumericMethodsClass
{


    Dictionary<string, Script> functions = new Dictionary<string, Script>();

    // Bisection method
    // Este metodo es un algoritmo de busqueda de raices
    // de una funcion continua en un intervalo dado.
    // El metodo de biseccion divide el intervalo a la mitad
    // y evalua la funcion en el punto medio. Si el valor de la funcion
    // en el punto medio es cero, entonces el punto medio es la raiz.
    // Si el valor de la funcion en el punto medio es distinto de cero,
    // entonces el intervalo se divide en dos partes y se repite el proceso
    // hasta que se encuentre la raiz o se alcance el numero maximo de iteraciones.
    // El metodo de biseccion es un metodo lento, pero es muy estable.    
    public string Bisection(string code, double x1, double x2, int iterations, double tolerance)
    {

        NumericMethodsResult r = new NumericMethodsResult();
        DateTime init_time = DateTime.Now;

        if ((TestFunction(code, x1) * TestFunction(code, x2)) > 0)
        {
            r.message = "Error: f(x1) * f(x2) > 0";
            return JsonConvert.SerializeObject(r, Formatting.Indented);
        }

        double xm = 0;

        for (int i = 0; i < iterations; i++)
        {

            r.iterations = i;

            xm = (x1 + x2) / 2;

            if (TestFunction(code, xm) == 0 || (x2 - x1) / 2 < tolerance)
            {
                break;
            }

            if( TestFunction(code, xm) * TestFunction(code, x1) < 0) 
            {
                x2 = xm;
            }
            else
            {
                x1 = xm;
            }

        }

        DateTime end_time = DateTime.Now;
        TimeSpan time_span = end_time - init_time;
        r.delay = time_span.TotalMilliseconds;

        r.message = "Ok.";
        r.result = xm;

        return JsonConvert.SerializeObject(r, Formatting.Indented);
    }



    // False position method
    // Este metodo es un algoritmo de busqueda de raices
    // de una funcion continua en un intervalo dado.
    // El metodo de falsa posicion es similar al metodo de biseccion,
    // pero en vez de dividir el intervalo a la mitad, se calcula el punto medio
    // de la recta que une los puntos (x1, f(x1)) y (x2, f(x2)).
    // El metodo de falsa posicion es mas rapido que el metodo de biseccion,
    // pero es menos estable.
    public string FalsePosition(string code, double x1, double x2, int iterations, double tolerance) 
    {

        NumericMethodsResult r = new NumericMethodsResult();
        DateTime init_time = DateTime.Now;

        if (TestFunction(code, x1) * TestFunction(code, x2) >= 0)
        {
            r.message = "Error: f(x1) * f(x2) >= 0";
            return JsonConvert.SerializeObject(r, Formatting.Indented);
        }

        double xm = 0;

        for (int i = 0; i < iterations; i++)
        {
            
            r.iterations = i;

            xm = (x1 * TestFunction(code, x2) - x2 * TestFunction(code, x1)) / (TestFunction(code, x2) - TestFunction(code, x1));

            if (TestFunction(code, xm) <= tolerance)
            {
                break;
            }

            if (TestFunction(code, xm) * TestFunction(code, x1) < 0)
            {
                x2 = xm;
            }
            else
            {
                x1 = xm;
            }

        }

        DateTime end_time = DateTime.Now;
        TimeSpan time_span = end_time - init_time;
        r.delay = time_span.TotalMilliseconds;

        r.message = "Ok.";
        r.result = xm;
        return JsonConvert.SerializeObject(r, Formatting.Indented);

    }


    // Newton-Raphson in C#
    // Este metodo es un algoritmo de busqueda de raices
    // de una funcion continua en un intervalo dado.
    // El metodo de Newton-Raphson es un metodo iterativo
    // que utiliza la derivada de la funcion para aproximar
    // la raiz de la funcion.
    // El metodo de Newton-Raphson es mas rapido que el metodo de biseccion,
    // pero es menos estable.
    public string NewtonRaphson(string code, string derivated_code, double x, int iterations, double tolerance)
    {

        NumericMethodsResult r = new NumericMethodsResult();
        DateTime init_time = DateTime.Now;

        double x1 = 0;

        for (int i = 0; i < iterations; i++)
        {
            r.iterations = i;

            x1 = x - (TestFunction(code, x) / TestFunctionDerivative(derivated_code, x));

            if (Math.Abs(x1 - x) <= tolerance)
            {
                break;
            }

            x = x1;
        }

        DateTime end_time = DateTime.Now;
        TimeSpan time_span = end_time - init_time;
        r.delay = time_span.TotalMilliseconds;

        r.message = "Ok.";
        r.result = x1;
        return JsonConvert.SerializeObject(r, Formatting.Indented);

    }



    // Secant Method in c#
    // Este metodo es un algoritmo de busqueda de raices
    // de una funcion continua en un intervalo dado.
    // El metodo de la secante es un metodo iterativo
    // que utiliza la derivada de la funcion para aproximar
    // la raiz de la funcion.
    // El metodo de la secante es mas rapido que el metodo de biseccion,
    // pero es menos estable.
    public string Secant(string code, double x0, double x1, int iterations, double tolerance)
    {

        NumericMethodsResult r = new NumericMethodsResult();
        DateTime init_time = DateTime.Now;

        double x2 = 0;

        for (int i = 0; i < iterations; i++)
        {
            r.iterations = i;

            x2 = x1 - (TestFunction(code, x1) * (x1 - x0)) / (TestFunction(code, x1) - TestFunction(code, x0));

            if (Math.Abs(x2 - x1) <= tolerance)
            {
                break;
            }

            x0 = x1;
            x1 = x2;
        }

        DateTime end_time = DateTime.Now;
        TimeSpan time_span = end_time - init_time;
        r.delay = time_span.TotalMilliseconds;

        r.message = "Ok.";
        r.result = x2;
        return JsonConvert.SerializeObject(r, Formatting.Indented);

    }



    // Test function
    // Esta funcion es la funcion que se va a evaluar
    // para encontrar sus raices.    
    public double TestFunction(string code, double x)
    {
        string result = Eval("TestFunction", "return " + code + ";", x);

        if (result.StartsWith("Error:"))
        {
            Console.WriteLine(result);
            return 0;
        }

        return Convert.ToDouble(result);
    }


    // Test function derivative
    // Esta funcion es la derivada de la funcion que se va a evaluar
    // para encontrar sus raices.    
    public double TestFunctionDerivative(string code, double x)
    {
        string result = Eval("TestFunctionDerivative", "return " + code + ";", x);

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



    // NumericMethodsResult class
    // Esta clase es la que se va a serializar en formato JSON
    // para ser enviada al cliente.
    public class NumericMethodsResult 
    {
        public string message { get; set; } = "";
        public double result { get; set; } = 0;
        public int iterations { get; set; } = 0;
        public double delay { get; set; } = 0;
    }    
    

}
public class Globals
{
    public double x = 0;
}
