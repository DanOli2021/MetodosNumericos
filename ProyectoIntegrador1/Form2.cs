using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIntegrador1
{
    public partial class Form2 : Form
    {

        Algebra g = new Algebra();

        public Form2()
        {

            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            InitializeComponent();
            ConsoleWriter console1 = new ConsoleWriter(this.txtResultados);
            Console.SetOut(console1);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessCommand(textBox1.Text);
            textBox1.Text = string.Empty;
            textBox1.Focus();
        }




        void ProcessCommand(string command)
        {

            Console.WriteLine(command);

            string result = command;

            if (result.Trim().ToLower() == "help")
            {
                Console.WriteLine("add <formula> - Add a formula to the system.");
                Console.WriteLine("clear fomulas - Clear all formulas.");
                Console.WriteLine("show formulas - Show all formulas.");
                Console.WriteLine("show coefficients - Show the coefficients table.");
                Console.WriteLine("analize <formula> - Analize a formula.");
                Console.WriteLine("solve using jacobis - Solve the system using Jacobis method.");
                Console.WriteLine("solve using gauss-seidel - Solve the system using Gauss-Seidel method.");
                return;
            }

            if (result.Trim().ToLower().StartsWith("solve using jacobis"))
            {
                Console.WriteLine(g.SolveUsingJacobis());
                return;
            }

            if (result.Trim().ToLower().StartsWith("solve using gauss-seidel"))
            {
                Console.WriteLine(g.SolveUsingGaussSeidel());
                return;
            }


            if (result.Trim().ToLower().StartsWith("add"))
            {
                string local_result = g.AnalizeFormula(result.Substring(4));

                if (local_result.StartsWith("Error:"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    return;
                }

                Formula f = JsonConvert.DeserializeObject<Formula>(local_result);
                g.AddFormula(f);
                Console.WriteLine("Ok.");
                return;
            }

            if (result.Trim().ToLower().StartsWith("clear fomulas"))
            {
                g.Formulas.Clear();
                Console.WriteLine("Ok.");
                return;
            }


            if (result.Trim().ToLower().StartsWith("show coefficients"))
            {
                Console.WriteLine(g.CreateCoeffientsTable());
                return;
            }


            if (result.Trim().ToLower().StartsWith("show formulas"))
            {
                Console.WriteLine(JsonConvert.SerializeObject(g.Formulas, Formatting.Indented));
                return;
            }

            if (result.Trim().ToLower().StartsWith("analize"))
            {
                string local_result = g.AnalizeFormula(result.Substring(7));

                if (local_result.StartsWith("Error:"))
                {
                    Console.WriteLine(local_result);
                    return;
                }

                Console.WriteLine(local_result);
                return;
            }


            Console.WriteLine("Comando desconocido");

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if( e.KeyValue == 13)
            {
                ProcessCommand(textBox1.Text);
                textBox1.Text = string.Empty;
                textBox1.Focus();
            }
        }
    }


}


