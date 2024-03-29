﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessCommand(textBox1.Text);
            textBox1.Text = string.Empty;
            textBox1.Focus();
        }




        void ProcessCommand(string command)
        {

            ConsoleWrite(command);

            string result = command;

            if (result.Trim().ToLower() == "help")
            {
                ConsoleWrite("add <formula> - Add a formula to the system.");
                ConsoleWrite("clear fomulas - Clear all formulas.");
                ConsoleWrite("show formulas - Show all formulas.");
                ConsoleWrite("show coefficients - Show the coefficients table.");
                ConsoleWrite("analize <formula> - Analize a formula.");
                ConsoleWrite("solve using jacobis - Solve the system using Jacobis method.");
                ConsoleWrite("solve using parallel jacobis - Solve using the Jacobis method with parallelism.");
                ConsoleWrite("solve using gauss-seidel - Solve the system using Gauss-Seidel method.");
                return;
            }

            if (result.Trim().ToLower().StartsWith("solve using jacobis"))
            {
                ConsoleWrite(g.SolveUsingJacobis());
                return;
            }

            if (result.Trim().ToLower().StartsWith("solve using parallel jacobis"))
            {
                ConsoleWrite(g.SolveUsingParallelJacobis());
                return;
            }


            if (result.Trim().ToLower().StartsWith("solve using gauss-seidel"))
            {
                ConsoleWrite(g.SolveUsingGaussSeidel());
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
                ConsoleWrite("Ok.");
                return;
            }

            if (result.Trim().ToLower().StartsWith("clear fomulas"))
            {
                g.Formulas.Clear();
                ConsoleWrite("Ok.");
                return;
            }


            if (result.Trim().ToLower().StartsWith("show coefficients"))
            {
                ConsoleWrite(g.CreateCoeffientsTable());
                return;
            }


            if (result.Trim().ToLower().StartsWith("show formulas"))
            {
                ConsoleWrite(JsonConvert.SerializeObject(g.Formulas, Formatting.Indented));
                return;
            }

            if (result.Trim().ToLower().StartsWith("analize"))
            {
                string local_result = g.AnalizeFormula(result.Substring(7));

                if (local_result.StartsWith("Error:"))
                {
                    ConsoleWrite(local_result);
                    return;
                }

                ConsoleWrite(local_result);
                return;
            }


            if( result.Trim().ToLower().StartsWith( "post to") )
            {
                string url = result.Substring(7).Trim();
                List<string> formulas = new List<string>();

                foreach (Formula item in g.Formulas)
                {
                    formulas.Add( item.formula );     
                }

                ConsoleWrite( SendJsonToUrl(url, JsonConvert.SerializeObject(formulas, Formatting.Indented)) );

                return;
            }


            ConsoleWrite("Comando desconocido");

        }


        private void ConsoleWrite(string result) 
        {
            this.txtResultados.AppendText(result + "\r\n");
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

        public static string SendJsonToUrl(string url, string json)
        {
            try
            {

                HttpClient web = new HttpClient();
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = web.PostAsync(url, content).Result;

                if (!result.IsSuccessStatusCode)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Error: Reason: " + result.ReasonPhrase);
                    return sb.ToString();
                }

                var byteArray = result.Content.ReadAsByteArrayAsync().Result;
                return Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);

            }
            catch (Exception e)
            {
                return $"Error: ReadUrl: {e.ToString()}";
            }

        }


    }

}


