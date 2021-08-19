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
            this.PnlMenu = new System.Windows.Forms.Panel();
            this.PnlTutorial = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.LblHighSel = new System.Windows.Forms.Label();
            this.LblHighscore = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LbHighscore = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LblUsername = new System.Windows.Forms.Label();
            this.TbUsername = new System.Windows.Forms.TextBox();
            this.BtnTutorial = new System.Windows.Forms.Button();
            this.BtnGame = new System.Windows.Forms.Button();
            this.TmrHosMovement = new System.Windows.Forms.Timer(this.components);
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
            this.BtnExit = new System.Windows.Forms.Button();
            this.TmrTurn = new System.Windows.Forms.Timer(this.components);
            this.TmrHumMovement = new System.Windows.Forms.Timer(this.components);
            this.TmrDam = new System.Windows.Forms.Timer(this.components);
            this.TmrRegen = new System.Windows.Forms.Timer(this.components);
            this.TmrAttack = new System.Windows.Forms.Timer(this.components);
            this.PnlGame.SuspendLayout();
            this.PnlMenu.SuspendLayout();
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
            this.PnlGame.Controls.Add(this.PnlMenu);
            this.PnlGame.Location = new System.Drawing.Point(2, 0);
            this.PnlGame.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PnlGame.Name = "PnlGame";
            this.PnlGame.Size = new System.Drawing.Size(1110, 691);
            this.PnlGame.TabIndex = 0;
            this.PnlGame.Click += new System.EventHandler(this.PnlGame_Click);
            this.PnlGame.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlGame_Paint);
            this.PnlGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PnlGame_MouseDown);
            this.PnlGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PnlGame_MouseMove);
            this.PnlGame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PnlGame_MouseUp);
            this.PnlGame.Validated += new System.EventHandler(this.PnlGame_Validated);
            // 
            // PnlMenu
            // 
            this.PnlMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlMenu.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PnlMenu.CausesValidation = false;
            this.PnlMenu.Controls.Add(this.PnlTutorial);
            this.PnlMenu.Controls.Add(this.label4);
            this.PnlMenu.Controls.Add(this.label5);
            this.PnlMenu.Controls.Add(this.btnSwitch);
            this.PnlMenu.Controls.Add(this.LblHighSel);
            this.PnlMenu.Controls.Add(this.LblHighscore);
            this.PnlMenu.Controls.Add(this.label3);
            this.PnlMenu.Controls.Add(this.label2);
            this.PnlMenu.Controls.Add(this.LbHighscore);
            this.PnlMenu.Controls.Add(this.label1);
            this.PnlMenu.Controls.Add(this.LblUsername);
            this.PnlMenu.Controls.Add(this.TbUsername);
            this.PnlMenu.Controls.Add(this.BtnTutorial);
            this.PnlMenu.Controls.Add(this.BtnGame);
            this.PnlMenu.Location = new System.Drawing.Point(0, 1);
            this.PnlMenu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PnlMenu.Name = "PnlMenu";
            this.PnlMenu.Size = new System.Drawing.Size(1110, 691);
            this.PnlMenu.TabIndex = 1;
            this.PnlMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlMenu_Paint);
            // 
            // PnlTutorial
            // 
            this.PnlTutorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlTutorial.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PnlTutorial.CausesValidation = false;
            this.PnlTutorial.Location = new System.Drawing.Point(0, 1);
            this.PnlTutorial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PnlTutorial.Name = "PnlTutorial";
            this.PnlTutorial.Size = new System.Drawing.Size(1110, 691);
            this.PnlTutorial.TabIndex = 99;
            this.PnlTutorial.Visible = false;
            this.PnlTutorial.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PnlTutorial_MouseClick);
            // 
            // label4
            // 
            this.label4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grip;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Matura MT Script Capitals", 60F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(97, 509);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 107);
            this.label4.TabIndex = 13;
            this.label4.Text = "14";
            // 
            // label5
            // 
            this.label5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grip;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Matura MT Script Capitals", 30F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(-5, 617);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(329, 53);
            this.label5.TabIndex = 12;
            this.label5.Text = "Character Max!";
            // 
            // btnSwitch
            // 
            this.btnSwitch.BackgroundImage = global::FarmWars.Properties.Resources.Button;
            this.btnSwitch.Font = new System.Drawing.Font("Matura MT Script Capitals", 20F, System.Drawing.FontStyle.Bold);
            this.btnSwitch.ForeColor = System.Drawing.Color.White;
            this.btnSwitch.Location = new System.Drawing.Point(834, 427);
            this.btnSwitch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(266, 42);
            this.btnSwitch.TabIndex = 10;
            this.btnSwitch.Text = "Show All";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.button1_Click);
            // 
            // LblHighSel
            // 
            this.LblHighSel.AutoSize = true;
            this.LblHighSel.BackColor = System.Drawing.Color.Transparent;
            this.LblHighSel.Font = new System.Drawing.Font("Lucida Handwriting", 25F, System.Drawing.FontStyle.Bold);
            this.LblHighSel.ForeColor = System.Drawing.Color.White;
            this.LblHighSel.Location = new System.Drawing.Point(893, 153);
            this.LblHighSel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblHighSel.Name = "LblHighSel";
            this.LblHighSel.Size = new System.Drawing.Size(150, 44);
            this.LblHighSel.TabIndex = 9;
            this.LblHighSel.Text = "Top 10";
            this.LblHighSel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblHighscore
            // 
            this.LblHighscore.AutoSize = true;
            this.LblHighscore.BackColor = System.Drawing.Color.Transparent;
            this.LblHighscore.Font = new System.Drawing.Font("Lucida Handwriting", 25F, System.Drawing.FontStyle.Bold);
            this.LblHighscore.ForeColor = System.Drawing.Color.White;
            this.LblHighscore.Location = new System.Drawing.Point(856, 108);
            this.LblHighscore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblHighscore.Name = "LblHighscore";
            this.LblHighscore.Size = new System.Drawing.Size(237, 44);
            this.LblHighscore.TabIndex = 8;
            this.LblHighscore.Text = "HighScores";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Lucida Handwriting", 30F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(838, 153);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 52);
            this.label3.TabIndex = 7;
            this.label3.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Matura MT Script Capitals", 30F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(816, 602);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(299, 53);
            this.label2.TabIndex = 6;
            this.label2.Text = "Letters Only!";
            // 
            // LbHighscore
            // 
            this.LbHighscore.BackColor = System.Drawing.Color.NavajoWhite;
            this.LbHighscore.Font = new System.Drawing.Font("Lucida Handwriting", 14F, System.Drawing.FontStyle.Bold);
            this.LbHighscore.FormattingEnabled = true;
            this.LbHighscore.HorizontalExtent = 100;
            this.LbHighscore.HorizontalScrollbar = true;
            this.LbHighscore.ItemHeight = 24;
            this.LbHighscore.Location = new System.Drawing.Point(834, 207);
            this.LbHighscore.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LbHighscore.Name = "LbHighscore";
            this.LbHighscore.Size = new System.Drawing.Size(266, 220);
            this.LbHighscore.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(257, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(589, 114);
            this.label1.TabIndex = 4;
            this.label1.Text = "Farm Wars";
            // 
            // LblUsername
            // 
            this.LblUsername.AutoSize = true;
            this.LblUsername.BackColor = System.Drawing.Color.Transparent;
            this.LblUsername.Font = new System.Drawing.Font("Lucida Handwriting", 50F, System.Drawing.FontStyle.Bold);
            this.LblUsername.ForeColor = System.Drawing.Color.White;
            this.LblUsername.Location = new System.Drawing.Point(264, 487);
            this.LblUsername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblUsername.Name = "LblUsername";
            this.LblUsername.Size = new System.Drawing.Size(653, 87);
            this.LblUsername.TabIndex = 3;
            this.LblUsername.Text = "Enter Username";
            this.LblUsername.Click += new System.EventHandler(this.label1_Click);
            // 
            // TbUsername
            // 
            this.TbUsername.Font = new System.Drawing.Font("Matura MT Script Capitals", 50F, System.Drawing.FontStyle.Bold);
            this.TbUsername.Location = new System.Drawing.Point(324, 577);
            this.TbUsername.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TbUsername.Name = "TbUsername";
            this.TbUsername.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TbUsername.Size = new System.Drawing.Size(488, 96);
            this.TbUsername.TabIndex = 2;
            this.TbUsername.TextChanged += new System.EventHandler(this.TbUsername_TextChanged);
            // 
            // BtnTutorial
            // 
            this.BtnTutorial.BackgroundImage = global::FarmWars.Properties.Resources.Button;
            this.BtnTutorial.Font = new System.Drawing.Font("Matura MT Script Capitals", 50F, System.Drawing.FontStyle.Bold);
            this.BtnTutorial.ForeColor = System.Drawing.Color.White;
            this.BtnTutorial.Location = new System.Drawing.Point(324, 307);
            this.BtnTutorial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnTutorial.Name = "BtnTutorial";
            this.BtnTutorial.Size = new System.Drawing.Size(488, 162);
            this.BtnTutorial.TabIndex = 1;
            this.BtnTutorial.Text = "Tutorial";
            this.BtnTutorial.UseVisualStyleBackColor = true;
            this.BtnTutorial.Click += new System.EventHandler(this.BtnTutorial_Click);
            // 
            // BtnGame
            // 
            this.BtnGame.BackgroundImage = global::FarmWars.Properties.Resources.Button;
            this.BtnGame.Font = new System.Drawing.Font("Matura MT Script Capitals", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGame.ForeColor = System.Drawing.Color.White;
            this.BtnGame.Location = new System.Drawing.Point(324, 140);
            this.BtnGame.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnGame.Name = "BtnGame";
            this.BtnGame.Size = new System.Drawing.Size(488, 162);
            this.BtnGame.TabIndex = 0;
            this.BtnGame.Text = "Launch Game";
            this.BtnGame.UseVisualStyleBackColor = true;
            this.BtnGame.Click += new System.EventHandler(this.BtnGame_Click);
            // 
            // TmrHosMovement
            // 
            this.TmrHosMovement.Enabled = true;
            this.TmrHosMovement.Interval = 5;
            this.TmrHosMovement.Tick += new System.EventHandler(this.TmrGame_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seedToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(100, 26);
            // 
            // seedToolStripMenuItem
            // 
            this.seedToolStripMenuItem.Name = "seedToolStripMenuItem";
            this.seedToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1096, 23);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
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
            this.drawMapToolStripMenuItem.Size = new System.Drawing.Size(73, 19);
            this.drawMapToolStripMenuItem.Text = "Draw Map";
            // 
            // reDrawMapToolStripMenuItem
            // 
            this.reDrawMapToolStripMenuItem.Name = "reDrawMapToolStripMenuItem";
            this.reDrawMapToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.reDrawMapToolStripMenuItem.Text = "Draw Map";
            // 
            // reLoadMapToolStripMenuItem
            // 
            this.reLoadMapToolStripMenuItem.Name = "reLoadMapToolStripMenuItem";
            this.reLoadMapToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(125, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(125, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // pathFindingToolStripMenuItem
            // 
            this.pathFindingToolStripMenuItem.Name = "pathFindingToolStripMenuItem";
            this.pathFindingToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            // 
            // moveGuyToolStripMenuItem
            // 
            this.moveGuyToolStripMenuItem.Name = "moveGuyToolStripMenuItem";
            this.moveGuyToolStripMenuItem.Size = new System.Drawing.Size(73, 19);
            this.moveGuyToolStripMenuItem.Text = "Move Guy";
            this.moveGuyToolStripMenuItem.Click += new System.EventHandler(this.moveGuyToolStripMenuItem_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.BtnExit.BackgroundImage = global::FarmWars.Properties.Resources.Button;
            this.BtnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnExit.FlatAppearance.BorderColor = System.Drawing.Color.SandyBrown;
            this.BtnExit.FlatAppearance.BorderSize = 0;
            this.BtnExit.Font = new System.Drawing.Font("Viner Hand ITC", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(560, 6);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(56, 19);
            this.BtnExit.TabIndex = 2;
            this.BtnExit.Text = "Save";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Visible = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // TmrTurn
            // 
            this.TmrTurn.Enabled = true;
            this.TmrTurn.Tick += new System.EventHandler(this.TmrTurn_Tick);
            // 
            // TmrHumMovement
            // 
            this.TmrHumMovement.Interval = 5;
            this.TmrHumMovement.Tick += new System.EventHandler(this.TmrHumMovement_Tick);
            // 
            // TmrDam
            // 
            this.TmrDam.Enabled = true;
            this.TmrDam.Interval = 1000;
            this.TmrDam.Tick += new System.EventHandler(this.TmrDam_Tick);
            // 
            // TmrRegen
            // 
            this.TmrRegen.Enabled = true;
            this.TmrRegen.Interval = 2000;
            this.TmrRegen.Tick += new System.EventHandler(this.TmrRegen_Tick);
            // 
            // TmrAttack
            // 
            this.TmrAttack.Tick += new System.EventHandler(this.TmrAttack_Tick);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 731);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.PnlGame);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormGame";
            this.Text = "FarmWars";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGame_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormGame_KeyUp);
            this.PnlGame.ResumeLayout(false);
            this.PnlMenu.ResumeLayout(false);
            this.PnlMenu.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        public System.Windows.Forms.Button BtnExit;
        public System.Windows.Forms.Timer TmrHosMovement;
        private System.Windows.Forms.Timer TmrTurn;
        public System.Windows.Forms.Timer TmrHumMovement;
        private System.Windows.Forms.Timer TmrDam;
        public System.Windows.Forms.Panel PnlMenu;
        private System.Windows.Forms.Button BtnGame;
        private System.Windows.Forms.TextBox TbUsername;
        private System.Windows.Forms.Button BtnTutorial;
        private System.Windows.Forms.Label LblUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox LbHighscore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Label LblHighSel;
        private System.Windows.Forms.Label LblHighscore;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer TmrRegen;
        private System.Windows.Forms.Timer TmrAttack;
        public System.Windows.Forms.Panel PnlTutorial;
    }
}

