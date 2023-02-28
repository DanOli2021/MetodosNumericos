namespace ProyectoIntegrador1
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textFormula = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textInferior = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textSuperior = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textIteraciones = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtResultados = new System.Windows.Forms.TextBox();
            this.textTolerancia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textFormula
            // 
            this.textFormula.BackColor = System.Drawing.Color.Black;
            this.textFormula.ForeColor = System.Drawing.Color.Lime;
            this.textFormula.Location = new System.Drawing.Point(254, 12);
            this.textFormula.Name = "textFormula";
            this.textFormula.Size = new System.Drawing.Size(658, 29);
            this.textFormula.TabIndex = 1;
            this.textFormula.Text = "Pow(x,2)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(33, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fomula";
            // 
            // textInferior
            // 
            this.textInferior.BackColor = System.Drawing.Color.Black;
            this.textInferior.ForeColor = System.Drawing.Color.Lime;
            this.textInferior.Location = new System.Drawing.Point(254, 88);
            this.textInferior.Name = "textInferior";
            this.textInferior.Size = new System.Drawing.Size(248, 29);
            this.textInferior.TabIndex = 6;
            this.textInferior.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(33, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "Limite inferior";
            // 
            // textSuperior
            // 
            this.textSuperior.BackColor = System.Drawing.Color.Black;
            this.textSuperior.ForeColor = System.Drawing.Color.Lime;
            this.textSuperior.Location = new System.Drawing.Point(254, 58);
            this.textSuperior.Name = "textSuperior";
            this.textSuperior.Size = new System.Drawing.Size(248, 29);
            this.textSuperior.TabIndex = 5;
            this.textSuperior.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(33, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = "Limite superior";
            // 
            // textIteraciones
            // 
            this.textIteraciones.BackColor = System.Drawing.Color.Black;
            this.textIteraciones.ForeColor = System.Drawing.Color.Lime;
            this.textIteraciones.Location = new System.Drawing.Point(254, 118);
            this.textIteraciones.Name = "textIteraciones";
            this.textIteraciones.Size = new System.Drawing.Size(248, 29);
            this.textIteraciones.TabIndex = 9;
            this.textIteraciones.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(33, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(217, 22);
            this.label4.TabIndex = 10;
            this.label4.Text = "Número de subintervalos";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.Location = new System.Drawing.Point(508, 105);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(199, 55);
            this.button3.TabIndex = 14;
            this.button3.Text = "Integración de Romberg";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(713, 47);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 52);
            this.button2.TabIndex = 13;
            this.button2.Text = "Reglas de Simpson";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(508, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 52);
            this.button1.TabIndex = 12;
            this.button1.Text = "Regla de trapecio";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtResultados
            // 
            this.txtResultados.BackColor = System.Drawing.Color.Black;
            this.txtResultados.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtResultados.ForeColor = System.Drawing.Color.Lime;
            this.txtResultados.Location = new System.Drawing.Point(13, 189);
            this.txtResultados.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtResultados.Multiline = true;
            this.txtResultados.Name = "txtResultados";
            this.txtResultados.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultados.Size = new System.Drawing.Size(899, 325);
            this.txtResultados.TabIndex = 15;
            this.txtResultados.Text = " ";
            // 
            // textTolerancia
            // 
            this.textTolerancia.BackColor = System.Drawing.Color.Black;
            this.textTolerancia.ForeColor = System.Drawing.Color.Lime;
            this.textTolerancia.Location = new System.Drawing.Point(254, 149);
            this.textTolerancia.Name = "textTolerancia";
            this.textTolerancia.Size = new System.Drawing.Size(248, 29);
            this.textTolerancia.TabIndex = 16;
            this.textTolerancia.Text = "0.0001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(33, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 22);
            this.label5.TabIndex = 17;
            this.label5.Text = "Tolerancia";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 519);
            this.Controls.Add(this.textTolerancia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtResultados);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textIteraciones);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textInferior);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textSuperior);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textFormula);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textFormula;
        private Label label1;
        private TextBox textInferior;
        private Label label3;
        private TextBox textSuperior;
        private Label label2;
        private TextBox textIteraciones;
        private Label label4;
        private Button button3;
        private Button button2;
        private Button button1;
        private TextBox txtResultados;
        private TextBox textTolerancia;
        private Label label5;
    }
}