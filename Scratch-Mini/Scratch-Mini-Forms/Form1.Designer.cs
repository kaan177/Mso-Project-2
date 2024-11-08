﻿namespace Scratch_Mini_Forms
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
            pictureBox1 = new PictureBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // TurnButton
            // 
            TurnButton.BackColor = Color.FromArgb(224, 224, 224);
            TurnButton.ForeColor = SystemColors.ActiveCaptionText;
            TurnButton.Location = new Point(314, 43);
            TurnButton.Name = "TurnButton";
            TurnButton.Size = new Size(111, 58);
            TurnButton.TabIndex = 0;
            TurnButton.Text = "Turn";
            TurnButton.UseVisualStyleBackColor = false;
            TurnButton.Click += TurnButton_Click;
            // 
            // MoveButton
            // 
            MoveButton.Location = new Point(433, 43);
            MoveButton.Name = "MoveButton";
            MoveButton.Size = new Size(111, 58);
            MoveButton.TabIndex = 1;
            MoveButton.Text = "Move";
            MoveButton.UseVisualStyleBackColor = true;
            // 
            // RepeatButton
            // 
            RepeatButton.Location = new Point(197, 43);
            RepeatButton.Name = "RepeatButton";
            RepeatButton.RightToLeft = RightToLeft.No;
            RepeatButton.Size = new Size(111, 58);
            RepeatButton.TabIndex = 2;
            RepeatButton.Text = "Repeat";
            RepeatButton.UseVisualStyleBackColor = true;
            // 
            // RepeatUntilButton
            // 
            RepeatUntilButton.Location = new Point(551, 43);
            RepeatUntilButton.Name = "RepeatUntilButton";
            RepeatUntilButton.Size = new Size(111, 58);
            RepeatUntilButton.TabIndex = 3;
            RepeatUntilButton.Text = "RepeatUntil";
            RepeatUntilButton.UseVisualStyleBackColor = true;
            // 
            // OutputTxtBox
            // 
            OutputTxtBox.Location = new Point(201, 417);
            OutputTxtBox.Multiline = true;
            OutputTxtBox.Name = "OutputTxtBox";
            OutputTxtBox.ReadOnly = true;
            OutputTxtBox.Size = new Size(343, 101);
            OutputTxtBox.TabIndex = 4;
            OutputTxtBox.TextChanged += OutputTxtBox_TextChanged;
            // 
            // RunProgramButton
            // 
            RunProgramButton.Location = new Point(201, 330);
            RunProgramButton.Name = "RunProgramButton";
            RunProgramButton.Size = new Size(111, 67);
            RunProgramButton.TabIndex = 5;
            RunProgramButton.Text = "Run";
            RunProgramButton.UseVisualStyleBackColor = true;
            RunProgramButton.Click += RunProgramButton_Click;
            // 
            // MetricsButton
            // 
            MetricsButton.Location = new Point(433, 330);
            MetricsButton.Name = "MetricsButton";
            MetricsButton.Size = new Size(111, 67);
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
            menuStrip1.Size = new Size(800, 33);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "Choose File";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // chooseProgramToolStripMenuItem
            // 
            chooseProgramToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { basicToolStripMenuItem, advancedToolStripMenuItem, expertToolStripMenuItem });
            chooseProgramToolStripMenuItem.Name = "chooseProgramToolStripMenuItem";
            chooseProgramToolStripMenuItem.Size = new Size(162, 29);
            chooseProgramToolStripMenuItem.Text = "Choose Program";
            chooseProgramToolStripMenuItem.Click += chooseProgramToolStripMenuItem_Click;
            // 
            // basicToolStripMenuItem
            // 
            basicToolStripMenuItem.Name = "basicToolStripMenuItem";
            basicToolStripMenuItem.Size = new Size(193, 34);
            basicToolStripMenuItem.Text = "Basic";
            basicToolStripMenuItem.Click += basicToolStripMenuItem_Click;
            // 
            // advancedToolStripMenuItem
            // 
            advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            advancedToolStripMenuItem.Size = new Size(193, 34);
            advancedToolStripMenuItem.Text = "Advanced";
            advancedToolStripMenuItem.Click += advancedToolStripMenuItem_Click;
            // 
            // expertToolStripMenuItem
            // 
            expertToolStripMenuItem.Name = "expertToolStripMenuItem";
            expertToolStripMenuItem.Size = new Size(193, 34);
            expertToolStripMenuItem.Text = "Expert";
            expertToolStripMenuItem.Click += expertToolStripMenuItem_Click;
            // 
            // GridPanel
            // 
            GridPanel.BackColor = SystemColors.Info;
            GridPanel.Location = new Point(551, 108);
            GridPanel.Name = "GridPanel";
            GridPanel.Size = new Size(227, 214);
            GridPanel.TabIndex = 9;
            // 
            // UserInputTxtBox
            // 
            UserInputTxtBox.Location = new Point(201, 108);
            UserInputTxtBox.Multiline = true;
            UserInputTxtBox.Name = "UserInputTxtBox";
            UserInputTxtBox.Size = new Size(343, 212);
            UserInputTxtBox.TabIndex = 10;
            // 
            // pictureBox1
            // 
            pictureBox1.ImageLocation = "";
            pictureBox1.Location = new Point(55, 140);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(257, 200);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 528);
            Controls.Add(pictureBox1);
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
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private PictureBox pictureBox1;
    }
}