using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Data;
using System.Globalization;
using System.Text;

namespace ProyectoIntegrador1
{
    public partial class Form1 : Form
    {


        NumericMethodsClass nm = new NumericMethodsClass();
        
        public Form1()
        {

            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            double x1 = 0;
            double x2 = 0;
            int iterations = 0;
            double tolerance = 0;

            if (double.TryParse(textX1.Text, out x1) == false)
            {
                txtResultados.Text = "Error: x1 no es un numero del tipo double";
                return;
            }

            if (double.TryParse(textX2.Text, out x2) == false)
            {
                txtResultados.Text = "Error: x2 no es un numero del tipo double";
                return;
            }

            if (int.TryParse(textIteraciones.Text, out iterations) == false)
            {
                txtResultados.Text = "Error: iteraciones no es un numero entero.";
                return;
            }

            if (double.TryParse(textTolerancia.Text, out tolerance) == false)
            {
                txtResultados.Text = "Error: tolerancia no es un numero.";
                return;
            }

            string result = nm.Bisection(textFormula.Text, x1, x2, iterations, tolerance);
            txtResultados.Text = "Método de bisección: \n\n"  + result;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double x1 = 0;
            int iterations = 0;
            double tolerance = 0;

            if (double.TryParse(textX1.Text, out x1) == false)
            {
                txtResultados.Text = "Error: x1 no es un numero del tipo double";
                return;
            }

            if (int.TryParse(textIteraciones.Text, out iterations) == false)
            {
                txtResultados.Text = "Error: iteraciones no es un numero entero.";
                return;
            }

            if (double.TryParse(textTolerancia.Text, out tolerance) == false)
            {
                txtResultados.Text = "Error: tolerancia no es un numero.";
                return;
            }

            string result = nm.NewtonRaphson(textFormula.Text, textDerivatedFormula.Text, x1, iterations, tolerance);
            txtResultados.Text = "Método Newton-Raphson: \n\n" + result;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            double x1 = 0;
            double x2 = 0;
            int iterations = 0;
            double tolerance = 0;

            if (double.TryParse(textX1.Text, out x1) == false)
            {
                txtResultados.Text = "Error: x1 no es un numero del tipo double";
                return;
            }

            if (double.TryParse(textX2.Text, out x2) == false)
            {
                txtResultados.Text = "Error: x2 no es un numero del tipo double";
                return;
            }

            if (int.TryParse(textIteraciones.Text, out iterations) == false)
            {
                txtResultados.Text = "Error: iteraciones no es un numero entero.";
                return;
            }

            if (double.TryParse(textTolerancia.Text, out tolerance) == false)
            {
                txtResultados.Text = "Error: tolerancia no es un numero.";
                return;
            }

            string result = nm.Secant(textFormula.Text, x1, x2, iterations, tolerance);
            txtResultados.Text = "Método de la secante: \n\n" + result;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            double x1 = 0;
            double x2 = 0;
            int iterations = 0;
            double tolerance = 0;

            if (double.TryParse(textX1.Text, out x1) == false)
            {
                txtResultados.Text = "Error: x1 no es un numero del tipo double";
                return;
            }

            if (double.TryParse(textX2.Text, out x2) == false)
            {
                txtResultados.Text = "Error: x2 no es un numero del tipo double";
                return;
            }

            if (int.TryParse(textIteraciones.Text, out iterations) == false)
            {
                txtResultados.Text = "Error: iteraciones no es un numero entero.";
                return;
            }

            if (double.TryParse(textTolerancia.Text, out tolerance) == false)
            {
                txtResultados.Text = "Error: tolerancia no es un numero.";
                return;
            }

            string result = nm.FalsePosition(textFormula.Text, x1, x2, iterations, tolerance);
            txtResultados.Text = "Método de la Falsa posición: \n\n" + result;


        }
    }



}



