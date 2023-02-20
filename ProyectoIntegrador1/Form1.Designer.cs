namespace ProyectoIntegrador1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textFormula = new System.Windows.Forms.TextBox();
            this.textX1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textX2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textIteraciones = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textTolerancia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtResultados = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textDerivatedFormula = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fomula";
            // 
            // textFormula
            // 
            this.textFormula.BackColor = System.Drawing.Color.Black;
            this.textFormula.ForeColor = System.Drawing.Color.Lime;
            this.textFormula.Location = new System.Drawing.Point(234, 4);
            this.textFormula.Name = "textFormula";
            this.textFormula.Size = new System.Drawing.Size(658, 24);
            this.textFormula.TabIndex = 0;
            this.textFormula.Text = "(-4 * Math.Pow(x, 3)) + (6 * Math.Pow(x, 2)) + (2 * x)";
            // 
            // textX1
            // 
            this.textX1.BackColor = System.Drawing.Color.Black;
            this.textX1.ForeColor = System.Drawing.Color.Lime;
            this.textX1.Location = new System.Drawing.Point(234, 76);
            this.textX1.Name = "textX1";
            this.textX1.Size = new System.Drawing.Size(248, 24);
            this.textX1.TabIndex = 1;
            this.textX1.Text = "1.50";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(13, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Valor de X1";
            // 
            // textX2
            // 
            this.textX2.BackColor = System.Drawing.Color.Black;
            this.textX2.ForeColor = System.Drawing.Color.Lime;
            this.textX2.Location = new System.Drawing.Point(234, 101);
            this.textX2.Name = "textX2";
            this.textX2.Size = new System.Drawing.Size(248, 24);
            this.textX2.TabIndex = 2;
            this.textX2.Text = " 2.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(13, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Valor de X2";
            // 
            // textIteraciones
            // 
            this.textIteraciones.BackColor = System.Drawing.Color.Black;
            this.textIteraciones.ForeColor = System.Drawing.Color.Lime;
            this.textIteraciones.Location = new System.Drawing.Point(234, 131);
            this.textIteraciones.Name = "textIteraciones";
            this.textIteraciones.Size = new System.Drawing.Size(248, 24);
            this.textIteraciones.TabIndex = 3;
            this.textIteraciones.Text = "100";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(14, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "Iteraciones";
            // 
            // textTolerancia
            // 
            this.textTolerancia.BackColor = System.Drawing.Color.Black;
            this.textTolerancia.ForeColor = System.Drawing.Color.Lime;
            this.textTolerancia.Location = new System.Drawing.Point(234, 161);
            this.textTolerancia.Name = "textTolerancia";
            this.textTolerancia.Size = new System.Drawing.Size(248, 24);
            this.textTolerancia.TabIndex = 4;
            this.textTolerancia.Text = "0.0001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(13, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tolerancia";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(488, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 52);
            this.button1.TabIndex = 5;
            this.button1.Text = "Método de bisección";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(693, 76);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 52);
            this.button2.TabIndex = 6;
            this.button2.Text = "Newton-Raphson ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtResultados
            // 
            this.txtResultados.BackColor = System.Drawing.Color.Black;
            this.txtResultados.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtResultados.ForeColor = System.Drawing.Color.Lime;
            this.txtResultados.Location = new System.Drawing.Point(12, 195);
            this.txtResultados.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtResultados.Multiline = true;
            this.txtResultados.Name = "txtResultados";
            this.txtResultados.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultados.Size = new System.Drawing.Size(880, 323);
            this.txtResultados.TabIndex = 7;
            this.txtResultados.Text = " ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(12, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(212, 22);
            this.label6.TabIndex = 9;
            this.label6.Text = "Derivación de la formula";
            // 
            // textDerivatedFormula
            // 
            this.textDerivatedFormula.BackColor = System.Drawing.Color.Black;
            this.textDerivatedFormula.ForeColor = System.Drawing.Color.Lime;
            this.textDerivatedFormula.Location = new System.Drawing.Point(234, 34);
            this.textDerivatedFormula.Name = "textDerivatedFormula";
            this.textDerivatedFormula.Size = new System.Drawing.Size(658, 24);
            this.textDerivatedFormula.TabIndex = 10;
            this.textDerivatedFormula.Text = "(-12 * Math.Pow(x, 2)) + (12 * x) + 2";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.Location = new System.Drawing.Point(488, 132);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(199, 55);
            this.button3.TabIndex = 11;
            this.button3.Text = "Método de la secante";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button4.Location = new System.Drawing.Point(693, 130);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(199, 55);
            this.button4.TabIndex = 12;
            this.button4.Text = "Método de la Falsa posición";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 529);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textDerivatedFormula);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtResultados);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textTolerancia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textIteraciones);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textX2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textX1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textFormula);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Métodos númericos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox textFormula;
        private TextBox textX1;
        private Label label2;
        private TextBox textX2;
        private Label label3;
        private TextBox textIteraciones;
        private Label label4;
        private TextBox textTolerancia;
        private Label label5;
        private Button button1;
        private Button button2;
        private TextBox txtResultados;
        private Label label6;
        private TextBox textDerivatedFormula;
        private Button button3;
        private Button button4;
    }
}