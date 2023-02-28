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
    public partial class Form4 : Form
    {

        Algebra g = new Algebra();

        public Form4()
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
            GetResults();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();

            int r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = DecimalHour("7:30");
            dgv.Rows[r].Cells["y"].Value = 18;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = DecimalHour("7:45");
            dgv.Rows[r].Cells["y"].Value = 24;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = DecimalHour("8:00");
            dgv.Rows[r].Cells["y"].Value = 26;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = DecimalHour("8:15");
            dgv.Rows[r].Cells["y"].Value = 20;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = DecimalHour("8:45");
            dgv.Rows[r].Cells["y"].Value = 18;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = DecimalHour("9:15");
            dgv.Rows[r].Cells["y"].Value = 9;

            for (int i = MinutesFromHour("7:30"); i <= MinutesFromHour("9:15"); i = i + 4)
            {
                r = dgv2.Rows.Add();
                dgv2.Rows[r].Cells["x1"].Value = DecimalMinute(i);
            }

            GetResults();

        }


        public double DecimalHour(string hour) 
        {
            string[] hourParts = hour.Split(':');
            double hourDecimal = Math.Round( double.Parse(hourParts[0]) + double.Parse(hourParts[1]) / 60, 6 );
            return hourDecimal;
        }


        public double DecimalMinute(int minute) 
        { 
            return Math.Round( (double) minute / 60, 6 );
        }

        public int MinutesFromHour(string hour)
        {
            string[] hourParts = hour.Split(':');
            int hourDecimal = int.Parse(hourParts[0]) * 60 + int.Parse(hourParts[1]);
            return hourDecimal;
        }


        void GetResults() 
        {
            List<double> xList = new List<double>();
            List<double> yList = new List<double>();

            formsPlot1.Plot.Clear();

            foreach (DataGridViewRow row in dgv.Rows)
            {

                if (row.Cells["x"].Value == null || row.Cells["y"].Value == null) continue;

                xList.Add(double.Parse(row.Cells["x"].Value.ToString()));
                yList.Add(double.Parse(row.Cells["y"].Value.ToString()));
            }

            double[] x = xList.ToArray();
            double[] y = yList.ToArray();

            string result = g.LagrangePolynomial(xList.ToArray(), yList.ToArray());
            ConsoleWrite("Polinomio:");
            ConsoleWrite(result);

            formsPlot1.Plot.AddScatter(x, y);

            List<double> xWishList = new List<double>();
            List<double> yWishList = new List<double>();

            double total = 0;

            foreach (DataGridViewRow row in dgv2.Rows)
            {

                if (row.Cells["x1"].Value == null) continue;

                double xWish = double.Parse(row.Cells["x1"].Value.ToString());
                xWishList.Add(xWish);

                double yWish = Math.Round( g.TestFunction(result, xWish), 6 );
                yWishList.Add(yWish);

                total += yWish;
                row.Cells["y1"].Value = yWish.ToString();

                ConsoleWrite("x:" + xWish + " y: " + yWish);

            }

            ConsoleWrite(" y total: " + total);

            if(xWishList.Count> 0) 
            {
                formsPlot1.Plot.AddScatter(xWishList.ToArray(), yWishList.ToArray());
            }
            
            formsPlot1.Refresh();

        }

        private void ConsoleWrite(string result)
        {
            this.txtResultados.AppendText(result + "\r\n");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();

            int r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = 0;
            dgv.Rows[r].Cells["y"].Value = 0;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = 2;
            dgv.Rows[r].Cells["y"].Value = 1.9;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = 4;
            dgv.Rows[r].Cells["y"].Value = 2;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = 6;
            dgv.Rows[r].Cells["y"].Value = 2;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = 8;
            dgv.Rows[r].Cells["y"].Value = 2.4;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = 10;
            dgv.Rows[r].Cells["y"].Value = 2.6;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = 12;
            dgv.Rows[r].Cells["y"].Value = 2.25;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = 14;
            dgv.Rows[r].Cells["y"].Value = 1.12;

            r = dgv.Rows.Add();
            dgv.Rows[r].Cells["x"].Value = 16;
            dgv.Rows[r].Cells["y"].Value = 0;

            GetResults();


        }
    }
}
