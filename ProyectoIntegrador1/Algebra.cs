using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace ProyectoIntegrador1
{
    public class Algebra
    {
        public List<Formula> Formulas { get; set; } = new List<Formula>();
        public DataTable CoefficientData = new DataTable();
        Dictionary<string, Script> functions = new Dictionary<string, Script>();
        NumericMethodsResult r = new NumericMethodsResult();

        public string AnalizeFormula(string formula)
        {
            string[] string_formula_parts = formula.Split('=');

            if (string_formula_parts.Length == 1)
            {
                return "Error: No equals sign found.";
            }

            Formula f = new Formula();

            List<string> monomials_list = new List<string>();
            string monimials_string = "";

            string_formula_parts[0] += "+";

            for (int i = 0; i < string_formula_parts[0].Length; i++)
            {
                if (i > 0)
                {
                    if (string_formula_parts[0][i] == '+' || string_formula_parts[0][i] == '-')
                    {
                        monimials_string = Regex.Replace(monimials_string, @"\s+", string.Empty);

                        if (!monimials_string.StartsWith("+") && !monimials_string.StartsWith("-"))
                        {
                            monimials_string = "+" + monimials_string;
                        }

                        monomials_list.Add(monimials_string);
                        monimials_string = "";
                    }
                }

                monimials_string += string_formula_parts[0][i];

            }

            string[] string_monomials = string_formula_parts[0].Split('+', '-');
            string result = "";

            string_formula_parts[1] = Regex.Replace(string_formula_parts[1], @"\s+", string.Empty);

            if (!(string_formula_parts[1].StartsWith("+") || string_formula_parts[1].StartsWith("-")))
            {
                string_formula_parts[1] = "+" + string_formula_parts[1];
            }

            Monomial monomial_result = CreateMonomial(string_formula_parts[1], out result);

            f.monomial_result = monomial_result;

            if (result.StartsWith("Error:"))
            {
                return result;
            }

            foreach (string item in monomials_list)
            {
                Monomial m = CreateMonomial(item, out result);

                if (result.StartsWith("Error:"))
                {
                    return result;
                }

                f.Monomials.Add(m);

            }

            f.formula = formula;

            return JsonConvert.SerializeObject(f, Formatting.Indented);
        }


        public Monomial CreateMonomial(string string_monomial, out string result)
        {
            result = "";
            string variable = "";
            string sign = "";
            string coefficient = "";
            string exponente = "";

            sign = string_monomial.Substring(0, 1);

            int index = 1;

            for (int i = 1; i < string_monomial.Length; ++i)
            {

                index = i;

                if (IsNumber(string_monomial[i]))
                {
                    coefficient += string_monomial[i];
                }
                else
                {
                    break;
                }
            }


            for (int i = index; i < string_monomial.Length; ++i)
            {
                index = i + 1;

                if (IsAlpha(string_monomial[i]))
                {
                    variable += string_monomial[i];
                }
                else
                {
                    break;
                }
            }

            for (int i = index; i < string_monomial.Length; ++i)
            {
                if (string_monomial[i] == '^')
                {
                    index = i + 1;
                    break;
                }
            }

            for (int i = index; i < string_monomial.Length; ++i)
            {
                if (IsNumber(string_monomial[i]))
                {
                    exponente += string_monomial[i];
                }
            }

            Monomial m = new Monomial();
            m.Variable = variable;


            if (string.IsNullOrEmpty(coefficient))
            {
                m.Coefficient = 1;
            }
            else
            {
                m.Coefficient = double.Parse(coefficient);
            }

            m.sign = sign;

            if (!string.IsNullOrEmpty(exponente))
            {
                m.Exponent = double.Parse(exponente);
            }
            else
            {
                m.Exponent = 1;
            }

            return m;

        }

        public void AddFormula(Formula formula)
        {
            this.Formulas.Add(formula);
        }


        public string CreateCoeffientsTable()
        {
            this.CoefficientData = new DataTable();

            foreach (Formula f in this.Formulas)
            {
                foreach (Monomial m in f.Monomials)
                {
                    if (!this.CoefficientData.Columns.Contains(m.Variable))
                    {
                        this.CoefficientData.Columns.Add(m.Variable);
                    }
                }
            }

            foreach (Formula f in this.Formulas)
            {
                DataRow row = this.CoefficientData.NewRow();

                foreach (Monomial m in f.Monomials)
                {
                    row[m.Variable] = double.Parse( m.sign + m.Coefficient.ToString() );
                }

                this.CoefficientData.Rows.Add(row);
            }

            return JsonConvert.SerializeObject(this.CoefficientData, Formatting.Indented);
        }

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


        public string SolveUsingJacobis()
        {

            CreateCoeffientsTable();

            double[,] A = new double[this.CoefficientData.Rows.Count, this.CoefficientData.Columns.Count];
            double[] b = new double[this.CoefficientData.Rows.Count];
            double[] x0 = new double[this.CoefficientData.Columns.Count];
            double tolerance = 0.01;
            int maxIterations = 1000;

            for (int i = 0; i < this.CoefficientData.Rows.Count; i++)
            {
                for (int j = 0; j < this.CoefficientData.Columns.Count; j++)
                {
                    double coefficient = 0;
                    double.TryParse(this.CoefficientData.Rows[i][j].ToString(), out coefficient);
                    A[i, j] = coefficient;
                }
            }

            for (int i = 0; i < this.Formulas.Count; i++)
            {
                b[i] = double.Parse(this.Formulas[i].monomial_result.sign + this.Formulas[i].monomial_result.Coefficient.ToString());
            }

            double[] x = Jacobi(A, b, maxIterations, tolerance);

            string result = "";

            for (int i = 0; i < x.Length; i++)
            {
                result += this.CoefficientData.Columns[i].ColumnName + " = " + x[i] + Environment.NewLine;
            }

            return result;
        }


        public string SolveUsingGaussSeidel()
        {
            CreateCoeffientsTable();

            double[,] A = new double[this.CoefficientData.Rows.Count, this.CoefficientData.Columns.Count];
            double[] b = new double[this.CoefficientData.Rows.Count];
            double[] x0 = new double[this.CoefficientData.Columns.Count];
            double tolerance = 0.0001;
            int maxIterations = 100;

            for (int i = 0; i < this.CoefficientData.Rows.Count; i++)
            {
                for (int j = 0; j < this.CoefficientData.Columns.Count; j++)
                {
                    double coefficient = 0;
                    double.TryParse(this.CoefficientData.Rows[i][j].ToString(), out coefficient);
                    A[i, j] = coefficient;
                }
            }

            for (int i = 0; i < this.Formulas.Count; i++)
            {
                b[i] = this.Formulas[i].monomial_result.Coefficient;
            }

            double[] x = GaussSeidelSolver.Solve(A, b, tolerance, maxIterations);

            string result = "";

            for (int i = 0; i < x.Length; i++)
            {
                result += this.CoefficientData.Columns[i].ColumnName + " = " + x[i] + Environment.NewLine;
            }

            return result;
        }


        public static double[] Jacobi(double[,] A, double[] b, int n, double tol)
        {
            int m = A.GetLength(0); // número de filas de A
            double[] x = new double[m]; // vector inicial
            double[] x_old = new double[m]; // vector de iteración anterior

            for (int i = 0; i < m; i++)
            {
                x_old[i] = x[i] = 0; // inicializar los vectores en 0
            }

            for (int k = 1; k <= n; k++)
            {
                for (int i = 0; i < m; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < m; j++)
                    {
                        if (j != i)
                        {
                            sum += A[i, j] * x_old[j];
                        }
                    }
                    x[i] = (b[i] - sum) / A[i, i];
                }
                double diff = 0;
                for (int i = 0; i < m; i++)
                {
                    diff += Math.Abs(x[i] - x_old[i]);
                }
                if (diff < tol)
                {
                    Console.WriteLine("Converged after {0} iterations.", k);
                    return x;
                }
                x_old = (double[])x.Clone(); // copiar x a x_old para la siguiente iteración
            }

            Console.WriteLine("Did not converge after {0} iterations.", n);
            return x;
        }


        /// <summary>
        /// Resuelve una ecuación diferencial utilizando el método de Heun.
        /// </summary>
        /// <param name="f">La función que representa la ecuación diferencial a resolver.</param>
        /// <param name="x0">El valor inicial de x.</param>
        /// <param name="y0">El valor inicial de y.</param>
        /// <param name="h">El tamaño de paso.</param>
        /// <param name="n">El número de iteraciones.</param>
        /// <returns>Un arreglo con los valores de y en cada punto x del intervalo de integración.</returns>
        public static double[] Heun(Func<double, double, double> f, double x0, double y0, double h, int n)
        {
            // Inicializar un arreglo para almacenar los valores de y en cada punto x del intervalo de integración
            double[] y = new double[n + 1];

            // Asignar el valor inicial de y al primer elemento del arreglo
            y[0] = y0;

            // Calcular el valor de y en cada punto x del intervalo de integración utilizando el método de Heun
            for (int i = 0; i < n; i++)
            {
                // Calcular k1 y k2
                double k1 = f(x0 + i * h, y[i]);
                double k2 = f(x0 + (i + 1) * h, y[i] + h * k1);

                // Calcular y en el siguiente punto x utilizando la fórmula del método de Heun
                y[i + 1] = y[i] + h * (k1 + k2) / 2;
            }

            // Devolver el arreglo con los valores de y en cada punto x del intervalo de integración
            return y;
        }


        /// <summary>
        /// Resuelve una ecuación diferencial utilizando el método de Euler.
        /// </summary>
        /// <param name="f">La función que representa la ecuación diferencial a resolver.</param>
        /// <param name="x0">El valor inicial de x.</param>
        /// <param name="y0">El valor inicial de y.</param>
        /// <param name="h">El tamaño de paso.</param>
        /// <param name="n">El número de iteraciones.</param>
        /// <returns>Un arreglo con los valores de y en cada punto x del intervalo de integración.</returns>
        public static double[] Euler(Func<double, double, double> f, double x0, double y0, double h, int n)
        {
            // Inicializar un arreglo para almacenar los valores de y en cada punto x del intervalo de integración
            double[] y = new double[n + 1];

            // Asignar el valor inicial de y al primer elemento del arreglo
            y[0] = y0;

            // Calcular el valor de y en cada punto x del intervalo de integración utilizando el método de Euler
            for (int i = 0; i < n; i++)
            {
                // Calcular y en el siguiente punto x utilizando la fórmula del método de Euler
                y[i + 1] = y[i] + h * f(x0 + i * h, y[i]);
            }

            // Devolver el arreglo con los valores de y en cada punto x del intervalo de integración
            return y;
        }


        /// <summary>
        /// Resuelve una ecuación diferencial utilizando el método de Runge-Kutta de cuarto orden.
        /// </summary>
        /// <param name="f">La función que representa la ecuación diferencial a resolver.</param>
        /// <param name="x0">El valor inicial de x.</param>
        /// <param name="y0">El valor inicial de y.</param>
        /// <param name="h">El tamaño de paso.</param>
        /// <param name="n">El número de iteraciones.</param>
        /// <returns>Un arreglo con los valores de y en cada punto x del intervalo de integración.</returns>
        public static double[] RungeKutta(Func<double, double, double> f, double x0, double y0, double h, int n)
        {
            // Inicializar un arreglo para almacenar los valores de y en cada punto x del intervalo de integración
            double[] y = new double[n + 1];

            // Asignar el valor inicial de y al primer elemento del arreglo
            y[0] = y0;

            // Calcular el valor de y en cada punto x del intervalo de integración utilizando el método de Runge-Kutta de cuarto orden
            for (int i = 0; i < n; i++)
            {
                // Calcular los valores k1, k2, k3 y k4
                double k1 = h * f(x0 + i * h, y[i]);
                double k2 = h * f(x0 + i * h + h / 2, y[i] + k1 / 2);
                double k3 = h * f(x0 + i * h + h / 2, y[i] + k2 / 2);
                double k4 = h * f(x0 + i * h + h, y[i] + k3);

                // Calcular y en el siguiente punto x utilizando la fórmula del método de Runge-Kutta de cuarto orden
                y[i + 1] = y[i] + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
            }

            // Devolver el arreglo con los valores de y en cada punto x del intervalo de integración
            return y;
        }


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

                if (Math.Abs(TestFunction(code, xm)) < tolerance)
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

        /// <summary>
        /// Aproxima el valor de la integral de una función f(x) en el intervalo [a, b] utilizando el método trapezoidal.
        /// </summary>
        /// <param name="f">La función a integrar.</param>
        /// <param name="a">El límite inferior del intervalo.</param>
        /// <param name="b">El límite superior del intervalo.</param>
        /// <param name="n">El número de subintervalos en los que se divide el intervalo [a, b].</param>
        /// <returns>Una aproximación del valor de la integral de f(x) en el intervalo [a, b].</returns>
        public double MetodoTrapezoidal(string f, double a, double b, int n)
        {
            double h = (b - a) / n; // Ancho de cada subintervalo
            double suma = 0; // Suma de los valores de f(x) en los extremos de cada subintervalo

            for (int i = 1; i < n; i++)
            {
                double xi = a + i * h; // Punto medio de cada subintervalo
                suma += TestFunction(f,xi);
            }

            double aproximacion = h * (TestFunction(f, a) + TestFunction(f, b) + 2 * suma) / 2; // Fórmula del método trapezoidal

            return aproximacion;
        }



        public double Simpson(string f, double a, double b, int n)
        {
            if (n % 2 != 0)
            {
                Console.WriteLine("El número de subintervalos debe ser par.");
                return 0;
            }

            double h = (b - a) / n;
            double sum = TestFunction(f,a) + TestFunction(f,b);
            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                sum += 2 * TestFunction(f,x) * (1 + i % 2);
            }
            return sum * h / 3;
        }


        public double RombergIntegration(string f, double a, double b, int maxIterations, double targetError)
        {
            double[,] R = new double[maxIterations + 1, maxIterations + 1];
            R[1, 1] = (b - a) / 2.0 * (TestFunction(f, a) + TestFunction(f, b));
            double h = b - a;

            for (int m = 2; m <= maxIterations; m++)
            {
                h /= 2.0;
                double sum = 0.0;
                for (int i = 1; i <= Math.Pow(2, m - 2); i++)
                {
                    double x = a + (2 * i - 1) * h;
                    sum += TestFunction(f,x);
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


        public static bool IsAlpha(char c)
        {

            if (c >= 'a' && c <= 'z')
            {
                return true;
            }

            if (c >= 'A' && c <= 'Z')
            {
                return true;
            }

            return false;

        }

        public static bool IsNumber(char c)
        {

            if ((c >= '0' && c <= '9') || c == '.')
            {
                return true;
            }

            return false;

        }

        // Test function
        // Esta funcion es la funcion que se va a evaluar
        // para encontrar sus raices.    
        public double TestFunction(string code, double x)
        {
            string result = Eval("TestFunction", "return (" + code + ");", x);

            r.fomula_error = "";

            if (result.StartsWith("Error:"))
            {
                Console.WriteLine(result);
                r.fomula_error = result;
                return 0;
            }

            return Convert.ToDouble(result);
        }


        // Test function derivative
        // Esta funcion es la derivada de la funcion que se va a evaluar
        // para encontrar sus raices.    
        public double TestFunctionDerivative(string code, double x)
        {
            string result = Eval("TestFunctionDerivative", "return (" + code + ");", x);

            if (result.StartsWith("Error:"))
            {
                r.derivative_fomula_error = result;
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




    public class Formula
    {
        public string formula { get; set; } = "";
        public List<Monomial> Monomials { get; set; } = new List<Monomial>();
        public Monomial monomial_result { get; set; }
    }




    public class Monomial
    {
        public string Variable { get; set; } = "";
        public double Coefficient { get; set; } = 0;
        public double Exponent { get; set; } = 0;
        public string sign { get; set; } = "";

    }



    public static class GaussSeidelSolver
    {
        public static double[] Solve(double[,] A, double[] b, double tolerance, int maxIterations)
        {
            int n = b.Length;
            double[] x = new double[n];
            double[] prevX = new double[n];

            // Initialize x with all zeros
            for (int i = 0; i < n; i++)
            {
                x[i] = 0;
            }

            int iteration = 0;
            while (iteration < maxIterations)
            {
                for (int i = 0; i < n; i++)
                {
                    prevX[i] = x[i];
                }

                for (int i = 0; i < n; i++)
                {
                    double sum = b[i];
                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                        {
                            sum -= A[i, j] * x[j];
                        }
                    }
                    x[i] = sum / A[i, i];
                }

                iteration++;

                if (CheckConvergence(x, prevX, tolerance))
                {
                    break;
                }
            }

            return x;
        }

        private static bool CheckConvergence(double[] x, double[] prevX, double tolerance)
        {
            int n = x.Length;
            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(x[i] - prevX[i]) > tolerance)
                {
                    return false;
                }
            }
            return true;
        }



    }


    // NumericMethodsResult class
    // Esta clase es la que se va a serializar en formato JSON
    // para ser enviada al cliente.
    public class NumericMethodsResult
    {
        public string message { get; set; } = "";
        public string fomula_error { get; set; } = "";
        public string derivative_fomula_error { get; set; } = "";
        public double result { get; set; } = 0;
        public List<double> iteration_results { get; set; } = new List<double>();
        public int iterations { get; set; } = 0;
        public double delay { get; set; } = 0;
    }

    public class Globals
    {
        public double x = 0;
    }

}
