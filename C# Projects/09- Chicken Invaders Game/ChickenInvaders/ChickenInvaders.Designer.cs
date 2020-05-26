namespace ChickenInvaders
{
    partial class ChickenInvaders
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
            this.components = new System.ComponentModel.Container();
            this.tm = new System.Windows.Forms.Timer(this.components);
            this.lblScore = new System.Windows.Forms.Label();
            this.eggTimer = new System.Windows.Forms.Timer(this.components);
            this.chickenTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tm
            // 
            this.tm.Enabled = true;
            this.tm.Interval = 15;
            this.tm.Tick += new System.EventHandler(this.tm_tick);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(12, 9);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(96, 33);
            this.lblScore.TabIndex = 0;
            this.lblScore.Text = "Score: 0";
            // 
            // eggTimer
            // 
            this.eggTimer.Enabled = true;
            this.eggTimer.Interval = 15;
            this.eggTimer.Tick += new System.EventHandler(this.eggTimr_Tick);
            // 
            // chickenTimer
            // 
            this.chickenTimer.Enabled = true;
            this.chickenTimer.Interval = 15;
            this.chickenTimer.Tick += new System.EventHandler(this.chickenTimer_Tick);
            // 
            // ChickenInvaders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.lblScore);
            this.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "ChickenInvaders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChickenInvaders_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ChickenInvaders_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tm;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Timer eggTimer;
        private System.Windows.Forms.Timer chickenTimer;
    }
}

