using Newtonsoft.Json;
using System.Text.RegularExpressions;

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

    Console.WriteLine( g.AnalizeFormula(result) );

}




public class Algebra 
{
    List<Formula> Formulas { get; set; } = new List<Formula>();

    public string AnalizeFormula(string formula)
    {
        string[] string_formula_parts = formula.Split('=');

        if(string_formula_parts.Length == 1 ) 
        {
            return "Error: No equals sign found.";
        }

        Formula f = new Formula();

        List<string> monomials_list = new List<string>();
        string monimials_string = "";

        string_formula_parts[0] += "+";

        for ( int i = 0; i < string_formula_parts[0].Length; i++ )
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

        string[] string_monomials = string_formula_parts[0].Split('+','-');
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

        return JsonConvert.SerializeObject(f, Formatting.Indented);
    }

    public Monomial CreateMonomial(string string_monomial, out string result) 
    {
        result = "";
        string variable = "";
        string sign = "";
        string coefficient = "";
        string exponente = "";

        sign = string_monomial.Substring(0,1);

        int index = 0;

        for (int i = 1; i < string_monomial.Length; i++)
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


        for (int i = index; i < string_monomial.Length; i++)
        {
            index = i;

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
                index = i;
                break;
            }
        }

        for (int i = index; i < string_monomial.Length; i++)
        {
            if (IsNumber(string_monomial[i]))
            {
                exponente += string_monomial[i];
            }
        }

        Monomial m = new Monomial();
        m.Variable = variable;


        if( string.IsNullOrEmpty(coefficient))
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



}




public class Formula
{
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



