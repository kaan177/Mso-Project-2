namespace Scratch_Mini_Forms
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
            TurnButton = new Button();
            MoveButton = new Button();
            RepeatButton = new Button();
            RepeatUntilButton = new Button();
            OutputTxtBox = new TextBox();
            RunProgramButton = new Button();
            MetricsButton = new Button();
            menuStrip1 = new MenuStrip();
            chooseProgramToolStripMenuItem = new ToolStripMenuItem();
            basicToolStripMenuItem = new ToolStripMenuItem();
            advancedToolStripMenuItem = new ToolStripMenuItem();
            expertToolStripMenuItem = new ToolStripMenuItem();
            GridPanel = new Panel();
            UserInputTxtBox = new TextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // TurnButton
            // 
            TurnButton.BackColor = Color.FromArgb(224, 224, 224);
            TurnButton.ForeColor = SystemColors.ActiveCaptionText;
            TurnButton.Location = new Point(220, 26);
            TurnButton.Margin = new Padding(2, 2, 2, 2);
            TurnButton.Name = "TurnButton";
            TurnButton.Size = new Size(78, 35);
            TurnButton.TabIndex = 0;
            TurnButton.Text = "Turn";
            TurnButton.UseVisualStyleBackColor = false;
            TurnButton.Click += TurnButton_Click;
            // 
            // MoveButton
            // 
            MoveButton.Location = new Point(303, 26);
            MoveButton.Margin = new Padding(2, 2, 2, 2);
            MoveButton.Name = "MoveButton";
            MoveButton.Size = new Size(78, 35);
            MoveButton.TabIndex = 1;
            MoveButton.Text = "Move";
            MoveButton.UseVisualStyleBackColor = true;
            // 
            // RepeatButton
            // 
            RepeatButton.Location = new Point(138, 26);
            RepeatButton.Margin = new Padding(2, 2, 2, 2);
            RepeatButton.Name = "RepeatButton";
            RepeatButton.RightToLeft = RightToLeft.No;
            RepeatButton.Size = new Size(78, 35);
            RepeatButton.TabIndex = 2;
            RepeatButton.Text = "Repeat";
            RepeatButton.UseVisualStyleBackColor = true;
            // 
            // RepeatUntilButton
            // 
            RepeatUntilButton.Location = new Point(386, 26);
            RepeatUntilButton.Margin = new Padding(2, 2, 2, 2);
            RepeatUntilButton.Name = "RepeatUntilButton";
            RepeatUntilButton.Size = new Size(78, 35);
            RepeatUntilButton.TabIndex = 3;
            RepeatUntilButton.Text = "RepeatUntil";
            RepeatUntilButton.UseVisualStyleBackColor = true;
            // 
            // OutputTxtBox
            // 
            OutputTxtBox.Location = new Point(141, 250);
            OutputTxtBox.Margin = new Padding(2, 2, 2, 2);
            OutputTxtBox.Multiline = true;
            OutputTxtBox.Name = "OutputTxtBox";
            OutputTxtBox.ReadOnly = true;
            OutputTxtBox.Size = new Size(241, 62);
            OutputTxtBox.TabIndex = 4;
            OutputTxtBox.TextChanged += OutputTxtBox_TextChanged;
            // 
            // RunProgramButton
            // 
            RunProgramButton.Location = new Point(141, 198);
            RunProgramButton.Margin = new Padding(2, 2, 2, 2);
            RunProgramButton.Name = "RunProgramButton";
            RunProgramButton.Size = new Size(78, 40);
            RunProgramButton.TabIndex = 5;
            RunProgramButton.Text = "Run";
            RunProgramButton.UseVisualStyleBackColor = true;
            RunProgramButton.Click += RunProgramButton_Click;
            // 
            // MetricsButton
            // 
            MetricsButton.Location = new Point(303, 198);
            MetricsButton.Margin = new Padding(2, 2, 2, 2);
            MetricsButton.Name = "MetricsButton";
            MetricsButton.Size = new Size(78, 40);
            MetricsButton.TabIndex = 6;
            MetricsButton.Text = "Metrics";
            MetricsButton.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { chooseProgramToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 1, 0, 1);
            menuStrip1.Size = new Size(560, 24);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "Choose File";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // chooseProgramToolStripMenuItem
            // 
            chooseProgramToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { basicToolStripMenuItem, advancedToolStripMenuItem, expertToolStripMenuItem });
            chooseProgramToolStripMenuItem.Name = "chooseProgramToolStripMenuItem";
            chooseProgramToolStripMenuItem.Size = new Size(108, 22);
            chooseProgramToolStripMenuItem.Text = "Choose Program";
            chooseProgramToolStripMenuItem.Click += chooseProgramToolStripMenuItem_Click;
            // 
            // basicToolStripMenuItem
            // 
            basicToolStripMenuItem.Name = "basicToolStripMenuItem";
            basicToolStripMenuItem.Size = new Size(127, 22);
            basicToolStripMenuItem.Text = "Basic";
            basicToolStripMenuItem.Click += basicToolStripMenuItem_Click;
            // 
            // advancedToolStripMenuItem
            // 
            advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            advancedToolStripMenuItem.Size = new Size(127, 22);
            advancedToolStripMenuItem.Text = "Advanced";
            advancedToolStripMenuItem.Click += advancedToolStripMenuItem_Click;
            // 
            // expertToolStripMenuItem
            // 
            expertToolStripMenuItem.Name = "expertToolStripMenuItem";
            expertToolStripMenuItem.Size = new Size(127, 22);
            expertToolStripMenuItem.Text = "Expert";
            expertToolStripMenuItem.Click += expertToolStripMenuItem_Click;
            // 
            // GridPanel
            // 
            GridPanel.BackColor = SystemColors.Info;
            GridPanel.Location = new Point(386, 66);
            GridPanel.Margin = new Padding(2, 2, 2, 2);
            GridPanel.Name = "GridPanel";
            GridPanel.Size = new Size(159, 127);
            GridPanel.TabIndex = 9;
            // 
            // UserInputTxtBox
            // 
            UserInputTxtBox.Location = new Point(141, 65);
            UserInputTxtBox.Margin = new Padding(2, 2, 2, 2);
            UserInputTxtBox.Multiline = true;
            UserInputTxtBox.Name = "UserInputTxtBox";
            UserInputTxtBox.Size = new Size(241, 129);
            UserInputTxtBox.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 317);
            Controls.Add(UserInputTxtBox);
            Controls.Add(GridPanel);
            Controls.Add(MetricsButton);
            Controls.Add(RunProgramButton);
            Controls.Add(OutputTxtBox);
            Controls.Add(RepeatUntilButton);
            Controls.Add(RepeatButton);
            Controls.Add(MoveButton);
            Controls.Add(TurnButton);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2, 2, 2, 2);
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button TurnButton;
        private Button MoveButton;
        private Button RepeatButton;
        private Button RepeatUntilButton;
        private TextBox OutputTxtBox;
        private Button RunProgramButton;
        private Button MetricsButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem chooseProgramToolStripMenuItem;
        private ToolStripMenuItem basicToolStripMenuItem;
        private ToolStripMenuItem advancedToolStripMenuItem;
        private ToolStripMenuItem expertToolStripMenuItem;
        private Panel GridPanel;
        private TextBox UserInputTxtBox;
    }
}