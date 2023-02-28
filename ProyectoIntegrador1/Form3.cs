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
    public partial class Form3 : Form
    {

        Algebra g = new Algebra();

        public Form3()
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
            double approach = g.MetodoTrapezoidal(textFormula.Text, double.Parse(textInferior.Text), double.Parse(textSuperior.Text), int.Parse(textIteraciones.Text) );
            ConsoleWrite("Formula: " + textFormula.Text);
            ConsoleWrite("Trapezoidal Rule: " + approach.ToString() );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double approach = g.Simpson(textFormula.Text, double.Parse(textInferior.Text), double.Parse(textSuperior.Text), int.Parse(textIteraciones.Text));
            ConsoleWrite("Formula: " + textFormula.Text);
            ConsoleWrite("Simpson`s Rules: " + approach.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double approach = g.RombergIntegration(textFormula.Text, double.Parse(textInferior.Text), double.Parse(textSuperior.Text), int.Parse(textIteraciones.Text), double.Parse(textTolerancia.Text));
            ConsoleWrite("Formula: " + textFormula.Text);
            ConsoleWrite("Romberg Integration: " + approach.ToString());
        }

        private void ConsoleWrite(string result)
        {
            this.txtResultados.AppendText(result + "\r\n");
        }

    }
}
