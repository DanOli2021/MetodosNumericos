using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

public static class NumericMethodsClass
{
    // Bisection method
    // 
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


    
    
    public static double TestFunction(double x)
    {
        // return -4x^3 + 6x^2 + 2x
        return (-4 * Math.Pow(x, 3)) + (6 * Math.Pow(x, 2)) + (2 * x);

        // return x^3 - 2x^2 + 1
        //return Math.Pow(x, 3) - (2 * Math.Pow(x, 2)) + 1;
    }

    public static double TestFunctionDerivative(double x)
    {
        // return -12x^2 + 12x + 2
        return (-12 * Math.Pow(x, 2)) + (12 * x) + 2;
    }

    public class NumericMethodsResult 
    {
        public string message { get; set; } = "";
        public double result { get; set; } = 0;
        public int iterations { get; set; } = 0;
        public double delay { get; set; } = 0;
    }
    

}
