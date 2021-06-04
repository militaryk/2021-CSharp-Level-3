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
            this.TmrGame = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.seedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.drawMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reDrawMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reLoadMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathFindingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveGuyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlGame
            // 
            this.PnlGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlGame.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PnlGame.CausesValidation = false;
            this.PnlGame.Location = new System.Drawing.Point(16, 38);
            this.PnlGame.Margin = new System.Windows.Forms.Padding(4);
            this.PnlGame.Name = "PnlGame";
            this.PnlGame.Size = new System.Drawing.Size(1895, 910);
            this.PnlGame.TabIndex = 0;
            this.PnlGame.Click += new System.EventHandler(this.PnlGame_Click);
            this.PnlGame.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlGame_Paint);
            this.PnlGame.DoubleClick += new System.EventHandler(this.PnlGame_DoubleClick);
            this.PnlGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PnlGame_MouseMove);
            this.PnlGame.Validated += new System.EventHandler(this.PnlGame_Validated);
            // 
            // TmrGame
            // 
            this.TmrGame.Enabled = true;
            this.TmrGame.Interval = 5;
            this.TmrGame.Tick += new System.EventHandler(this.TmrGame_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seedToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(112, 28);
            // 
            // seedToolStripMenuItem
            // 
            this.seedToolStripMenuItem.Name = "seedToolStripMenuItem";
            this.seedToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.seedToolStripMenuItem.Text = "Seed";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawMapToolStripMenuItem,
            this.moveGuyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1924, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // drawMapToolStripMenuItem
            // 
            this.drawMapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reDrawMapToolStripMenuItem,
            this.reLoadMapToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem,
            this.pathFindingToolStripMenuItem});
            this.drawMapToolStripMenuItem.Name = "drawMapToolStripMenuItem";
            this.drawMapToolStripMenuItem.Size = new System.Drawing.Size(92, 24);
            this.drawMapToolStripMenuItem.Text = "Draw Map";
            // 
            // reDrawMapToolStripMenuItem
            // 
            this.reDrawMapToolStripMenuItem.Name = "reDrawMapToolStripMenuItem";
            this.reDrawMapToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.reDrawMapToolStripMenuItem.Text = "Draw Map";
            this.reDrawMapToolStripMenuItem.Click += new System.EventHandler(this.reDrawMapToolStripMenuItem_Click);
            // 
            // reLoadMapToolStripMenuItem
            // 
            this.reLoadMapToolStripMenuItem.Name = "reLoadMapToolStripMenuItem";
            this.reLoadMapToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.reLoadMapToolStripMenuItem.Text = "Read List";
            this.reLoadMapToolStripMenuItem.Click += new System.EventHandler(this.reLoadMapToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // pathFindingToolStripMenuItem
            // 
            this.pathFindingToolStripMenuItem.Name = "pathFindingToolStripMenuItem";
            this.pathFindingToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.pathFindingToolStripMenuItem.Text = "PathFinding";
            this.pathFindingToolStripMenuItem.Click += new System.EventHandler(this.pathFindingToolStripMenuItem_Click);
            // 
            // moveGuyToolStripMenuItem
            // 
            this.moveGuyToolStripMenuItem.Name = "moveGuyToolStripMenuItem";
            this.moveGuyToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.moveGuyToolStripMenuItem.Text = "Move Guy";
            this.moveGuyToolStripMenuItem.Click += new System.EventHandler(this.moveGuyToolStripMenuItem_Click);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 961);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.PnlGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormGame";
            this.Text = "FarmWars";
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer TmrGame;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem seedToolStripMenuItem;
        public System.Windows.Forms.Panel PnlGame;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem drawMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reDrawMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reLoadMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pathFindingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveGuyToolStripMenuItem;
    }
}

