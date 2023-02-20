using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;



NumberFormatInfo nfi = new NumberFormatInfo();
nfi.NumberDecimalSeparator = ".";
CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

Algebra g = new Algebra();

while (true)
{

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Equation System Solver > ");
    Console.ResetColor();
    string result = Console.ReadLine();

    if (result.Trim().ToLower() == "quit")
    {
        break;
    }

    if (result.Trim().ToLower() == "help")
    {
        Console.WriteLine("add <formula> - Add a formula to the system.");
        Console.WriteLine("clear fomulas - Clear all formulas.");
        Console.WriteLine("show formulas - Show all formulas.");
        Console.WriteLine("show coefficients - Show the coefficients table.");
        Console.WriteLine("analize <formula> - Analize a formula.");
        Console.WriteLine("solve using jacobis - Solve the system using Jacobis method.");
        Console.WriteLine("solve using gauss-seidel - Solve the system using Gauss-Seidel method.");
        Console.WriteLine("quit - Exit the program.");
        continue;
    }


    if (result.Trim().ToLower().StartsWith("test lagrange"))
    {
        double[] x = { 1, 3, 5, 7, 13 };
        double[] y = { 800, 2310, 3090, 3940, 4755 };

        string polynomial = g.LagrangePolynomial(x, y);
        
        Console.WriteLine("Polinomio resultante: ");
        Console.WriteLine(polynomial);
        Console.WriteLine();

        for (int i = 1; i <= 13; i++)
        {
            Console.WriteLine($"t = {i} ->" + g.TestFunction(polynomial, i));
        }

        continue;
    }


    if (result.Trim().ToLower().StartsWith("solve using jacobis"))
    {
        Console.WriteLine(g.SolveUsingJacobis());
        continue;
    }

    if (result.Trim().ToLower().StartsWith("solve using gauss-seidel"))
    {
        Console.WriteLine(g.SolveUsingGaussSeidel());
        continue;
    }


    if (result.Trim().ToLower().StartsWith("add"))
    {
        string local_result = g.AnalizeFormula(result.Substring(4));

        if (local_result.StartsWith("Error:"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(local_result);
            Console.ResetColor();
            continue;
        }

        Formula f = JsonConvert.DeserializeObject<Formula>(local_result);
        g.AddFormula(f);
        Console.WriteLine("Ok.");
        continue;
    }

    if (result.Trim().ToLower().StartsWith("clear fomulas"))
    {
        g.Formulas.Clear();
        Console.WriteLine("Ok.");
        continue;
    }


    if (result.Trim().ToLower().StartsWith("show coefficients"))
    {
        Console.WriteLine(g.CreateCoeffientsTable());
        continue;
    }


    if (result.Trim().ToLower().StartsWith("show formulas"))
    {
        Console.WriteLine(JsonConvert.SerializeObject(g.Formulas, Formatting.Indented));
        continue;
    }

    if (result.Trim().ToLower().StartsWith("analize"))
    {
        string local_result = g.AnalizeFormula(result.Substring(7));

        if (local_result.StartsWith("Error:"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(local_result);
            Console.ResetColor();
            continue;
        }

        Console.WriteLine(local_result);
        continue;
    }


    Console.WriteLine("Comando desconocido");

}




public class Algebra
{
    public List<Formula> Formulas { get; set; } = new List<Formula>();
    public DataTable CoefficientData = new DataTable();
    Dictionary<string, Script> functions = new Dictionary<string, Script>();
    NumericMethodsResult r;


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
                row[m.Variable] = m.Coefficient;
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

        double[] x = Jacobi(A, b, x0, tolerance, maxIterations);

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



    public double[] Jacobi(double[,] A, double[] b, double[] x0, double tolerance, int maxIterations)
    {
        int n = b.Length;
        double[] x = new double[n];
        double[] xPrev = new double[n];

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            for (int i = 0; i < n; i++)
            {
                xPrev[i] = x[i];
                x[i] = b[i];
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        x[i] -= A[i, j] * xPrev[j];
                    }
                }
                x[i] /= A[i, i];
            }

            // Verificar la convergencia
            double error = 0;
            for (int i = 0; i < n; i++)
            {
                error += Math.Abs(x[i] - xPrev[i]);
            }

            if (error < tolerance)
            {
                break;
            }
        }

        return x;
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

        if (result.StartsWith("Error:"))
        {
            r.fomula_error = result;
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
