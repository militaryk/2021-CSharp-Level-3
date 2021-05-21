namespace FarmWars
{
    partial class FormGame
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
            this.PnlGame = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblCordY = new System.Windows.Forms.Label();
            this.LblCord = new System.Windows.Forms.Label();
            this.TmrGame = new System.Windows.Forms.Timer(this.components);
            this.PnlGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlGame
            // 
            this.PnlGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlGame.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PnlGame.CausesValidation = false;
            this.PnlGame.Controls.Add(this.label4);
            this.PnlGame.Controls.Add(this.label3);
            this.PnlGame.Controls.Add(this.label2);
            this.PnlGame.Controls.Add(this.label1);
            this.PnlGame.Controls.Add(this.LblCordY);
            this.PnlGame.Controls.Add(this.LblCord);
            this.PnlGame.Location = new System.Drawing.Point(-1, 0);
            this.PnlGame.Name = "PnlGame";
            this.PnlGame.Size = new System.Drawing.Size(1496, 782);
            this.PnlGame.TabIndex = 0;
            this.PnlGame.Click += new System.EventHandler(this.PnlGame_Click);
            this.PnlGame.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlGame_Paint);
            this.PnlGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PnlGame_MouseMove);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label4.Location = new System.Drawing.Point(409, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label3.Location = new System.Drawing.Point(409, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(206, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(206, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // LblCordY
            // 
            this.LblCordY.AutoSize = true;
            this.LblCordY.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.LblCordY.Location = new System.Drawing.Point(3, 28);
            this.LblCordY.Name = "LblCordY";
            this.LblCordY.Size = new System.Drawing.Size(60, 24);
            this.LblCordY.TabIndex = 1;
            this.LblCordY.Text = "label1";
            // 
            // LblCord
            // 
            this.LblCord.AutoSize = true;
            this.LblCord.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.LblCord.Location = new System.Drawing.Point(4, 4);
            this.LblCord.Name = "LblCord";
            this.LblCord.Size = new System.Drawing.Size(76, 24);
            this.LblCord.TabIndex = 0;
            this.LblCord.Text = "LblCord";
            // 
            // TmrGame
            // 
            this.TmrGame.Enabled = true;
            this.TmrGame.Interval = 5;
            this.TmrGame.Tick += new System.EventHandler(this.TmrGame_Tick);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1494, 781);
            this.Controls.Add(this.PnlGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormGame";
            this.Text = "FarmWars";
            this.Load += new System.EventHandler(this.FormGame_Load);
            this.PnlGame.ResumeLayout(false);
            this.PnlGame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlGame;
        private System.Windows.Forms.Timer TmrGame;
        private System.Windows.Forms.Label LblCord;
        private System.Windows.Forms.Label LblCordY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

