using Newtonsoft.Json;

// Clase que contiene los métodos numéricos
public static class NumericMethodsClass
{
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
    public static string Bisection(double x1, double x2, int iterations, double tolerance)
    {

        NumericMethodsResult r = new NumericMethodsResult();
        DateTime init_time = DateTime.Now;
        
        if (TestFunction(x1) * TestFunction(x2) >= 0)
        {
            r.message = "Error: f(x1) * f(x2) >= 0";
            return JsonConvert.SerializeObject(r, Formatting.Indented);
        }

        double xm = 0;

        for (int i = 0; i < iterations; i++)
        {

            r.iterations = i;

            xm = (x1 + x2) / 2;

            if (TestFunction(xm) == 0 || (x2 - x1) / 2 < tolerance)
            {
                break;
            }

            if( TestFunction(xm) * TestFunction(x1) < 0) 
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
        r.delay = time_span.TotalMicroseconds;

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
    public static string FalsePosition(double x1, double x2, int iterations, double tolerance) 
    {

        NumericMethodsResult r = new NumericMethodsResult();
        DateTime init_time = DateTime.Now;

        if (TestFunction(x1) * TestFunction(x2) >= 0)
        {
            r.message = "Error: f(x1) * f(x2) >= 0";
            return JsonConvert.SerializeObject(r, Formatting.Indented);
        }

        double xm = 0;

        for (int i = 0; i < iterations; i++)
        {
            
            r.iterations = i;

            xm = (x1 * TestFunction(x2) - x2 * TestFunction(x1)) / (TestFunction(x2) - TestFunction(x1));

            if (TestFunction(xm) <= tolerance)
            {
                break;
            }

            if (TestFunction(xm) * TestFunction(x1) < 0)
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
        r.delay = time_span.TotalMicroseconds;

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
    public static string NewtonRaphson(double x0, int iterations, double tolerance)
    {

        NumericMethodsResult r = new NumericMethodsResult();
        DateTime init_time = DateTime.Now;

        double x1 = 0;

        for (int i = 0; i < iterations; i++)
        {
            r.iterations = i;

            x1 = x0 - (TestFunction(x0) / TestFunctionDerivative(x0));

            if (Math.Abs(x1 - x0) <= tolerance)
            {
                break;
            }

            x0 = x1;
        }

        DateTime end_time = DateTime.Now;
        TimeSpan time_span = end_time - init_time;
        r.delay = time_span.TotalMicroseconds;

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
    public static string Secant(double x0, double x1, int iterations, double tolerance)
    {

        NumericMethodsResult r = new NumericMethodsResult();
        DateTime init_time = DateTime.Now;

        double x2 = 0;

        for (int i = 0; i < iterations; i++)
        {
            r.iterations = i;

            x2 = x1 - (TestFunction(x1) * (x1 - x0)) / (TestFunction(x1) - TestFunction(x0));

            if (Math.Abs(x2 - x1) <= tolerance)
            {
                break;
            }

            x0 = x1;
            x1 = x2;
        }

        DateTime end_time = DateTime.Now;
        TimeSpan time_span = end_time - init_time;
        r.delay = time_span.TotalMicroseconds;

        r.message = "Ok.";
        r.result = x2;
        return JsonConvert.SerializeObject(r, Formatting.Indented);

    }



    // Test function
    // Esta funcion es la funcion que se va a evaluar
    // para encontrar sus raices.    
    public static double TestFunction(double x)
    {
        // return -4x^3 + 6x^2 + 2x
        return (-4 * Math.Pow(x, 3)) + (6 * Math.Pow(x, 2)) + (2 * x);

        // return x^3 - 2x^2 + 1
        //return Math.Pow(x, 3) - (2 * Math.Pow(x, 2)) + 1;
    }


    // Test function derivative
    // Esta funcion es la derivada de la funcion que se va a evaluar
    // para encontrar sus raices.    
    public static double TestFunctionDerivative(double x)
    {
        // return -12x^2 + 12x + 2
        return (-12 * Math.Pow(x, 2)) + (12 * x) + 2;
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
