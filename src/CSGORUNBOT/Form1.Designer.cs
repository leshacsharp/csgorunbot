
namespace CSGORUNBOT
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.defaultPricetextBox = new System.Windows.Forms.TextBox();
            this.betChanceTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.multiplyPriceTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.maxProfitTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.defaultPlusMinusTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.defaultStepTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.betIfChanceTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.betAfterGamesTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(745, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "start bot";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(730, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 34);
            this.button2.TabIndex = 1;
            this.button2.Text = "launch browser";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(745, 207);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 34);
            this.button3.TabIndex = 2;
            this.button3.Text = "stop bot";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "default price";
            // 
            // defaultPricetextBox
            // 
            this.defaultPricetextBox.Location = new System.Drawing.Point(101, 73);
            this.defaultPricetextBox.Name = "defaultPricetextBox";
            this.defaultPricetextBox.Size = new System.Drawing.Size(150, 31);
            this.defaultPricetextBox.TabIndex = 4;
            // 
            // betChanceTextBox
            // 
            this.betChanceTextBox.Location = new System.Drawing.Point(101, 159);
            this.betChanceTextBox.Name = "betChanceTextBox";
            this.betChanceTextBox.Size = new System.Drawing.Size(150, 31);
            this.betChanceTextBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "bet chance";
            // 
            // multiplyPriceTextBox
            // 
            this.multiplyPriceTextBox.Location = new System.Drawing.Point(101, 245);
            this.multiplyPriceTextBox.Name = "multiplyPriceTextBox";
            this.multiplyPriceTextBox.Size = new System.Drawing.Size(150, 31);
            this.multiplyPriceTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "multiply price if fail";
            // 
            // maxProfitTextBox
            // 
            this.maxProfitTextBox.Location = new System.Drawing.Point(101, 338);
            this.maxProfitTextBox.Name = "maxProfitTextBox";
            this.maxProfitTextBox.Size = new System.Drawing.Size(150, 31);
            this.maxProfitTextBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(117, 310);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "max profit($)";
            // 
            // defaultPlusMinusTextBox
            // 
            this.defaultPlusMinusTextBox.Location = new System.Drawing.Point(312, 73);
            this.defaultPlusMinusTextBox.Name = "defaultPlusMinusTextBox";
            this.defaultPlusMinusTextBox.Size = new System.Drawing.Size(150, 31);
            this.defaultPlusMinusTextBox.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(312, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "default plus/minus";
            // 
            // defaultStepTextBox
            // 
            this.defaultStepTextBox.Location = new System.Drawing.Point(312, 159);
            this.defaultStepTextBox.Name = "defaultStepTextBox";
            this.defaultStepTextBox.Size = new System.Drawing.Size(150, 31);
            this.defaultStepTextBox.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(329, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 25);
            this.label6.TabIndex = 13;
            this.label6.Text = "default step";
            // 
            // betIfChanceTextBox
            // 
            this.betIfChanceTextBox.Location = new System.Drawing.Point(312, 245);
            this.betIfChanceTextBox.Name = "betIfChanceTextBox";
            this.betIfChanceTextBox.Size = new System.Drawing.Size(150, 31);
            this.betIfChanceTextBox.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(329, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 25);
            this.label7.TabIndex = 15;
            this.label7.Text = "bet if chance";
            // 
            // betAfterGamesTextBox
            // 
            this.betAfterGamesTextBox.Location = new System.Drawing.Point(312, 338);
            this.betAfterGamesTextBox.Name = "betAfterGamesTextBox";
            this.betAfterGamesTextBox.Size = new System.Drawing.Size(150, 31);
            this.betAfterGamesTextBox.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(281, 310);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(226, 25);
            this.label8.TabIndex = 17;
            this.label8.Text = "bet after number of games";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(298, 372);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(183, 25);
            this.label9.TabIndex = 19;
            this.label9.Text = "(for multiple strategy)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(298, 279);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(183, 25);
            this.label10.TabIndex = 20;
            this.label10.Text = "(for multiple strategy)";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(717, 301);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(183, 34);
            this.button4.TabIndex = 21;
            this.button4.Text = "default settings 1";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(717, 347);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(183, 34);
            this.button5.TabIndex = 22;
            this.button5.Text = "default settings 2";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 450);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.betAfterGamesTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.betIfChanceTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.defaultStepTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.defaultPlusMinusTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.maxProfitTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.multiplyPriceTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.betChanceTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.defaultPricetextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox defaultPricetextBox;
        private System.Windows.Forms.TextBox betChanceTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox multiplyPriceTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox maxProfitTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox defaultPlusMinusTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox defaultStepTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox betIfChanceTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox betAfterGamesTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

