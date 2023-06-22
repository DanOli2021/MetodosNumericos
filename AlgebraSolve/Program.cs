using System.Text;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Algebra Recolver!");

app.MapPost("/jacobis", async delegate (HttpContext context)
{
    using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8))
    {
        try
        {
            string jsonstring = await reader.ReadToEndAsync();

            List<string> Formulas = JsonConvert.DeserializeObject<List<string>>(jsonstring);
            Algebra g = new Algebra();

            foreach (string item in Formulas)
            {
                string local_result = g.AnalizeFormula(item);

                if (local_result.StartsWith("Error:"))
                {
                    return local_result;
                }

                Formula f = JsonConvert.DeserializeObject<Formula>(local_result);
                g.AddFormula(f);

            }

            return g.SolveUsingParallelJacobis();

        }
        catch (Exception e)
        {
            return $"Error: {e}";
        }

    }

});



app.Run();
