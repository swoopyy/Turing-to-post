namespace WindowsFormsApplication2
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_open = new System.Windows.Forms.Button();
            this.button_saveto = new System.Windows.Forms.Button();
            this.button_convert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_open
            // 
            this.button_open.Location = new System.Drawing.Point(12, 12);
            this.button_open.Name = "button_open";
            this.button_open.Size = new System.Drawing.Size(75, 57);
            this.button_open.TabIndex = 0;
            this.button_open.Text = "Open";
            this.button_open.UseVisualStyleBackColor = true;
            this.button_open.Click += new System.EventHandler(this.button_open_Click);
            // 
            // button_saveto
            // 
            this.button_saveto.Location = new System.Drawing.Point(93, 12);
            this.button_saveto.Name = "button_saveto";
            this.button_saveto.Size = new System.Drawing.Size(75, 57);
            this.button_saveto.TabIndex = 1;
            this.button_saveto.Text = "Save to";
            this.button_saveto.UseVisualStyleBackColor = true;
            this.button_saveto.Click += new System.EventHandler(this.button_saveto_Click);
            // 
            // button_convert
            // 
            this.button_convert.Location = new System.Drawing.Point(174, 12);
            this.button_convert.Name = "button_convert";
            this.button_convert.Size = new System.Drawing.Size(75, 57);
            this.button_convert.TabIndex = 2;
            this.button_convert.Text = "Convert";
            this.button_convert.UseVisualStyleBackColor = true;
            this.button_convert.Click += new System.EventHandler(this.button_convert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "© swoopy\r\nturingtopost@gmail.com";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 111);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_convert);
            this.Controls.Add(this.button_saveto);
            this.Controls.Add(this.button_open);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Turing to Post";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_open;
        private System.Windows.Forms.Button button_saveto;
        private System.Windows.Forms.Button button_convert;
        private System.Windows.Forms.Label label1;

    }
}

