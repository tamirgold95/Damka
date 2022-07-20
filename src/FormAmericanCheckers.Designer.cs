namespace AmericanCheckers
{
    partial class FormAmericanCheckers
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
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.labelFirstPlayerScore = new System.Windows.Forms.Label();
            this.labelSecondPlayerScore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer1.Location = new System.Drawing.Point(107, 23);
            this.labelPlayer1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(73, 17);
            this.labelPlayer1.TabIndex = 0;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer2.Location = new System.Drawing.Point(369, 23);
            this.labelPlayer2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(73, 17);
            this.labelPlayer2.TabIndex = 1;
            this.labelPlayer2.Text = "Player 2:";
            // 
            // buttonQuit
            // 
            this.buttonQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonQuit.Location = new System.Drawing.Point(-1, -1);
            this.buttonQuit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(100, 28);
            this.buttonQuit.TabIndex = 2;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // labelFirstPlayerScore
            // 
            this.labelFirstPlayerScore.AutoSize = true;
            this.labelFirstPlayerScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelFirstPlayerScore.Location = new System.Drawing.Point(239, 23);
            this.labelFirstPlayerScore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFirstPlayerScore.Name = "labelFirstPlayerScore";
            this.labelFirstPlayerScore.Size = new System.Drawing.Size(17, 17);
            this.labelFirstPlayerScore.TabIndex = 3;
            this.labelFirstPlayerScore.Text = "0";
            // 
            // labelSecondPlayerScore
            // 
            this.labelSecondPlayerScore.AutoSize = true;
            this.labelSecondPlayerScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelSecondPlayerScore.Location = new System.Drawing.Point(515, 23);
            this.labelSecondPlayerScore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSecondPlayerScore.Name = "labelSecondPlayerScore";
            this.labelSecondPlayerScore.Size = new System.Drawing.Size(17, 17);
            this.labelSecondPlayerScore.TabIndex = 4;
            this.labelSecondPlayerScore.Text = "0";
            // 
            // FormAmericanCheckers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(651, 490);
            this.Controls.Add(this.labelSecondPlayerScore);
            this.Controls.Add(this.labelFirstPlayerScore);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAmericanCheckers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AmericanCheckers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.Label labelFirstPlayerScore;
        private System.Windows.Forms.Label labelSecondPlayerScore;
    }
}